using System;
using System.Collections.Generic;
using ContractsCore.Actions;
using ContractsCore.Exceptions;
using ContractsCore.Permissions;
using Action = ContractsCore.Actions.Action;

namespace ContractsCore.Contracts
{
	public abstract class PermittedContract : Contract
	{
		protected PermittedContract(Address address)
			: base(address)
		{
		}

		protected internal override bool Receive(Action action)
		{
			if (action == null)
			{
				throw new ArgumentNullException(nameof(action));
			}

			switch (action)
			{
				case AddPermissionAction permissionAction:
					this.HandleAddPermissionAction(permissionAction);
					return true;

				case RemovePermissionAction permissionAction:
					this.HandleRemovePermissionAction(permissionAction);
					return true;

				default:
					return this.HandleReceivedAction(action);
			}
		}

		/*protected abstract void ReceiveTracingBullet(TracingBulletAction action);

		protected abstract List<TracingElement> GetAllowedForForwording(TracingBulletAction action,
			ref List<TracingElement> queue);

		protected internal abstract void BulletTaken(List<Stack<Address>> ways, Action targetAction);*/

		protected abstract bool CheckPermission(Action action);

		protected abstract void HandleAddPermissionAction(AddPermissionAction action);

		protected abstract void HandleRemovePermissionAction(RemovePermissionAction action);
	}
}