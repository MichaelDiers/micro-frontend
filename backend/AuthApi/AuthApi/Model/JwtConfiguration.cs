namespace AuthApi.Model
{
	using AuthApi.Contracts;

	/// <summary>
	///   The configuration for json web tokens.
	/// </summary>
	public class JwtConfiguration : IJwtConfiguration
	{
		/// <summary>
		///   Gets the token audience.
		/// </summary>
		public string Audience { get; set; }

		/// <summary>
		///   Gets a value that indicates when the token will expire in minutes.
		/// </summary>
		public int Expires { get; set; }

		/// <summary>
		///   Gets the token issuer.
		/// </summary>
		public string Issuer { get; set; }

		/// <summary>
		///   Gets the rsa private key.
		/// </summary>
		public string PrivateKey { get; set; }

		/// <summary>
		///   Gets the rsa public key.
		/// </summary>
		public string PublicKey { get; set; }
	}
}