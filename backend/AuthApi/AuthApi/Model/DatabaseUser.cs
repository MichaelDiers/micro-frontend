namespace AuthApi.Model
{
	using System;
	using AuthApi.Contracts;

	/// <summary>
	///   Specifies the data of a database user.
	/// </summary>
	public class DatabaseUser : User, IDatabaseUser
	{
		/// <summary>
		///   Creates a new instance of <see cref="User" />.
		/// </summary>
		public DatabaseUser()
		{
		}

		/// <summary>
		///   Creates a new instance of <see cref="User" />.
		/// </summary>
		/// <param name="user">The user data.</param>
		/// <param name="key">The unique database key for the user.</param>
		public DatabaseUser(IUser user, string key)
			: this(user?.Email, user?.Password, key)
		{
		}

		/// <summary>
		///   Creates a new instance of <see cref="User" />.
		/// </summary>
		/// <param name="email">The email address of the user.</param>
		/// <param name="password">The password of the user.</param>
		/// <param name="key">The unique database key for the user.</param>
		public DatabaseUser(string email, string password, string key)
			: base(email, password)
		{
			if (string.IsNullOrWhiteSpace(key))
			{
				throw new ArgumentException("Value cannot be null or whitespace.", nameof(key));
			}

			this.Email = email;
			this.Key = key;
			this.Password = password;
		}

		/// <summary>
		///   Gets or sets the key used as an unique identifier.
		/// </summary>
		public string Key { get; set; }
	}
}