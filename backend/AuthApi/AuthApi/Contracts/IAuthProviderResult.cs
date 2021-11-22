namespace AuthApi.Contracts
{
	/// <summary>
	///   Specifies the result fot operations of <see cref="IAuthProvider" />.
	/// </summary>
	public interface IAuthProviderResult
	{
		/// <summary>
		///   Gets the operation result that indicates success or the reason for failure.
		/// </summary>
		AuthResult AuthResult { get; }

		/// <summary>
		///   Gets a token if the operation succeeded and null otherwise.
		/// </summary>
		string Token { get; }
	}
}