namespace AuthApi.Test.Mock
{
	using System.Threading.Tasks;
	using AuthApi.Contracts;

	public class DatabaseMock : IDatabase
	{
		private readonly bool createResult;
		private readonly bool initializeResult;
		private readonly IUser readResult;

		public DatabaseMock()
		{
		}

		public DatabaseMock(bool initializeResult, IUser readResult, bool createResult)
		{
			this.initializeResult = initializeResult;
			this.readResult = readResult;
			this.createResult = createResult;
		}

		public Task<bool> CreateAsync(IUser user)
		{
			return Task.FromResult(this.createResult);
		}

		public Task<bool> Initialize()
		{
			return Task.FromResult(this.initializeResult);
		}

		public Task<IUser> ReadAsync(string userName)
		{
			return Task.FromResult(this.readResult);
		}
	}
}