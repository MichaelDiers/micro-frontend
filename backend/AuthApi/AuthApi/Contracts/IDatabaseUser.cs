namespace AuthApi.Contracts
{
	/// <summary>
	///   Specifies the data of a database user.
	/// </summary>
	public interface IDatabaseUser : IUser
	{
		/// <summary>
		///   Gets the key used as an unique identifier.
		/// </summary>
		string Key { get; }
	}
}