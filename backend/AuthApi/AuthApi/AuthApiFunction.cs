namespace AuthApi
{
	using System;
	using System.IO;
	using System.Net;
	using System.Threading.Tasks;
	using AuthApi.Contracts;
	using AuthApi.Model;
	using Google.Cloud.Functions.Framework;
	using Google.Cloud.Functions.Hosting;
	using Microsoft.AspNetCore.Http;
	using Microsoft.Extensions.Logging;
	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;

	/// <summary>
	///   Google cloud function for authenticating users.
	/// </summary>
	[FunctionsStartup(typeof(Startup))]
	public class AuthApiFunction : IHttpFunction
	{
		/// <summary>
		///   The name for the api key in the http request.
		/// </summary>
		private const string HeaderApiKeyName = "x-api-key";

		/// <summary>
		///   The name of the header used for authorization.
		/// </summary>
		private const string HeaderAuthorization = "authorization";

		/// <summary>
		///   The application configuration from appsettings.
		/// </summary>
		private readonly IAppConfiguration appConfiguration;

		/// <summary>
		///   The logic provider for authenticating users.
		/// </summary>
		private readonly IAuthProvider authProvider;

		/// <summary>
		///   The error logger.
		/// </summary>
		private readonly ILogger<AuthApiFunction> logger;

		/// <summary>
		///   Creates a new instance of <see cref="AuthApiFunction" />.
		/// </summary>
		/// <param name="logger">The error logger.</param>
		/// <param name="authProvider">Provider for handling operations on users.</param>
		/// <param name="appConfiguration">The configuration of the application.</param>
		public AuthApiFunction(
			ILogger<AuthApiFunction> logger,
			IAuthProvider authProvider,
			IAppConfiguration appConfiguration)
		{
			this.appConfiguration = appConfiguration ?? throw new ArgumentNullException(nameof(appConfiguration));

			this.authProvider = authProvider ?? throw new ArgumentNullException(nameof(authProvider));
			this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		/// <summary>
		///   The entry method and logic of the google cloud function.
		/// </summary>
		/// <param name="context">The context of the current http request.</param>
		/// <returns>An instance of <see cref="Task" />.</returns>
		public async Task HandleAsync(HttpContext context)
		{
			try
			{
				await this.HandleHttpMethodsAsync(context);
			}
			catch (Exception ex)
			{
				this.logger.LogError(ex, "Unexpected error.");
				context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
			}
		}

		/// <summary>
		///   Matches the http method of the request to a method in the auth provider and processes the request.
		/// </summary>
		/// <param name="context">The context of the current http request.</param>
		/// <returns>A <see cref="Task" />.</returns>
		private async Task HandleHttpMethodsAsync(HttpContext context)
		{
			var request = await this.ReadRequest(context);
			if (string.Equals(context.Request.Method, HttpMethods.Post, StringComparison.InvariantCultureIgnoreCase)
			    && string.Equals(context.Request.Path, "/signUp", StringComparison.InvariantCultureIgnoreCase))
			{
				var result = await this.authProvider.SignUpAsync(request);
				await HandleResult(context, result);
			}
			else if (string.Equals(context.Request.Method, HttpMethods.Post, StringComparison.InvariantCultureIgnoreCase)
			         && string.Equals(context.Request.Path, "/signIn", StringComparison.InvariantCultureIgnoreCase))
			{
				var result = await this.authProvider.SignInAsync(request);
				await HandleResult(context, result);
			}
			else if (string.Equals(context.Request.Method, HttpMethods.Post, StringComparison.InvariantCultureIgnoreCase)
			         && string.Equals(context.Request.Path, "/init", StringComparison.InvariantCultureIgnoreCase))
			{
				var result = await this.authProvider.InitializeAsync();
				await HandleResult(context, result);
			}
			else
			{
				context.Response.StatusCode = (int) HttpStatusCode.Forbidden;
			}
		}

		/// <summary>
		///   Matches the operation result to an http status code and includes
		///   the token in the response if the operation succeeded.
		/// </summary>
		/// <param name="context">The context of the current http request.</param>
		/// <param name="result">The <see cref="IAuthProviderResult" /> of the executed operation.</param>
		/// <returns>A <see cref="Task" />.</returns>
		private static async Task HandleResult(HttpContext context, IAuthProviderResult result)
		{
			if (result != null)
			{
				switch (result.AuthResult)
				{
					case AuthResult.Authenticated:
						context.Response.StatusCode = (int) HttpStatusCode.OK;
						break;
					case AuthResult.AlreadyExists:
						context.Response.StatusCode = (int) HttpStatusCode.Conflict;
						break;
					case AuthResult.Unauthorized:
						context.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
						break;
					case AuthResult.Created:
						context.Response.StatusCode = (int) HttpStatusCode.Created;
						break;
					case AuthResult.BadRequest:
						context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
						break;
					case AuthResult.Forbidden:
						context.Response.StatusCode = (int) HttpStatusCode.Forbidden;
						break;
					case AuthResult.None:
					case AuthResult.InternalError:
						context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}

				if ((result.AuthResult == AuthResult.Authenticated || result.AuthResult == AuthResult.Created)
				    && !string.IsNullOrWhiteSpace(result.Token))
				{
					var json = new JObject
					{
						[nameof(IAuthProviderResult.Token)] = result.Token
					};
					context.Response.ContentType = "application/json";
					await context.Response.WriteAsync(json.ToString());
				}
			}
		}

		/// <summary>
		///   Read the body of the request as json.
		/// </summary>
		/// <typeparam name="T">The type of the body object.</typeparam>
		/// <param name="context">The context of the current request.</param>
		/// <returns>A <see cref="Task" /> whose result is of type T.</returns>
		private static async Task<T> ReadBody<T>(HttpContext context) where T : class
		{
			try
			{
				var json = await new StreamReader(context.Request.Body).ReadToEndAsync();
				return JsonConvert.DeserializeObject<T>(json);
			}
			catch
			{
				return null;
			}
		}

		/// <summary>
		///   Read the header data of the request.
		/// </summary>
		/// <param name="context">The context of the current request.</param>
		/// <param name="header">The name of the header to be read.</param>
		/// <returns>The header value if exists and null otherwise.</returns>
		private static string ReadHeader(HttpContext context, string header)
		{
			if (context.Request?.Headers?.TryGetValue(header, out var headerValue) == true)
			{
				return headerValue;
			}

			return null;
		}

		/// <summary>
		///   Read the request data.
		/// </summary>
		/// <param name="context">The context of the current request.</param>
		/// <returns>A <see cref="Task" /> whose result is <see cref="RequestData" />.</returns>
		private async Task<RequestData> ReadRequest(HttpContext context)
		{
			try
			{
				var request = await ReadBody<RequestData>(context) ?? new RequestData();

				request.RequestApiKey = ReadHeader(context, HeaderApiKeyName);
				request.ExpectedApiKey = this.appConfiguration.ApiKey;
				request.Token = ReadHeader(context, HeaderAuthorization);
				if (!string.IsNullOrWhiteSpace(request.Token))
				{
					var token = request.Token.Split(" ");
					if (token.Length == 2)
					{
						request.Token = token[1];
					}
				}

				return request;
			}
			catch
			{
				return null;
			}
		}
	}
}