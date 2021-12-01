namespace AuthApi.Model
{
	using AuthApi.Contracts;

	/// <summary>
	///   Describes the appsettings.
	/// </summary>
	public class AppConfiguration : IAppConfiguration
	{
		/// <summary>
		///   Gets or sets the api key.
		/// </summary>
		public string ApiKey { get; set; }

		/// <summary>
		///   Gets or sets the json web token configuration.
		/// </summary>
		public JwtConfiguration Jwt { get; set; }

		/// <summary>
		///   Gets or sets the mongodb configuration.
		/// </summary>
		public MongoDbAtlasConfiguration MongoDbAtlas { get; set; }
	}
}