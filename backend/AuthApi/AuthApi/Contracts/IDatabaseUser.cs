namespace AuthApi.Contracts
{
	using MongoDB.Bson;

	/// <summary>
	///   Specifies the data of a database user.
	/// </summary>
	public interface IDatabaseUser : IUser
	{
		/// <summary>
		///   Gets the mongodb id.
		/// </summary>
		ObjectId Id { get; }
	}
}