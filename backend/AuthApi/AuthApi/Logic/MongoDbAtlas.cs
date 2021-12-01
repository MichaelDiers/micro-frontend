namespace AuthApi.Logic
{
	using System.Diagnostics;
	using System.Threading.Tasks;
	using AuthApi.Contracts;
	using AuthApi.Model;
	using MongoDB.Bson;
	using MongoDB.Driver;

	/// <summary>
	///   Access to the user database.
	/// </summary>
	public class MongoDbAtlas : IDatabase
	{
		/// <summary>
		///   The configuration of the database.
		/// </summary>
		private readonly IMongoDbAtlasConfiguration configuration;

		/// <summary>
		///   The mongo database.
		/// </summary>
		private readonly IMongoDatabase database;

		/// <summary>
		///   Creates a new instance of <see cref="MongoDbAtlas" />.
		/// </summary>
		/// <param name="configuration">The database configuration.</param>
		public MongoDbAtlas(IMongoDbAtlasConfiguration configuration)
		{
			Debug.WriteLine(configuration.ConnectionString);
			var settings = MongoClientSettings.FromConnectionString(configuration.ConnectionString);
			var client = new MongoClient(settings);
			this.database = client.GetDatabase(configuration.DatabaseName);
			this.configuration = configuration;
		}

		/// <summary>
		///   Create a new user.
		/// </summary>
		/// <param name="user">The user data.</param>
		/// <returns>A <see cref="Task" /> whose result is true if the user is created and false otherwise.</returns>
		public async Task<bool> CreateAsync(IUser user)
		{
			try
			{
				if (await this.ReadAsync(user.UserName) != null)
				{
					return false;
				}

				var databaseUser = new DatabaseUser(user);
				var collection = this.database.GetCollection<DatabaseUser>(this.configuration.CollectionName);
				await collection.InsertOneAsync(databaseUser);

				return true;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		///   InitializeAsync the user database.
		/// </summary>
		/// <returns>A <see cref="Task" /> whose result is true if the database is initialized and false otherwise.</returns>
		public async Task<bool> Initialize()
		{
			try
			{
				// drop collection
				await this.database.DropCollectionAsync(this.configuration.CollectionName);

				var defaultCollation = new Collation("de", strength: CollationStrength.Primary);

				// create collection
				await this.database.CreateCollectionAsync(
					this.configuration.CollectionName,
					new CreateCollectionOptions
					{
						Collation = defaultCollation
					});

				// create index
				var collection = this.database.GetCollection<DatabaseUser>(this.configuration.CollectionName);
				var indexKeyDefinition = Builders<DatabaseUser>.IndexKeys.Ascending(
					new StringFieldDefinition<DatabaseUser>(nameof(DatabaseUser.UserName)));
				await collection.Indexes.CreateOneAsync(
					new CreateIndexModel<DatabaseUser>(
						indexKeyDefinition,
						new CreateIndexOptions
						{
							Name = "UniqueUserName",
							Unique = true,
							Collation = defaultCollation
						}));

				// create user
				await collection.InsertOneAsync(
					new DatabaseUser
					{
						Id = ObjectId.GenerateNewId(),
						Password = this.configuration.ServicePassword,
						UserName = this.configuration.ServiceUser,
						Roles = Roles.Service
					});

				return true;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		///   Read existing user from the database.
		/// </summary>
		/// <param name="userName">The name of the user.</param>
		/// <returns>A <see cref="Task" /> whose result is the user if an user with specified user name exists and null otherwise.</returns>
		public async Task<IUser> ReadAsync(string userName)
		{
			try
			{
				var filter = Builders<DatabaseUser>.Filter.Eq(nameof(DatabaseUser.UserName), userName);
				var result = await this.database.GetCollection<DatabaseUser>(this.configuration.CollectionName)
					.Find(filter)
					.FirstOrDefaultAsync();
				return result != null ? new User(result) : null;
			}
			catch
			{
				return null;
			}
		}
	}
}