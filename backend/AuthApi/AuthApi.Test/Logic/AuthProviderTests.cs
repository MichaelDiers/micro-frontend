namespace AuthApi.Test.Logic
{
	using AuthApi.Contracts;
	using AuthApi.Logic;
	using AuthApi.Model;
	using AuthApi.Test.Mock;
	using Xunit;

	/// <summary>
	///   Tests for <see cref="AuthProvider" />.
	/// </summary>
	public class AuthProviderTests
	{
		[Fact]
		public async void SignInAsync_null_BadRequest()
		{
			var provider = Create();
			var result = await provider.SignInAsync(null);
			Assert.Equal(AuthResult.BadRequest, result.AuthResult);
			Assert.Null(result.Token);
		}

		[Fact]
		public async void SignInAsync_UnknownUser_null()
		{
			var provider = Create();
			var result = await provider.SignInAsync(new User("email", "password"));
			Assert.NotNull(result);
			Assert.Equal(AuthResult.Unauthorized, result.AuthResult);
			Assert.Null(result.Token);
		}

		[Theory]
		[InlineData(
			"email",
			"password",
			"Email")]
		[InlineData(
			"email",
			"password",
			"EMAIL")]
		[InlineData(
			"email",
			"password",
			"emaiL")]
		public async void SignUp_ExistingUser_AlreadyExists(string existingUser, string existingPassword, string newUser)
		{
			var provider = Create();
			var createResult = await provider.SignUpAsync(new User(existingUser, existingPassword));
			Assert.NotNull(createResult);
			Assert.Equal(AuthResult.Authenticated, createResult.AuthResult);
			Assert.Null(createResult.Token);

			createResult = await provider.SignUpAsync(new User(newUser, existingPassword));
			Assert.NotNull(createResult);
			Assert.Equal(AuthResult.AlreadyExists, createResult.AuthResult);
		}

		[Fact]
		public async void SignUp_ValidUser_Authenticated()
		{
			var provider = Create();
			var result = await provider.SignUpAsync(new User("email", "password"));
			Assert.Equal(AuthResult.Authenticated, result.AuthResult);
			// Todo
		}

		[Theory]
		[InlineData("email", "password", "email")]
		[InlineData("email", "password", "Email")]
		[InlineData("email", "password", "EmaiL")]
		[InlineData("email", "password", "EMAIL")]
		public async void SignUpAsync_ExistingUser_User(string email, string password, string readEmail)
		{
			var provider = Create();
			var createdResult = await provider.SignUpAsync(new User(email, password));
			Assert.NotNull(createdResult);
			Assert.Equal(AuthResult.Authenticated, createdResult.AuthResult);

			var signInResult = await provider.SignInAsync(new User(readEmail, password));
			Assert.NotNull(signInResult);
			Assert.Equal(AuthResult.Authenticated, signInResult.AuthResult);
			// todo
		}

		[Theory]
		[InlineData(null, null)]
		[InlineData("", null)]
		[InlineData(null, "")]
		[InlineData("", "")]
		[InlineData(null, "valid")]
		[InlineData("", "valid")]
		[InlineData("valid", null)]
		[InlineData("valid", "")]
		public async void SignUpAsync_InvalidUser_BadRequest(string email, string password)
		{
			var provider = Create();
			var result = await provider.SignUpAsync(
				new User
				{
					Email = email,
					Password = password
				});
			Assert.Equal(AuthResult.BadRequest, result.AuthResult);
			Assert.Null(result.Token);
		}

		[Fact]
		public async void SignUpAsync_null_BadRequest()
		{
			var provider = Create();
			var result = await provider.SignUpAsync(null);
			Assert.Equal(AuthResult.BadRequest, result.AuthResult);
			Assert.Null(result.Token);
		}

		private static IAuthProvider Create()
		{
			return new AuthProvider(new DatabaseMock());
		}
	}
}