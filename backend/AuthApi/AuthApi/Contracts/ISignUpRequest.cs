namespace AuthApi.Contracts
{
	/// <summary>
	///   Specifies the sign up request data.
	/// </summary>
	public interface ISignUpRequest
	{
		/// <summary>
		///   Gets the email address.
		/// </summary>
		string Email { get; }

		/// <summary>
		///   Gets the expected api key.
		/// </summary>
		string ExpectedApiKey { get; }

		/// <summary>
		///   Gets the password.
		/// </summary>
		public string Password { get; }

		/// <summary>
		///   Gets the api key from the header of the request header.
		/// </summary>
		string RequestApiKey { get; }

		/// <summary>
		///   Gets the name of the user.
		/// </summary>
		string UserName { get; }
	}
}