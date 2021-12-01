namespace AuthApi.Extensions
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Security.Claims;
	using AuthApi.Contracts;
	using AuthApi.Model;

	/// <summary>
	///   Extensions for user and claim handling.
	/// </summary>
	public static class UserExtensions
	{
		/// <summary>
		///   Create a user from claims.
		/// </summary>
		/// <param name="claims">The claims that contain the user data.</param>
		/// <returns>A new <see cref="IUser" />.</returns>
		public static IUser FromClaims(this IEnumerable<Claim> claims)
		{
			if (claims == null)
			{
				return null;
			}

			var user = new DatabaseUser();
			foreach (var claim in claims)
			{
				switch (claim.Type)
				{
					case nameof(IUser.UserName):
						user.UserName = claim.Value;
						break;
					case nameof(IUser.Roles):
						if (Enum.TryParse(claim.Value, out Roles roles))
						{
							user.Roles = roles;
						}

						break;
				}
			}

			return user;
		}

		/// <summary>
		///   Create claims from user data.
		/// </summary>
		/// <param name="user">The user from that the claims are created.</param>
		/// <returns>An <see cref="IEnumerable{T}" /> of <see cref="Claim" />.</returns>
		public static IEnumerable<Claim> ToClaims(this IUser user)
		{
			if (user == null)
			{
				return Enumerable.Empty<Claim>();
			}

			return new[]
			{
				new Claim(nameof(IUser.UserName), user.UserName),
				new Claim(nameof(IUser.Roles), user.Roles.ToString())
			};
		}
	}
}