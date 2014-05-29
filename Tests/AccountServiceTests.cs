using System;
using AutoFix.Demo;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Xunit;

namespace Tests
{
	public class AccountServiceTests
	{
		private readonly Fixture fixture;
		private readonly Mock<IAccountRepository> accountRepository;

		public AccountServiceTests()
		{
			fixture = new Fixture();
			fixture.Customize(new AutoMoqCustomization());
			accountRepository = fixture.Freeze<Mock<IAccountRepository>>();
		}

		[Fact]
		public void WhenAccountIdEmptyArgumentExceptionThrown()
		{
			var sut = fixture.Create<AccountService>();
			var ex = Assert.Throws<ArgumentException>(() => sut.GetPrimaryAccountHolder(Guid.Empty));
			Assert.Contains("AccountId cannot be empty", ex.Message);
		}

		[Fact]
		public void ReturnsPersonWhenPrimaryAccountHolderFound()
		{
			var expected = new Person();
			accountRepository.Setup(r => r.Lookup(It.IsAny<AccountLookup>())).Returns(expected);
			var sut = fixture.Create<AccountService>();
			var actual = sut.GetPrimaryAccountHolder(Guid.NewGuid());
			Assert.Equal(expected, actual);
		}
	}
}