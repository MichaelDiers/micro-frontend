namespace AuthApi
{
	using AuthApi.Contracts;
	using AuthApi.Logic;
	using Google.Cloud.Functions.Hosting;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Logging;

	/// <summary>
	///   Initialize the google cloud function.
	/// </summary>
	public class Startup : FunctionsStartup
	{
		/// <summary>
		///   Configure logging for the cloud function.
		/// </summary>
		/// <param name="context">The <see cref="WebHostBuilderContext" />.</param>
		/// <param name="logging">The <see cref="ILoggingBuilder" />.</param>
		public override void ConfigureLogging(WebHostBuilderContext context, ILoggingBuilder logging)
		{
			// logging.ClearProviders();
		}

		/// <summary>
		///   Initialize dependencies.
		/// </summary>
		/// <param name="context">The <see cref="WebHostBuilderContext" />.</param>
		/// <param name="services">The <see cref="IServiceCollection" />.</param>
		public override void ConfigureServices(WebHostBuilderContext context, IServiceCollection services)
		{
			services.AddScoped<IAuthProvider, AuthProvider>();
		}
	}
}