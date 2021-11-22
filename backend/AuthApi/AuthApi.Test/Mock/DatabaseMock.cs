namespace AuthApi.Test.Mock
{
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using AuthApi.Contracts;
	using AuthApi.Logic;

	internal class DatabaseMock : Database
	{
		private readonly IDictionary<string, IDatabaseUser> database = new Dictionary<string, IDatabaseUser>();

		protected override Task<bool> CreateAsync(IDatabaseUser user)
		{
			if (this.database.ContainsKey(user.Key))
			{
				return Task.FromResult(false);
			}

			this.database[user.Key] = user;
			return Task.FromResult(true);
		}

		protected override Task<IDatabaseUser> ReadUserAsync(string email)
		{
			if (this.database.TryGetValue(email, out var user))
			{
				return Task.FromResult(user);
			}

			return Task.FromResult<IDatabaseUser>(null);
		}
	}
}