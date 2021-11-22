namespace AuthApi.Model
{
	using System;
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
		/// <param name="user">The user data.</param>
		public User(IUser user)
			: this(user?.Email, user?.Password)
		{
		}

		/// <summary>
		///   Creates a new instance of <see cref="User" />.
		/// </summary>
		/// <param name="email">The email address of the user.</param>
		/// <param name="password">The password of the user.</param>
		public User(string email, string password)
		{
			if (string.IsNullOrWhiteSpace(email))
			{
				throw new ArgumentException("Value cannot be null or whitespace.", nameof(email));
			}

			if (string.IsNullOrWhiteSpace(password))
			{
				throw new ArgumentException("Value cannot be null or whitespace.", nameof(password));
			}

			this.Email = email;
			this.Password = password;
		}

		/// <summary>
		///   Gets the email address of the user.
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		///   Gets the password of the user.
		/// </summary>
		public string Password { get; set; }
	}
}