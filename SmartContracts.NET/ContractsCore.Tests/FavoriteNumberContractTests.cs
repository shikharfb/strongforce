using ContractsCore.Tests.Mocks;
using Xunit;

namespace ContractsCore.Tests
{
	public class FavoriteNumberContractTests
	{
		private readonly IAddressFactory addressFactory = new RandomAddressFactory();

		[Fact]
		public void Receive_WhenPassedSetFavoriteNumberAction_ReturnsTrue()
		{
			Address address = this.addressFactory.Create();
			var contract = new FavoriteNumberContract(address);
			var action = new SetFavoriteNumberAction(
				string.Empty,
				address,
				address,
				address,
				0);
			Assert.True(contract.Receive(action));
		}

		[Fact]
		public void Receive_WhenPassedSetFavoriteNumberAction_SetsNumberCorrectly()
		{
			const int expectedNumber = 32;
			Address address = this.addressFactory.Create();
			var contract = new FavoriteNumberContract(address);
			var action = new SetFavoriteNumberAction(
				string.Empty,
				address,
				address,
				address,
				expectedNumber);
			contract.Receive(action);

			Assert.Equal(expectedNumber, contract.Number);
		}
	}
}