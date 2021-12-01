namespace AuthApi.Contracts
{
	/// <summary>
	///   The configuration for json web tokens.
	/// </summary>
	public interface IJwtConfiguration
	{
		/// <summary>
		///   Gets the token audience.
		/// </summary>
		string Audience { get; }

		/// <summary>
		///   Gets a value that indicates when the token will expire in minutes.
		/// </summary>
		int Expires { get; }

		/// <summary>
		///   Gets the token issuer.
		/// </summary>
		string Issuer { get; }

		/// <summary>
		///   Gets the rsa private key.
		/// </summary>
		string PrivateKey { get; }

		/// <summary>
		///   Gets the rsa public key.
		/// </summary>
		string PublicKey { get; }
	}
}