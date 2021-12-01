namespace AuthApi.Logic
{
	using System;
	using System.Collections.Concurrent;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using AuthApi.Contracts;

	/// <summary>
	///   Cache for database operations.
	/// </summary>
	public class DatabaseCache : IDatabase
	{
		/// <summary>
		///   The database cache.
		/// </summary>
		private static readonly IDictionary<string, IUser> Cache = new ConcurrentDictionary<string, IUser>();

		/// <summary>
		///   The actual database used if the data is not cached.
		/// </summary>
		private readonly IDatabase database;

		/// <summary>
		///   Creates a new instance of <see cref="DatabaseCache" />.
		/// </summary>
		/// <param name="database">The actual database.</param>
		public DatabaseCache(IDatabase database)
		{
			this.database = database ?? throw new ArgumentNullException(nameof(database));
		}

		/// <summary>
		///   Create a new user.
		/// </summary>
		/// <param name="user">The user data.</param>
		/// <returns>A <see cref="Task" /> whose result is true if the user is created and false otherwise.</returns>
		public async Task<bool> CreateAsync(IUser user)
		{
			var userName = user?.UserName?.ToUpperInvariant();
			if (string.IsNullOrWhiteSpace(userName) || Cache.TryGetValue(userName, out _))
			{
				return false;
			}

			if (await this.database.CreateAsync(user))
			{
				Cache.Add(userName, user);
				return true;
			}

			return false;
		}

		/// <summary>
		///   InitializeAsync the user database.
		/// </summary>
		/// <returns>A <see cref="Task" /> whose result is true if the database is initialized and false otherwise.</returns>
		public async Task<bool> Initialize()
		{
			return await this.database.Initialize();
		}

		/// <summary>
		///   Read existing user from the database.
		/// </summary>
		/// <param name="userName">The name of the user.</param>
		/// <returns>A <see cref="Task" /> whose result is the user if an user with specified user name exists and null otherwise.</returns>
		public async Task<IUser> ReadAsync(string userName)
		{
			if (!string.IsNullOrWhiteSpace(userName) && Cache.TryGetValue(userName.ToUpperInvariant(), out var user))
			{
				return user;
			}

			return await this.database.ReadAsync(userName);
		}
	}
}