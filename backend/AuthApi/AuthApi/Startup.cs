namespace AuthApi
{
	using AuthApi.Contracts;
	using AuthApi.Logic;
	using AuthApi.Model;
	using Google.Cloud.Functions.Hosting;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Logging;

	/// <summary>
	///   InitializeAsync the google cloud function.
	/// </summary>
	public class Startup : FunctionsStartup
	{
		/// <summary>
		///   Connection to the database.
		/// </summary>
		private static IDatabase database;

		/// <summary>
		///   Lock for thread safety.
		/// </summary>
		private static readonly object SyncObj = new object();

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
			var configuration = new AppConfiguration();
			context.Configuration.Bind(configuration);

			services.AddSingleton<IHashProvider, HashProvider>();
			services.AddSingleton<IAppConfiguration>(configuration);
			services.AddSingleton<IMongoDbAtlasConfiguration>(configuration.MongoDbAtlas);
			services.AddSingleton<IJwtConfiguration>(configuration.Jwt);
			services.AddSingleton<IJwtProvider, JwtProvider>();
			services.AddSingleton(
				sp =>
				{
					if (database == null)
					{
						lock (SyncObj)
						{
							database ??= new DatabaseCache(
								new MongoDbAtlas(configuration.MongoDbAtlas, sp.GetRequiredService<ILogger<MongoDbAtlas>>()));
						}
					}

					return database;
				});
			services.AddScoped<IAuthProvider, AuthProvider>();
		}
	}
}