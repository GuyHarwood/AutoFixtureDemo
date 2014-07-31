using System;
using System.Linq;
using AutoFix.Demo;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture.Xunit;
using Xunit;
using Xunit.Extensions;

namespace Tests
{
	public class AccountServiceTests
	{
		[Theory, AutoMoqData]
		public void WhenAccountIdEmptyArgumentExceptionThrown(AccountService sut)
		{
			var ex = Assert.Throws<ArgumentException>(() => sut.GetPrimaryAccountHolder(Guid.Empty));
			Assert.Contains("AccountId cannot be empty", ex.Message);
		}

		[Theory, AutoMoqData]
		public void ReturnsPrimaryAccountHolderWhenFound([Frozen]Mock<IAccountRepository> accountRepository, AccountService sut)
		{
			var expected = new Person(Enumerable.Empty<Address>());
			accountRepository.Setup(r => r.Lookup(It.IsAny<AccountLookup>())).Returns(expected);
			var actual = sut.GetPrimaryAccountHolder(Guid.NewGuid());
			Assert.Equal(expected, actual);
		}
	}

	public class AutoMoqDataAttribute : AutoDataAttribute
	{
		public AutoMoqDataAttribute()
			: base(new Fixture()
				.Customize(new AutoMoqCustomization()))
		{
		}
	}
}