namespace AuthApi.Contracts
{
	using AuthApi.Model;

	/// <summary>
	///   Describes the appsettings.
	/// </summary>
	public interface IAppConfiguration
	{
		/// <summary>
		///   Gets the api key.
		/// </summary>
		string ApiKey { get; }

		/// <summary>
		///   Gets json web token configuration.
		/// </summary>
		JwtConfiguration Jwt { get; }

		/// <summary>
		///   Gets the mongodb configuration.
		/// </summary>
		MongoDbAtlasConfiguration MongoDbAtlas { get; }
	}
}