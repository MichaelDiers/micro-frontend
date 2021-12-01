namespace AuthApi.Contracts
{
	/// <summary>
	///   Provides operations for creating and validating json web tokens.
	/// </summary>
	public interface IJwtProvider
	{
		/// <summary>
		///   Create a new json web token.
		/// </summary>
		/// <param name="user">The payload is created from the user data.</param>
		/// <returns>The created token.</returns>
		string CreateEncodedJwt(IUser user);

		/// <summary>
		///   Validate a json web token.
		/// </summary>
		/// <param name="token">The token to be validated.</param>
		/// <returns>The payload as an <see cref="IUser" /> if the token is valid and null otherwise.</returns>
		IUser ValidateToken(string token);
	}
}