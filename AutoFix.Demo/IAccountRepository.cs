using System;

namespace AutoFix.Demo
{
	public interface IAccountRepository
	{
		Person Lookup(AccountLookup accountLookup);
	}

	public class AccountLookup
	{
		public bool Main { get; set; }
		public Guid AccountId { get; set; }
	}
}