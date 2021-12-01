namespace AuthApi.Contracts
{
	using System.Threading.Tasks;

	/// <summary>
	///   Provides operations on users for sign in and out.
	/// </summary>
	public interface IAuthProvider
	{
		/// <summary>
		///   Initializes the application data.
		/// </summary>
		/// <returns>A <see cref="Task" /> whose result is an <see cref="IAuthProviderResult" />.</returns>
		Task<IAuthProviderResult> InitializeAsync();

		/// <summary>
		///   Sign in an existing user.
		/// </summary>
		/// <param name="request">The request data.</param>
		/// <returns>A <see cref="Task" /> whose result is an <see cref="IAuthProviderResult" />.</returns>
		Task<IAuthProviderResult> SignInAsync(ISignInRequest request);

		/// <summary>
		///   Sign up a new user.
		/// </summary>
		/// <param name="request">The request data.</param>
		/// <returns>A <see cref="Task" /> whose result is an <see cref="IAuthProviderResult" />.</returns>
		Task<IAuthProviderResult> SignUpAsync(ISignUpRequest request);
	}
}