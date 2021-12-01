namespace AuthApi.Test.Logic
{
	using System;
	using AuthApi.Contracts;
	using AuthApi.Logic;
	using AuthApi.Model;
	using AuthApi.Test.Mock;
	using Microsoft.Extensions.Configuration;
	using Xunit;

	/// <summary>
	///   Tests for <see cref="AuthProvider" />.
	/// </summary>
	public class AuthProviderTests
	{
		[Fact]
		public void AuthProviderCtorTests()
		{
			Assert.Throws<ArgumentNullException>(() => new AuthProvider(null, null, null));
			Assert.Throws<ArgumentNullException>(() => new AuthProvider(new DatabaseMock(), new HashProvider(), null));
			Assert.Throws<ArgumentNullException>(
				() => new AuthProvider(new DatabaseMock(), null, new JwtProvider(new JwtConfiguration())));
			Assert.Throws<ArgumentNullException>(
				() => new AuthProvider(null, new HashProvider(), new JwtProvider(new JwtConfiguration())));
		}

		[Fact]
		public async void InitializeFails()
		{
			var provider = CreateAuthProvider(false);
			var result = await provider.InitializeAsync();
			Assert.NotNull(result);
			Assert.Equal(AuthResult.InternalError, result.AuthResult);
			Assert.Null(result.Token);
		}

		[Fact]
		public async void InitializeSucceeds()
		{
			var provider = CreateAuthProvider(true);
			var result = await provider.InitializeAsync();
			Assert.NotNull(result);
			Assert.Equal(AuthResult.Created, result.AuthResult);
			Assert.Null(result.Token);
		}

		[Theory]
		// api key tests
		[InlineData(
			null,
			"8c0c9596-92db-4f1a-8aa5-45479378d475",
			"userName",
			"password",
			false,
			null,
			AuthResult.Forbidden,
			false)]
		[InlineData(
			"8c0c9596-92db-4f1a-8aa5-45479378d475",
			null,
			"userName",
			"password",
			false,
			null,
			AuthResult.Forbidden,
			false)]
		[InlineData(
			"8c0c9596-92db-4f1a-8aa5-45479378d475",
			"8c0c9596-92db-4f1a-8aa5-45479378d476",
			"userName",
			"password",
			false,
			null,
			AuthResult.Forbidden,
			false)]
		// data tests
		[InlineData(
			"8c0c9596-92db-4f1a-8aa5-45479378d475",
			"8c0c9596-92db-4f1a-8aa5-45479378d475",
			null,
			"password",
			false,
			null,
			AuthResult.BadRequest,
			false)]
		[InlineData(
			"8c0c9596-92db-4f1a-8aa5-45479378d475",
			"8c0c9596-92db-4f1a-8aa5-45479378d475",
			"userName",
			null,
			false,
			null,
			AuthResult.BadRequest,
			false)]
		[InlineData(
			"8c0c9596-92db-4f1a-8aa5-45479378d475",
			"8c0c9596-92db-4f1a-8aa5-45479378d475",
			null,
			null,
			false,
			null,
			AuthResult.BadRequest,
			false)]
		// user does not exist
		[InlineData(
			"8c0c9596-92db-4f1a-8aa5-45479378d475",
			"8c0c9596-92db-4f1a-8aa5-45479378d475",
			"userName",
			"password",
			false,
			null,
			AuthResult.Unauthorized,
			false)]
		// invalid password
		[InlineData(
			"8c0c9596-92db-4f1a-8aa5-45479378d475",
			"8c0c9596-92db-4f1a-8aa5-45479378d475",
			"userName",
			"password",
			true,
			"password2",
			AuthResult.Unauthorized,
			false)]
		// signin success
		[InlineData(
			"8c0c9596-92db-4f1a-8aa5-45479378d475",
			"8c0c9596-92db-4f1a-8aa5-45479378d475",
			"userName",
			"password",
			true,
			"password",
			AuthResult.Authenticated,
			true)]
		public async void SignInAsync(
			string expectedApiKey,
			string requestApiKey,
			string userName,
			string password,
			bool userExists,
			string readPassword,
			AuthResult expectedAuthResult,
			bool expectedToken)
		{
			var hashProvider = new HashProvider();
			var readResult = userExists
				? new User(
					userName,
					"email",
					Roles.User,
					hashProvider.Hash(readPassword))
				: null;
			var provider = CreateAuthProvider(true, readResult);
			var result = await provider.SignInAsync(
				new RequestData
				{
					ExpectedApiKey = expectedApiKey,
					Password = password,
					RequestApiKey = requestApiKey,
					UserName = userName
				});
			Assert.NotNull(result);
			Assert.Equal(expectedAuthResult, result.AuthResult);
			if (expectedToken)
			{
				Assert.NotNull(result.Token);
			}
			else
			{
				Assert.Null(result.Token);
			}
		}

		[Theory]
		// api key tests
		[InlineData(
			null,
			"8c0c9596-92db-4f1a-8aa5-45479378d475",
			"userName",
			"password",
			"email",
			false,
			null,
			AuthResult.Forbidden,
			false)]
		[InlineData(
			"8c0c9596-92db-4f1a-8aa5-45479378d475",
			null,
			"userName",
			"password",
			"email",
			false,
			null,
			AuthResult.Forbidden,
			false)]
		[InlineData(
			"8c0c9596-92db-4f1a-8aa5-45479378d475",
			"8c0c9596-92db-4f1a-8aa5-45479378d476",
			"userName",
			"password",
			"email",
			false,
			null,
			AuthResult.Forbidden,
			false)]
		// data tests
		[InlineData(
			"8c0c9596-92db-4f1a-8aa5-45479378d475",
			"8c0c9596-92db-4f1a-8aa5-45479378d475",
			null,
			"password",
			"email",
			false,
			null,
			AuthResult.BadRequest,
			false)]
		[InlineData(
			"8c0c9596-92db-4f1a-8aa5-45479378d475",
			"8c0c9596-92db-4f1a-8aa5-45479378d475",
			"userName",
			null,
			"email",
			false,
			null,
			AuthResult.BadRequest,
			false)]
		[InlineData(
			"8c0c9596-92db-4f1a-8aa5-45479378d475",
			"8c0c9596-92db-4f1a-8aa5-45479378d475",
			null,
			null,
			"email",
			false,
			null,
			AuthResult.BadRequest,
			false)]
		[InlineData(
			"8c0c9596-92db-4f1a-8aa5-45479378d475",
			"8c0c9596-92db-4f1a-8aa5-45479378d475",
			"userName",
			"password",
			null,
			false,
			null,
			AuthResult.BadRequest,
			false)]
		// user does not exist
		[InlineData(
			"8c0c9596-92db-4f1a-8aa5-45479378d475",
			"8c0c9596-92db-4f1a-8aa5-45479378d475",
			"userName",
			"password",
			"email",
			false,
			null,
			AuthResult.Created,
			true)]
		// user exists
		[InlineData(
			"8c0c9596-92db-4f1a-8aa5-45479378d475",
			"8c0c9596-92db-4f1a-8aa5-45479378d475",
			"userName",
			"password",
			"email",
			true,
			"password",
			AuthResult.AlreadyExists,
			false)]
		public async void SignUpAsync(
			string expectedApiKey,
			string requestApiKey,
			string userName,
			string password,
			string email,
			bool userExists,
			string readPassword,
			AuthResult expectedAuthResult,
			bool expectedToken)
		{
			var hashProvider = new HashProvider();
			var readResult = userExists
				? new User(
					userName,
					"email",
					Roles.User,
					hashProvider.Hash(readPassword))
				: null;

			var provider = CreateAuthProvider(true, readResult, !userExists);
			var result = await provider.SignUpAsync(
				new RequestData
				{
					ExpectedApiKey = expectedApiKey,
					Password = password,
					RequestApiKey = requestApiKey,
					UserName = userName,
					Email = email
				});
			Assert.NotNull(result);
			Assert.Equal(expectedAuthResult, result.AuthResult);
			if (expectedToken)
			{
				Assert.NotNull(result.Token);
			}
			else
			{
				Assert.Null(result.Token);
			}
		}

		private static IAuthProvider CreateAuthProvider(
			bool initializeResult,
			IUser readResult = null,
			bool createdResult = true)
		{
			var config = new ConfigurationBuilder()
				.SetBasePath(AppContext.BaseDirectory)
				.AddJsonFile("appsettings.json", false, true)
				.Build();
			var appConfiguration = new AppConfiguration();
			config.Bind(appConfiguration);
			return new AuthProvider(
				new DatabaseMock(initializeResult, readResult, createdResult),
				new HashProvider(),
				new JwtProvider(appConfiguration.Jwt));
		}
	}
}