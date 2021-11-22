namespace AuthApi.Contracts
{
	/// <summary>
	///   Specifies the data of a user.
	/// </summary>
	public interface IUser
	{
		/// <summary>
		///   Gets the email address of the user.
		/// </summary>
		string Email { get; }

		/// <summary>
		///   Gets the password of the user.
		/// </summary>
		string Password { get; }
	}
}