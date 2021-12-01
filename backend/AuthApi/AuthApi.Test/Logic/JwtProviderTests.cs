namespace AuthApi.Test.Logic
{
	using System.Security.Cryptography;
	using AuthApi.Contracts;
	using AuthApi.Logic;
	using AuthApi.Model;
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

		[Theory]
		[InlineData(1024)]
		[InlineData(2048)]
		[InlineData(4096)]
		public void ValidateToken(int bits)
		{
			var payload = new User(
				"userName",
				"email",
				Roles.Service,
				"password");
			var provider = Create(bits);

			var token = provider.CreateEncodedJwt(payload);
			Assert.NotNull(token);

			var user = provider.ValidateToken(token);
			Assert.NotNull(user);
			Assert.Equal(payload.UserName, user.UserName);
			Assert.Equal(payload.Roles, user.Roles);
			Assert.Null(user.Password);
			Assert.Null(user.Email);
		}

		private static IJwtProvider Create(int bits = 2048)
		{
			var jwtConfiguration = new JwtConfiguration
			{
				Audience = "JwtAudience",
				Expires = 1,
				Issuer = "JwtIssuer"
			};

			using (var rsa = RSA.Create(bits))
			{
				jwtConfiguration.Keys = rsa.ToXmlString(true);
			}

			return new JwtProvider(jwtConfiguration);
		}
	}
}