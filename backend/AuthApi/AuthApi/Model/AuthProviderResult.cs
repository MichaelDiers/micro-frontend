namespace AuthApi.Model
{
	using AuthApi.Contracts;

	/// <summary>
	///   Specifies the result for operations of <see cref="IAuthProvider" />.
	/// </summary>
	public class AuthProviderResult : IAuthProviderResult
	{
		/// <summary>
		///   Creates a new instance of <see cref="AuthProviderResult" />.
		/// </summary>
		public AuthProviderResult()
		{
		}

		/// <summary>
		///   Creates a new instance of <see cref="AuthProviderResult" />.
		/// </summary>
		/// <param name="result">The result of the operation.</param>
		public AuthProviderResult(AuthResult result)
		{
			this.AuthResult = result;
		}

		/// <summary>
		///   Gets or sets the operation result that indicates success or the reason for failure.
		/// </summary>
		public AuthResult AuthResult { get; set; }

		/// <summary>
		///   Gets or sets a token if the operation succeeded and null otherwise.
		/// </summary>
		public string Token { get; set; }
	}
}