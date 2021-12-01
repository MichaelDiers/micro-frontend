namespace AuthApi.Contracts
{
	using System;

	[Flags]
	public enum Roles
	{
		None = 0,
		User = 1,
		Service = 1 << 2
	}
}