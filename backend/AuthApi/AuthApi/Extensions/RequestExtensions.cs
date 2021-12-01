namespace AuthApi.Extensions
{
	using AuthApi.Contracts;

	/// <summary>
	///   Extensions for request objects.
	/// </summary>
	public static class RequestExtensions
	{
		/// <summary>
		///   Validate a <see cref="ISignUpRequest" />.
		/// </summary>
		/// <param name="request">The request to be validated.</param>
		/// <returns>True if the request is valid and false otherwise.</returns>
		public static bool Validate(this ISignUpRequest request)
		{
			return !string.IsNullOrWhiteSpace(request?.Email)
			       && !string.IsNullOrWhiteSpace(request.Password)
			       && !string.IsNullOrWhiteSpace(request.UserName)
			       && !string.IsNullOrWhiteSpace(request.RequestApiKey)
			       && !string.IsNullOrWhiteSpace(request.ExpectedApiKey);
		}

		/// <summary>
		///   Validate a <see cref="ISignInRequest" />.
		/// </summary>
		/// <param name="request">The request to be validated.</param>
		/// <returns>True if the request is valid and false otherwise.</returns>
		public static bool Validate(this ISignInRequest request)
		{
			return !string.IsNullOrWhiteSpace(request.Password)
			       && !string.IsNullOrWhiteSpace(request.UserName)
			       && !string.IsNullOrWhiteSpace(request.RequestApiKey)
			       && !string.IsNullOrWhiteSpace(request.ExpectedApiKey);
		}
	}
}