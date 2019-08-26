using System;
using System.Collections.Generic;
using System.Linq;
using StrongForce.Core.Kits;
using StrongForce.Core.Permissions;
using StrongForce.Core.Tests.Mocks;
using Xunit;

namespace StrongForce.Core.Tests
{
	public class KitTests
	{
		/*
		[Fact]
		public void Kit_WhenInstantiated_CreatesContracts()
		{
			var favoriteContractsCount = 2;

			var factory = new SequentialAddressFactory();
			var registry = new TestRegistry(factory);
			var kit = new FavoriteNumberKit(favoriteContractsCount);

			Address address = registry.CreateContract<KitContract>(new Dictionary<string, object>()
			{
				{ "User", null },
			});
			((KitContract)registry.GetContract(address)).Kit = kit;

			registry.SendMessage(address, address, InstantiateKitAction.Type, new Dictionary<string, object>());

			Assert.Equal(favoriteContractsCount + 1, factory.AddressCount);
		}

		[Fact]
		public void Kit_WhenInstantiatedTwice_Throws()
		{
			var registry = new TestRegistry();
			var kit = new FavoriteNumberKit(2);

			Address address = registry.CreateContract<KitContract>(new Dictionary<string, object>()
			{
				{ "User", null },
			});
			((KitContract)registry.GetContract(address)).Kit = kit;

			registry.SendMessage(address, address, InstantiateKitAction.Type, new Dictionary<string, object>());
			Assert.Throws<InvalidOperationException>(() => registry.SendMessage(address, address, InstantiateKitAction.Type, new Dictionary<string, object>()));
		}
		*/
	}
}