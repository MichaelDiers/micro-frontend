namespace AuthApi.Contracts
{
	using System.Threading.Tasks;

	/// <summary>
	///   Provides operations on users for sign in and out.
	/// </summary>
	public interface IAuthProvider
	{
		/// <summary>
		///   Sign in a user with email and password.
		/// </summary>
		/// <param name="user">The user data used for signing in.</param>
		/// <returns>
		///   A <see cref="Task" /> whose result is an <see cref="IAuthProviderResult" />
		///   that includes a token is the operation succeeds and the an operation
		///   result.
		/// </returns>
		Task<IAuthProviderResult> SignInAsync(IUser user);

		/// <summary>
		///   Sign up a user with email and password.
		/// </summary>
		/// <param name="user">The user data used for signing up.</param>
		/// <returns>
		///   A <see cref="Task" /> whose result is an <see cref="IAuthProviderResult" />
		///   that includes a token is the operation succeeds and the an operation
		///   result.
		/// </returns>
		Task<IAuthProviderResult> SignUpAsync(IUser user);
	}
}