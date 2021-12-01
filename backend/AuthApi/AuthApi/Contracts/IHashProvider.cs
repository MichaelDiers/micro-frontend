namespace AuthApi.Contracts
{
	/// <summary>
	///   Provides operations for hashing and verifying hashes.
	/// </summary>
	public interface IHashProvider
	{
		/// <summary>
		///   Create the hash for the given text.
		/// </summary>
		/// <param name="text">The text to be hashed.</param>
		/// <returns>The created hash.</returns>
		string Hash(string text);

		/// <summary>
		///   Verifies that the given text and hash do match.
		/// </summary>
		/// <param name="text">The text that should match the hash.</param>
		/// <param name="hash">The hash that should match the text.</param>
		/// <returns>True if text and hash do match and false otherwise.</returns>
		bool Verify(string text, string hash);
	}
}