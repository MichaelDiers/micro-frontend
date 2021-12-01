namespace FunctionsTest
{
	using System;
	using System.Net;
	using System.Threading.Tasks;
	using Google.Cloud.Functions.Framework;
	using Google.Cloud.Functions.Hosting;
	using Microsoft.AspNetCore.Http;
	using Microsoft.Extensions.Logging;

	/// <summary>
	///   Google cloud function for authenticating users.
	/// </summary>
	[FunctionsStartup(typeof(Startup))]
	public class Function : IHttpFunction
	{
		private readonly ILogger<Function> logger;

		/// <summary>
		///   Creates a new instance of <see cref="Function" />.
		/// </summary>
		/// <param name="logger">The error logger.</param>
		public Function(ILogger<Function> logger)
		{
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
			await context.Response.WriteAsync("Hello World!");
			context.Response.StatusCode = (int) HttpStatusCode.OK;
		}
	}
}