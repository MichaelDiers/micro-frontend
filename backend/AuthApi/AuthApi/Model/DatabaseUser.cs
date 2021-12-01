namespace AuthApi.Model
{
	using AuthApi.Contracts;
	using MongoDB.Bson;

	/// <summary>
	///   Specifies the data of a database user.
	/// </summary>
	public class DatabaseUser : IDatabaseUser
	{
		/// <summary>
		///   Creates a new instance of <see cref="DatabaseUser" />.
		/// </summary>
		public DatabaseUser()
		{
		}

		/// <summary>
		///   Creates a new instance of <see cref="DatabaseUser" />.
		/// </summary>
		/// <param name="user">Data is initialized from the given user.</param>
		public DatabaseUser(IUser user)
			: this(
				ObjectId.GenerateNewId(),
				user.UserName,
				user.Email,
				user.Roles,
				user.Password)
		{
		}

		/// <summary>
		///   Creates a new instance of <see cref="DatabaseUser" />.
		/// </summary>
		/// <param name="objectId">The mongodb object id.</param>
		/// <param name="userName">The name of the user.</param>
		/// <param name="email">The email address of the user.</param>
		/// <param name="roles">The roles of the user.</param>
		/// <param name="password">The password of the user.</param>
		public DatabaseUser(
			ObjectId objectId,
			string userName,
			string email,
			Roles roles,
			string password)
		{
			this.Id = objectId;
			this.UserName = userName;
			this.Email = email;
			this.Roles = roles;
			this.Password = password;
		}

		/// <summary>
		///   Gets or sets the mongodb id.
		/// </summary>
		public ObjectId Id { get; set; }

		/// <summary>
		///   Gets or sets the email of the user.
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		///   Gets or sets the password of the user.
		/// </summary>
		public string Password { get; set; }

		/// <summary>
		///   Gets or sets the roles of the user.
		/// </summary>
		public Roles Roles { get; set; }

		/// <summary>
		///   Gets or sets the name of the user.
		/// </summary>
		public string UserName { get; set; }
	}
}