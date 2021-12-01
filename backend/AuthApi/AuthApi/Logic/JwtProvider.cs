namespace AuthApi.Logic
{
	using System;
	using System.Collections.Generic;
	using System.IdentityModel.Tokens.Jwt;
	using System.Security.Claims;
	using System.Security.Cryptography;
	using AuthApi.Contracts;
	using AuthApi.Extensions;
	using Microsoft.IdentityModel.Tokens;

	/// <summary>
	///   Provides operations for creating and validating json web tokens.
	/// </summary>
	public class JwtProvider : IJwtProvider
	{
		/// <summary>
		///   The configuration of jwt tokens.
		/// </summary>
		private readonly IJwtConfiguration jwtConfiguration;

		/// <summary>
		///   Creates a new instance of <see cref="JwtProvider" />.
		/// </summary>
		/// <param name="jwtConfiguration">The configuration for jwt tokens.</param>
		public JwtProvider(IJwtConfiguration jwtConfiguration)
		{
			this.jwtConfiguration = jwtConfiguration ?? throw new ArgumentNullException(nameof(jwtConfiguration));
		}

		/// <summary>
		///   Create a new json web token.
		/// </summary>
		/// <param name="user">The payload is created from the user data.</param>
		/// <returns>The created token.</returns>
		public string CreateEncodedJwt(IUser user)
		{
			using var rsa = RSA.Create();
			rsa.ImportRSAPrivateKey((ReadOnlySpan<byte>) Convert.FromBase64String(this.jwtConfiguration.PrivateKey), out _);
			var signingCredentials = new SigningCredentials(new RsaSecurityKey(rsa), "RS256")
			{
				CryptoProviderFactory = new CryptoProviderFactory
				{
					CacheSignatureProviders = false
				}
			};
			var claims = new List<Claim>(user.ToClaims());

			var utcNow = DateTime.UtcNow;
			var token = new JwtSecurityTokenHandler().CreateEncodedJwt(
				this.jwtConfiguration.Issuer,
				this.jwtConfiguration.Audience,
				new ClaimsIdentity(claims),
				utcNow,
				utcNow.AddMinutes(this.jwtConfiguration.Expires),
				utcNow,
				signingCredentials);
			return token;
		}

		/// <summary>
		///   Validate a json web token.
		/// </summary>
		/// <param name="token">The token to be validated.</param>
		/// <returns>The payload as an <see cref="IUser" /> if the token is valid and null otherwise.</returns>
		public IUser ValidateToken(string token)
		{
			try
			{
				var jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
				if (jwtSecurityToken == null)
				{
					return null;
				}

				using var rsa = RSA.Create();
				rsa.ImportSubjectPublicKeyInfo(Convert.FromBase64String(this.jwtConfiguration.PublicKey), out _);
				var validationParameters = new TokenValidationParameters
				{
					IssuerSigningKey = new RsaSecurityKey(rsa),
					ValidAudience = this.jwtConfiguration.Audience,
					ValidIssuer = this.jwtConfiguration.Issuer,
					RequireAudience = true,
					RequireExpirationTime = true,
					RequireSignedTokens = true,
					ValidateIssuer = true,
					CryptoProviderFactory = new CryptoProviderFactory
					{
						CacheSignatureProviders = false
					}
				};

				var securityTokenHandler = new JwtSecurityTokenHandler();
				var principal = securityTokenHandler.ValidateToken(token, validationParameters, out _);
				return principal?.Claims.FromClaims();
			}
			catch
			{
				return null;
			}
		}
	}
}