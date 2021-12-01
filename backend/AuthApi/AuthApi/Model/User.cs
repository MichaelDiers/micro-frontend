namespace AuthApi.Model
{
	using AuthApi.Contracts;

	/// <summary>
	///   Specifies the data of a user.
	/// </summary>
	public class User : IUser
	{
		/// <summary>
		///   Creates a new instance of <see cref="User" />.
		/// </summary>
		public User()
		{
		}

		/// <summary>
		///   Creates a new instance of <see cref="User" />.
		/// </summary>
		/// <param name="user">Data is initialized from that user.</param>
		public User(IUser user)
			: this(
				user.UserName,
				user.Email,
				user.Roles,
				user.Password)
		{
		}

		/// <summary>
		///   Creates a new instance of <see cref="User" />.
		/// </summary>
		/// <param name="userName">The name of the user.</param>
		/// <param name="email">The email address of the user.</param>
		/// <param name="roles">The roles of the user.</param>
		/// <param name="password">The password of the user.</param>
		public User(
			string userName,
			string email,
			Roles roles,
			string password)
		{
			this.UserName = userName;
			this.Email = email;
			this.Roles = roles;
			this.Password = password;
		}

		/// <summary>
		///   Gets or set the email address.
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		///   Gets or sets the password.
		/// </summary>
		public string Password { get; set; }

		/// <summary>
		///   Gets or sets the roles.
		/// </summary>
		public Roles Roles { get; set; }

		/// <summary>
		///   Gets or sets name of the user.
		/// </summary>
		public string UserName { get; set; }
	}
}