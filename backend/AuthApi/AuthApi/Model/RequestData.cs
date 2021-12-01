namespace AuthApi.Model
{
	using AuthApi.Contracts;

	/// <summary>
	///   Specifies the sign up request data.
	/// </summary>
	public class RequestData : ISignUpRequest, ISignInRequest
	{
		/// <summary>
		///   Gets or sets the json web token of the request header.
		/// </summary>
		public string Token { get; set; }

		/// <summary>
		///   Gets or sets the email address.
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		///   Gets or sets the expected api key.
		/// </summary>
		public string ExpectedApiKey { get; set; }

		/// <summary>
		///   Gets or sets the password.
		/// </summary>
		public string Password { get; set; }

		/// <summary>
		///   Gets or sets the api key from the header of the request header.
		/// </summary>
		public string RequestApiKey { get; set; }

		/// <summary>
		///   Gets or sets the name of the user.
		/// </summary>
		public string UserName { get; set; }
	}
}