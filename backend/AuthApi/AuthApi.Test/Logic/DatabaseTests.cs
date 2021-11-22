namespace AuthApi.Test.Logic
{
	using System;
	using AuthApi.Contracts;
	using AuthApi.Model;
	using AuthApi.Test.Mock;
	using Xunit;

	/// <summary>
	///   Tests the database access layer.
	/// </summary>
	public class DatabaseTests
	{
		[Theory]
		[InlineData("email", "password", "Email")]
		[InlineData("email", "password", "EMAIL")]
		[InlineData("email", "password", "emaiL")]
		public async void Create_ExistingUser_False(string existingUser, string existingPassword, string newUser)
		{
			var database = Create();
			var result = await database.CreateAsync(
				new User
				{
					Email = existingUser,
					Password = existingPassword
				});
			Assert.True(result);

			result = await database.CreateAsync(
				new User
				{
					Email = newUser,
					Password = existingPassword
				});
			Assert.False(result);
		}

		[Fact]
		public async void Create_ValidUser_True()
		{
			var database = Create();
			var result = await database.CreateAsync(
				new User
				{
					Email = "email",
					Password = "password"
				});
			Assert.True(result);
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
		public async void CreateAsync_InvalidUser_ArgumentException(string email, string password)
		{
			var database = Create();
			await Assert.ThrowsAsync<ArgumentException>(
				() => database.CreateAsync(
					new User
					{
						Email = email,
						Password = password
					}));
		}

		[Fact]
		public async void CreateAsync_null_ArgumentException()
		{
			var database = Create();
			await Assert.ThrowsAsync<ArgumentException>(() => database.CreateAsync(null));
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public async void ReadAsync_InvalidEmail_null(string email)
		{
			var database = Create();
			var result = await database.ReadAsync(email);
			Assert.Null(result);
		}

		[Fact]
		public async void ReadAsync_UnknownUser_null()
		{
			var database = Create();
			var result = await database.ReadAsync("email");
			Assert.Null(result);
		}

		[Theory]
		[InlineData("email", "password", "email")]
		[InlineData("email", "password", "Email")]
		[InlineData("email", "password", "EmaiL")]
		[InlineData("email", "password", "EMAIL")]
		public async void ReadAsync_ValidUser_User(string email, string password, string readEmail)
		{
			var database = Create();
			var createdResult = await database.CreateAsync(new User(email, password));
			Assert.True(createdResult);

			var readResult = await database.ReadAsync(readEmail);
			Assert.NotNull(readResult);
			Assert.Equal(email, readResult.Email);
			Assert.Equal(password, readResult.Password);
		}

		private static IDatabase Create()
		{
			return new DatabaseMock();
		}
	}
}