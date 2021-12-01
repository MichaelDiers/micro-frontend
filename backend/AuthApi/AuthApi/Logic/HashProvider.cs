namespace AuthApi.Logic
{
	using AuthApi.Contracts;
	using BCrypt.Net;

	/// <summary>
	///   Provides operations for hashing and verifying hashes.
	/// </summary>
	public class HashProvider : IHashProvider
	{
		/// <summary>
		///   Create the hash for the given text.
		/// </summary>
		/// <param name="text">The text to be hashed.</param>
		/// <returns>The created hash.</returns>
		public string Hash(string text)
		{
			return BCrypt.HashPassword(text);
		}

		/// <summary>
		///   Verifies that the given text and hash do match.
		/// </summary>
		/// <param name="text">The text that should match the hash.</param>
		/// <param name="hash">The hash that should match the text.</param>
		/// <returns>True if text and hash do match and false otherwise.</returns>
		public bool Verify(string text, string hash)
		{
			// ReSharper disable RedundantArgumentDefaultValue
			return BCrypt.Verify(
				text,
				hash,
				false,
				HashType.SHA384);
			// ReSharper restore RedundantArgumentDefaultValue
		}
	}
}