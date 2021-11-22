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
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.Logging;
	using Newtonsoft.Json;

	/// <summary>
	///   Google cloud function for authenticating users.
	/// </summary>
	[FunctionsStartup(typeof(Startup))]
	public class AuthApiFunction : IHttpFunction
	{
		/// <summary>
		///   The name of the api key in the application configuration.
		/// </summary>
		private const string ConfigurationApiKeyName = "ApiKey";

		/// <summary>
		///   The name for the api key in the http request.
		/// </summary>
		private const string HeaderApiKeyName = "x-api-key";

		/// <summary>
		///   The api key that is expected to be in incoming http request.
		/// </summary>
		private readonly string apiKey;

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
		/// <param name="logger">A logger for error messages.</param>
		/// <param name="configuration">Provides access to the appsettings.</param>
		/// <param name="authProvider">Provider for handling users.</param>
		public AuthApiFunction(ILogger<AuthApiFunction> logger, IConfiguration configuration, IAuthProvider authProvider)
		{
			if (configuration == null)
			{
				throw new ArgumentNullException(nameof(configuration));
			}

			this.authProvider = authProvider ?? throw new ArgumentNullException(nameof(authProvider));
			this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

			this.apiKey = configuration.GetValue<string>(ConfigurationApiKeyName);
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
				if (this.HandleApiKey(context))
				{
					await this.HandleHttpMethodsAsync(context);
				}
			}
			catch (Exception ex)
			{
				this.logger.LogError(ex, "Unexpected error.");
				context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
			}
		}

		/// <summary>
		///   Validate the api key from the request. Reject the request if the
		///   api key does not match.
		/// </summary>
		/// <param name="context">The context of the current http request.</param>
		/// <returns>True if the api key is valid and false otherwise.</returns>
		private bool HandleApiKey(HttpContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException(nameof(context));
			}

			if (context.Request?.Headers?.ContainsKey(HeaderApiKeyName) == true
			    && context.Request.Headers[HeaderApiKeyName] == this.apiKey)
			{
				return true;
			}

			context.Response.StatusCode = (int) HttpStatusCode.Forbidden;
			return false;
		}

		/// <summary>
		///   Matches the http method of the request to a method in the auth provider and processes the request.
		/// </summary>
		/// <param name="context">The context of the current http request.</param>
		/// <returns>A <see cref="Task" />.</returns>
		private async Task HandleHttpMethodsAsync(HttpContext context)
		{
			if (string.Equals(context.Request.Method, HttpMethods.Post, StringComparison.InvariantCultureIgnoreCase))
			{
				var user = await ReadBody<User>(context);
				var result = await this.authProvider.SignUpAsync(user);
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
					default:
						context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
						throw new ArgumentOutOfRangeException();
				}

				if (result.AuthResult == AuthResult.Authenticated && !string.IsNullOrWhiteSpace(result.Token))
				{
					var json = $"{{ \"token\": {result.Token}}}";
					await context.Response.WriteAsync(json);
				}
			}
		}

		/// <summary>
		///   ReadAsync the json data from the body of the http request
		///   and deserializes to an object of type <typeparamref name="T" />.
		/// </summary>
		/// <typeparam name="T">The type of the expected object.</typeparam>
		/// <param name="context">The context of the current http request.</param>
		/// <returns>An instance of <typeparamref name="T" /> or null if the body is not valid.</returns>
		private static async Task<T> ReadBody<T>(HttpContext context) where T : class
		{
			try
			{
				var json = await new StreamReader(context.Request.Body).ReadToEndAsync();
				return JsonConvert.DeserializeObject<T>(json);
			}
			catch
			{
				context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
				return null;
			}
		}
	}
}