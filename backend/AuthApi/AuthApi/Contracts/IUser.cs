namespace AuthApi.Contracts
{
	/// <summary>
	///   Describes the user data.
	/// </summary>
	public interface IUser
	{
		/// <summary>
		///   Gets the email address of the user.
		/// </summary>
		string Email { get; }

		/// <summary>
		///   Gets the password.
		/// </summary>
		string Password { get; }

		/// <summary>
		///   Gets the roles of the user.
		/// </summary>
		Roles Roles { get; }

		/// <summary>
		///   Gets the name of the user.
		/// </summary>
		string UserName { get; }
	}
}