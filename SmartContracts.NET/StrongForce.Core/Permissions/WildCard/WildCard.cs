using System;
using System.Collections.Generic;
using System.Text;

namespace StrongForce.Core.Permissions
{
	public abstract class WildCard : HashSet<Address>, IComparable<WildCard>
	{
		public abstract bool IsMember(Address member);

		public abstract List<Address> GetMembers();

		public override int GetHashCode()
		{
			if (this == null)
			{
				return 0;
			}

			return this.ToString().GetHashCode();
		}

		public override bool Equals(object other)
		{
			return this.SetEquals(other as HashSet<Address>);
		}

		public int CompareTo(WildCard other)
		{
			return this.Equals(other) ? 0 : 1;
		}
	}
}