namespace AuthApi.Model
{
	using AuthApi.Contracts;

	/// <summary>
	///   Specifies the result fot operations of <see cref="IAuthProvider" />.
	/// </summary>
	public class AuthProviderResult : IAuthProviderResult
	{
		/// <summary>
		///   Gets the operation result that indicates success or the reason for failure.
		/// </summary>
		public AuthResult AuthResult { get; set; }

		/// <summary>
		///   Gets a token if the operation succeeded and null otherwise.
		/// </summary>
		public string Token { get; set; }
	}
}