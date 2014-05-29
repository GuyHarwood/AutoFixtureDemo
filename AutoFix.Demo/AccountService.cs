using System;

namespace AutoFix.Demo
{
	public class AccountService
	{
		private readonly IAccountRepository accountRepository;
		private readonly ILog log;

		public AccountService(IAccountRepository accountRepository, ILog log)
		{
			this.accountRepository = accountRepository;
			this.log = log;
		}

		public Person GetPrimaryAccountHolder(Guid accountId)
		{
			if (accountId.Equals(Guid.Empty))
			{
				throw new ArgumentException("AccountId cannot be empty");
			}

			log.Info("Looking up primary account holder for account {0:N}", accountId);

			var response = accountRepository.Lookup(new AccountLookup
			{
				Main = true,
				AccountId = accountId
			});

			return response;
		}
	}

	public interface ILog
	{
		void Info(string msg);
		void Info(string format, params object[] args);
	}
}