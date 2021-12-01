namespace FunctionsTest
{
	using Google.Cloud.Functions.Hosting;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Logging;

	/// <summary>
	///   InitializeAsync the google cloud function.
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
		///   InitializeAsync dependencies.
		/// </summary>
		/// <param name="context">The <see cref="WebHostBuilderContext" />.</param>
		/// <param name="services">The <see cref="IServiceCollection" />.</param>
		public override void ConfigureServices(WebHostBuilderContext context, IServiceCollection services)
		{
		}
	}
}