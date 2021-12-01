namespace AuthApi.Contracts
{
	/// <summary>
	///   The configuration for connecting to mongo database.
	/// </summary>
	public interface IMongoDbAtlasConfiguration
	{
		/// <summary>
		///   Gets the name of the collection.
		/// </summary>
		string CollectionName { get; }

		/// <summary>
		///   Gets the database connection string.
		/// </summary>
		string ConnectionString { get; }

		/// <summary>
		///   Gets the name of the database.
		/// </summary>
		string DatabaseName { get; }

		/// <summary>
		///   Gets the password of the service user.
		/// </summary>
		string ServicePassword { get; }

		/// <summary>
		///   Gets the name of the service user.
		/// </summary>
		string ServiceUser { get; }
	}
}