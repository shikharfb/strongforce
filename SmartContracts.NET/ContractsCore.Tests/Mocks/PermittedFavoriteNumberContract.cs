using System.Collections.Generic;
using System.Linq;
using StrongForce.Core.Permissions;

namespace StrongForce.Core.Tests.Mocks
{
	public class PermittedFavoriteNumberContract : AclPermittedContract
	{
		public PermittedFavoriteNumberContract(Address address, ContractRegistry registry, Address permissionManager)
			: base(address, registry, permissionManager)
		{
		}

		public PermittedFavoriteNumberContract(Address address, ContractRegistry registry, Address permissionManager, AccessControlList acl)
			: base(address, registry, permissionManager, acl)
		{
		}

		public int Number { get; private set; }

		public List<List<Address>> LastWays = new List<List<Address>>();

		protected override object GetState() => this.Number;

		protected override bool HandleReceivedAction(Action action)
		{
			switch (action)
			{
				case SetFavoriteNumberAction favoriteNumberAction:
					this.HandleSetNumberAction(favoriteNumberAction);
					return true;

				default:
					return base.HandleReceivedAction(action);
			}
		}

		private void HandleSetNumberAction(SetFavoriteNumberAction favoriteNumberAction)
		{
			this.Number = favoriteNumberAction.Number;
		}

		public bool CheckPermission(Address address, Permission permission, Address target)
		{
			return this.acl.HasPermission(address, permission, target);
		}

		public bool GenerateActionAndFindPath(Address target, int num)
		{
			var setNumberAction = new SetFavoriteNumberAction(string.Empty, target, num);
			var x = new List<TracingElement>();
			var trace = new TracingBulletAction(string.Empty, target, setNumberAction, this.BulletTaken, null, ref x);
			this.SendEvent(trace);
			return true;
		}

		protected override void BulletTaken(List<Stack<Address>> ways, Action targetAction)
		{
			foreach (var stack in ways)
			{
				this.LastWays.Add(stack.ToList());
			}

			Address target = ways[0].Pop();
			this.ForwardEvent(targetAction, target, ways[0]);
		}
	}
}