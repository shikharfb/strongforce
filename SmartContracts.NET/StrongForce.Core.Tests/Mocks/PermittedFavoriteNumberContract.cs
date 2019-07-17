using System.Collections.Generic;
using System.Linq;
using StrongForce.Core.Permissions;

namespace StrongForce.Core.Tests.Mocks
{
	public class PermittedFavoriteNumberContract : AclPermittedContract
	{
		public PermittedFavoriteNumberContract(Address address, Address anyAddress, Address permissionManager)
			: this(address, permissionManager, new AccessControlList(anyAddress))
		{
		}

		public PermittedFavoriteNumberContract(Address address, Address permissionManager, AccessControlList acl)
			: base(address, permissionManager, acl)
		{
			this.LastWays = new List<List<Address>>();
		}

		public int Number { get; private set; }

		public List<List<Address>> LastWays { get; set; }

		public bool CheckPermission(Address address, Permission permission, Address target)
		{
			return this.Acl.HasPermission(address, permission, target);
		}

		public bool GenerateActionAndFindPath(Address target, int num)
		{
			var setNumberAction = new SetFavoriteNumberAction(target, num);
			var x = new List<TracingElement>();
			var trace = new TracingBulletAction(target, setNumberAction, this.BulletTaken, null, ref x);
			this.SendAction(trace);
			return true;
		}

		protected override object GetState() => this.Number;

		protected override bool HandleAction(Action action)
		{
			switch (action)
			{
				case SetFavoriteNumberAction favoriteNumberAction:
					this.HandleSetNumberAction(favoriteNumberAction);
					return true;

				default:
					return base.HandleAction(action);
			}
		}

		protected override void BulletTaken(List<Stack<Address>> ways, Action targetAction)
		{
			foreach (var stack in ways)
			{
				this.LastWays.Add(stack.ToList());
			}

			Address target = ways[0].Pop();
			this.ForwardAction(targetAction, target, ways[0]);
		}

		private void HandleSetNumberAction(SetFavoriteNumberAction favoriteNumberAction)
		{
			this.Number = favoriteNumberAction.Number;
		}
	}
}