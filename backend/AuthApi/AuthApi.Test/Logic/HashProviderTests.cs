namespace AuthApi.Test.Logic
{
	using System;
	using AuthApi.Logic;
	using Xunit;

	public class HashProviderTests
	{
		[Fact]
		public void Hash()
		{
			var result = new HashProvider().Hash("Hash");
			Assert.NotNull(result);
		}

		[Fact]
		public void HashWithException()
		{
			Assert.Throws<ArgumentNullException>(() => new HashProvider().Hash(null));
		}

		[Fact]
		public void Verify()
		{
			const string input = "Hash";
			var provider = new HashProvider();
			var result = provider.Hash(input);
			Assert.NotNull(result);
			Assert.True(provider.Verify(input, result));
		}

		[Fact]
		public void VerifyWithException()
		{
			Assert.Throws<ArgumentNullException>(() => new HashProvider().Verify(null, null));
			Assert.Throws<ArgumentNullException>(() => new HashProvider().Verify(null, "hash"));
			Assert.Throws<ArgumentNullException>(() => new HashProvider().Verify("hash", null));
		}
	}
}