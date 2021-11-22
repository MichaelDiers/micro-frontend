namespace AuthApi.Contracts
{
	/// <summary>
	///   Specification of results for <see cref="IAuthProvider" /> operations.
	/// </summary>
	public enum AuthResult
	{
		/// <summary>
		///   Undefined value.
		/// </summary>
		None = 0,

		/// <summary>
		///   Operation succeeded and user is authenticated.
		/// </summary>
		Authenticated = 1,

		/// <summary>
		///   An internal server error occurred.
		/// </summary>
		InternalError = 2,

		/// <summary>
		///   The user already exists and cannot be created.
		/// </summary>
		AlreadyExists = 3,

		/// <summary>
		///   User is unknown or user and password do not match.
		/// </summary>
		Unauthorized = 4,

		/// <summary>
		///   Input data is invalid.
		/// </summary>
		BadRequest = 5
	}
}