namespace AuthApi.Contracts
{
	using System.Threading.Tasks;

	/// <summary>
	///   Access to the user database.
	/// </summary>
	public interface IDatabase
	{
		/// <summary>
		///   CreateAsync a new user in the database.
		/// </summary>
		/// <param name="user">The user data.</param>
		/// <returns>A <see cref="Task" /> whose result is true if the user is created and false otherwise.</returns>
		Task<bool> CreateAsync(IUser user);

		/// <summary>
		///   ReadAsync user from the database.
		/// </summary>
		/// <param name="email">The email of the user.</param>
		/// <returns>A <see cref="Task" /> whose result is the user if an user with specified email exists and null otherwise.</returns>
		Task<IUser> ReadAsync(string email);
	}
}