namespace AuthApi.Test.Logic
{
	using System;
	using AuthApi.Contracts;
	using AuthApi.Logic;
	using AuthApi.Model;
	using Microsoft.Extensions.Configuration;
	using Xunit;

	public class JwtProviderTests
	{
		[Fact]
		public void CreateEncodedJwt()
		{
			var provider = Create();
			var token = provider.CreateEncodedJwt(
				new User(
					"userName",
					"email",
					Roles.Service,
					"password"));
			Assert.NotNull(token);
		}

		[Fact]
		public void ValidateToken()
		{
			var payload = new User(
				"userName",
				"email",
				Roles.Service,
				"password");
			var provider = Create();

			var token = provider.CreateEncodedJwt(payload);
			Assert.NotNull(token);

			var user = provider.ValidateToken(token);
			Assert.NotNull(user);
			Assert.Equal(payload.UserName, user.UserName);
			Assert.Equal(payload.Roles, user.Roles);
			Assert.Null(user.Password);
			Assert.Null(user.Email);
		}

		private static IJwtProvider Create()
		{
			var config = new ConfigurationBuilder()
				.SetBasePath(AppContext.BaseDirectory)
				.AddJsonFile("appsettings.json", false, true)
				.Build();
			var appConfiguration = new AppConfiguration();
			config.Bind(appConfiguration);
			return new JwtProvider(appConfiguration.Jwt);
		}
	}
}