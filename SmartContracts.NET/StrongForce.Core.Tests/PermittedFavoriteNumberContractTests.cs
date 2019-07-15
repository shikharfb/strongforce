using StrongForce.Core.Exceptions;
using StrongForce.Core.Permissions;
using StrongForce.Core.Tests.Mocks;
using Xunit;

namespace StrongForce.Core.Tests
{
	public class PermittedFavoriteNumberContractTests
	{
		private readonly IAddressFactory addressFactory;
		private ContractRegistryMock registry;

		public PermittedFavoriteNumberContractTests()
		{
			this.addressFactory = new RandomAddressFactory();
			this.registry = new ContractRegistryMock();
		}

		[Fact]
		public void Receive_WhenPassedSetFavoriteNumberActionWithGrantedPermissions_SetsNumberCorrectly()
		{
			Address contractAddress = this.addressFactory.Create();
			Address permissionManager = this.addressFactory.Create();
			var contract = new PermittedFavoriteNumberContract(contractAddress, permissionManager);
			this.registry.RegisterContract(contract);

			var addPermissionAction = new AddPermissionAction(
				string.Empty,
				contractAddress,
				new Permission(typeof(SetFavoriteNumberAction)),
				permissionManager);

			const int expectedNumber = 32;
			var setNumberAction = new SetFavoriteNumberAction(
				string.Empty,
				contractAddress,
				expectedNumber);

			Assert.True(this.registry.HandleSendAction(addPermissionAction, permissionManager));
			Assert.True(this.registry.HandleSendAction(setNumberAction, permissionManager));
			Assert.Equal(expectedNumber, contract.Number);
		}

		[Fact]
		public void Receive_WhenPassedUnsupportedActionWithPermissions_ReturnsFalse()
		{
			Address permissionManager = this.addressFactory.Create();
			Address contractAddress = this.addressFactory.Create();
			Contract contract = new PermittedFavoriteNumberContract(contractAddress, permissionManager);
			this.registry.RegisterContract(contract);

			var addPermissionAction = new Action(
				string.Empty,
				contractAddress);

			Assert.Throws<NoPermissionException>(
				() => this.registry.HandleSendAction(addPermissionAction, permissionManager));
		}
	}
}