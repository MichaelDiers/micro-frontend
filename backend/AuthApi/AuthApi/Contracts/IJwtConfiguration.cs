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
		///   Gets the rsa keys.
		/// </summary>
		string Keys { get; }
	}
}