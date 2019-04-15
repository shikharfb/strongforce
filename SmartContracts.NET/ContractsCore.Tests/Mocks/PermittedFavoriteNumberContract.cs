using ContractsCore.Actions;
using ContractsCore.Contracts;
using ContractsCore.Permissions;

namespace ContractsCore.Tests.Mocks
{
	public class PermittedFavoriteNumberContract : AclPermittedContract
	{
		public PermittedFavoriteNumberContract(Address address, Address permissionManager)
			: base(address, permissionManager)
		{
		}

		public PermittedFavoriteNumberContract(Address address, Address permissionManager, AccessControlList acl)
			: base(address, permissionManager, acl)
		{
		}

		public int Number { get; private set; }

		protected internal override object GetState() => this.Number;

		protected override bool HandleReceivedAction(Action action)
		{
			switch (action)
			{
				case SetFavoriteNumberAction favoriteNumberAction:
					this.HandleSetNumberAction(favoriteNumberAction);
					return true;

				default:
					return false;
			}
		}

		private void HandleSetNumberAction(SetFavoriteNumberAction action)
		{
			this.RequirePermission(action);
			this.Number = action.Number;
		}
	}
}