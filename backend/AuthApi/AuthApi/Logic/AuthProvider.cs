namespace AuthApi.Logic
{
	using System.Threading.Tasks;
	using AuthApi.Contracts;
	using AuthApi.Model;

	/// <summary>
	///   Provides operations on users for sign in and out.
	/// </summary>
	public class AuthProvider : IAuthProvider
	{
		/// <summary>
		///   Provides access to the database.
		/// </summary>
		private readonly IDatabase database;

		/// <summary>
		///   Creates a new instance of <see cref="AuthProvider" />.
		/// </summary>
		/// <param name="database">The database provider.</param>
		public AuthProvider(IDatabase database)
		{
			this.database = database;
		}

		/// <summary>
		///   Sign in a user with email and password.
		/// </summary>
		/// <param name="user">The user data used for signing in.</param>
		/// <returns>
		///   A <see cref="Task" /> whose result is an <see cref="IAuthProviderResult" />
		///   that includes a token is the operation succeeds and the an operation
		///   result.
		/// </returns>
		public async Task<IAuthProviderResult> SignInAsync(IUser user)
		{
			if (!ValidateUser(user))
			{
				return new AuthProviderResult
				{
					AuthResult = AuthResult.BadRequest
				};
			}

			var result = await this.database.ReadAsync(user.Email);
			if (result == null)
			{
				return new AuthProviderResult
				{
					AuthResult = AuthResult.Unauthorized
				};
			}

			return new AuthProviderResult
			{
				AuthResult = AuthResult.Authenticated
			};
		}

		/// <summary>
		///   Sign up a user with email and password.
		/// </summary>
		/// <param name="user">The user data used for signing up.</param>
		/// <returns>
		///   A <see cref="Task" /> whose result is an <see cref="IAuthProviderResult" />
		///   that includes a token is the operation succeeds and the an operation
		///   result.
		/// </returns>
		public async Task<IAuthProviderResult> SignUpAsync(IUser user)
		{
			if (!ValidateUser(user))
			{
				return new AuthProviderResult
				{
					AuthResult = AuthResult.BadRequest
				};
			}

			var result = await this.database.CreateAsync(user);
			if (!result)
			{
				return new AuthProviderResult
				{
					AuthResult = AuthResult.AlreadyExists
				};
			}

			return new AuthProviderResult
			{
				AuthResult = AuthResult.Authenticated
			};
		}

		/// <summary>
		///   Validate the user input.
		/// </summary>
		/// <param name="user">The provided user data.</param>
		/// <returns>True if the user is valid and false otherwise.</returns>
		private static bool ValidateUser(IUser user)
		{
			return user != null && !string.IsNullOrWhiteSpace(user.Email) && !string.IsNullOrWhiteSpace(user.Password);
		}
	}
}