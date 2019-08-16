using System;
using System.Collections.Generic;
using StrongForce.Core.Extensions;

namespace StrongForce.Core.Kits
{
	public class KitContract : Contract
	{
		public static Address DefaultAddress { get; } = new Address(new byte[] { 0 });

		public bool Instantiated { get; set; } = false;

		public Kit Kit { get; set; } = null;

		protected override void Initialize(IDictionary<string, object> payload)
		{
			this.Acl.AddPermission(
				null,
				InstantiateKitAction.Type,
				this.Address);

			this.Kit = (Kit)payload["Kit"];
		}

		protected override bool HandlePayloadAction(PayloadAction action)
		{
			switch (action.Type)
			{
				case InstantiateKitAction.Type:
					this.HandleInstantiateKitAction(action);
					return true;

				default:
					return base.HandlePayloadAction(action);
			}
		}

		private void HandleInstantiateKitAction(PayloadAction action)
		{
			if (this.Instantiated)
			{
				throw new InvalidOperationException("Kit was already instantiated");
			}

			this.Instantiated = true;

			this.Kit.CreateContractHandler = this.CreateContract;
			this.Kit.SendActionHandler = this.SendAction;

			this.Kit.Instantiate(action.Sender);
		}
	}
}