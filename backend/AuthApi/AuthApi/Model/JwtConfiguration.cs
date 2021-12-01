namespace AuthApi.Model
{
	using AuthApi.Contracts;

	/// <summary>
	///   The configuration for json web tokens.
	/// </summary>
	public class JwtConfiguration : IJwtConfiguration
	{
		/// <summary>
		///   Gets the rsa keys.
		/// </summary>
		public string Keys { get; set; }

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
	}
}