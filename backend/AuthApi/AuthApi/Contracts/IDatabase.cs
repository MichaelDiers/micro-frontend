namespace AuthApi.Contracts
{
	using System.Threading.Tasks;

	/// <summary>
	///   Access to the user database.
	/// </summary>
	public interface IDatabase
	{
		/// <summary>
		///   Create a new user.
		/// </summary>
		/// <param name="user">The user data.</param>
		/// <returns>A <see cref="Task" /> whose result is true if the user is created and false otherwise.</returns>
		Task<bool> CreateAsync(IUser user);

		/// <summary>
		///   InitializeAsync the user database.
		/// </summary>
		/// <returns>A <see cref="Task" /> whose result is true if the database is initialized and false otherwise.</returns>
		Task<bool> Initialize();

		/// <summary>
		///   Read existing user from the database.
		/// </summary>
		/// <param name="userName">The name of the user.</param>
		/// <returns>A <see cref="Task" /> whose result is the user if an user with specified user name exists and null otherwise.</returns>
		Task<IUser> ReadAsync(string userName);
	}
}