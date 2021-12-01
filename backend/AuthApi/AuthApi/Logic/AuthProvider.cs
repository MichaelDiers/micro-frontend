namespace AuthApi.Logic
{
	using System;
	using System.Threading.Tasks;
	using AuthApi.Contracts;
	using AuthApi.Extensions;
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
		///   Provider for creating and validating hashes.
		/// </summary>
		private readonly IHashProvider hashProvider;

		/// <summary>
		///   Provider for creating and validating json web tokens.
		/// </summary>
		private readonly IJwtProvider jwtProvider;

		/// <summary>
		///   Creates a new instance of <see cref="AuthProvider" />.
		/// </summary>
		/// <param name="database">Provides access to the database.</param>
		/// <param name="hashProvider">A provider for creating and validating hashes.</param>
		/// <param name="jwtProvider">Provider for creating and validating json web tokens.</param>
		public AuthProvider(IDatabase database, IHashProvider hashProvider, IJwtProvider jwtProvider)
		{
			this.database = database ?? throw new ArgumentNullException(nameof(database));
			this.hashProvider = hashProvider ?? throw new ArgumentNullException(nameof(hashProvider));
			this.jwtProvider = jwtProvider ?? throw new ArgumentNullException(nameof(jwtProvider));
		}

		/// <summary>
		///   Initializes the application data.
		/// </summary>
		/// <returns>A <see cref="Task" /> whose result is an <see cref="IAuthProviderResult" />.</returns>
		public async Task<IAuthProviderResult> InitializeAsync()
		{
			var result = await this.database.Initialize();
			return new AuthProviderResult(result ? AuthResult.Created : AuthResult.InternalError);
		}

		/// <summary>
		///   Sign in an existing user.
		/// </summary>
		/// <param name="request">The request data.</param>
		/// <returns>A <see cref="Task" /> whose result is an <see cref="IAuthProviderResult" />.</returns>
		public async Task<IAuthProviderResult> SignInAsync(ISignInRequest request)
		{
			if (string.IsNullOrWhiteSpace(request?.ExpectedApiKey) || request.ExpectedApiKey != request.RequestApiKey)
			{
				return new AuthProviderResult(AuthResult.Forbidden);
			}

			if (!request.Validate())
			{
				return new AuthProviderResult(AuthResult.BadRequest);
			}

			var databaseUser = await this.database.ReadAsync(request.UserName);
			if (databaseUser == null || !this.hashProvider.Verify(request.Password, databaseUser.Password))
			{
				return new AuthProviderResult
				{
					AuthResult = AuthResult.Unauthorized
				};
			}

			return new AuthProviderResult
			{
				AuthResult = AuthResult.Authenticated,
				Token = this.jwtProvider.CreateEncodedJwt(databaseUser)
			};
		}

		/// <summary>
		///   Sign up a new user.
		/// </summary>
		/// <param name="request">The request data.</param>
		/// <returns>A <see cref="Task" /> whose result is an <see cref="IAuthProviderResult" />.</returns>
		public async Task<IAuthProviderResult> SignUpAsync(ISignUpRequest request)
		{
			if (string.IsNullOrWhiteSpace(request?.ExpectedApiKey) || request.ExpectedApiKey != request.RequestApiKey)
			{
				return new AuthProviderResult(AuthResult.Forbidden);
			}

			if (!request.Validate())
			{
				return new AuthProviderResult(AuthResult.BadRequest);
			}

			var user = new User
			{
				Email = this.hashProvider.Hash(request.Email),
				Password = this.hashProvider.Hash(request.Password),
				Roles = Roles.User,
				UserName = request.UserName
			};

			var result = await this.database.CreateAsync(user);
			if (!result)
			{
				return new AuthProviderResult(AuthResult.AlreadyExists);
			}

			return new AuthProviderResult
			{
				AuthResult = AuthResult.Created,
				Token = this.jwtProvider.CreateEncodedJwt(user)
			};
		}
	}
}