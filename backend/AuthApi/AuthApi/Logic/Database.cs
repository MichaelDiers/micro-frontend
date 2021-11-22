namespace AuthApi.Logic
{
	using System.Threading.Tasks;
	using AuthApi.Contracts;
	using AuthApi.Model;

	/// <summary>
	///   Access to the user database.
	/// </summary>
	public abstract class Database : IDatabase
	{
		/// <summary>
		///   CreateAsync a new user in the database.
		/// </summary>
		/// <param name="user">The user data.</param>
		/// <returns>A <see cref="Task" /> whose result is true if the user is created and false otherwise.</returns>
		public async Task<bool> CreateAsync(IUser user)
		{
			var databaseUser = new DatabaseUser(user, user?.Email?.ToUpperInvariant());
			return await this.CreateAsync(databaseUser);
		}

		/// <summary>
		///   ReadAsync user from the database.
		/// </summary>
		/// <param name="email">The email of the user.</param>
		/// <returns>A <see cref="Task" /> whose result is the user if an user with specified email exists and null otherwise.</returns>
		public async Task<IUser> ReadAsync(string email)
		{
			if (string.IsNullOrWhiteSpace(email))
			{
				return null;
			}

			return await this.ReadUserAsync(email.ToUpperInvariant());
		}

		/// <summary>
		///   CreateAsync a new user in the database.
		/// </summary>
		/// <param name="user">The user data.</param>
		/// <returns>A <see cref="Task" /> whose result is true if the user is created and false otherwise.</returns>
		protected abstract Task<bool> CreateAsync(IDatabaseUser user);

		/// <summary>
		///   ReadAsync user from the database.
		/// </summary>
		/// <param name="email">The email of the user.</param>
		/// <returns>A <see cref="Task" /> whose result is the user if an user with specified email exists and null otherwise.</returns>
		protected abstract Task<IDatabaseUser> ReadUserAsync(string email);
	}
}