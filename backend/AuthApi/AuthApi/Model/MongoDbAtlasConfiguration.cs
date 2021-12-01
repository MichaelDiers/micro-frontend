namespace AuthApi.Model
{
	using AuthApi.Contracts;

	/// <summary>
	///   The configuration for connecting to mongo database.
	/// </summary>
	public class MongoDbAtlasConfiguration : IMongoDbAtlasConfiguration
	{
		/// <summary>
		///   Gets or sets the name of the collection.
		/// </summary>
		public string CollectionName { get; set; }

		/// <summary>
		///   Gets or sets the database connection string.
		/// </summary>
		public string ConnectionString { get; set; }

		/// <summary>
		///   Gets or sets the name of the database.
		/// </summary>
		public string DatabaseName { get; set; }

		/// <summary>
		///   Gets or sets the password for the service user.
		/// </summary>
		public string ServicePassword { get; set; }

		/// <summary>
		///   Gets or set the name of the service user.
		/// </summary>
		public string ServiceUser { get; set; }
	}
}