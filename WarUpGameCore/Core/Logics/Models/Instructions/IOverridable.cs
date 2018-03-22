using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarUp.Core.Logics.Models.Instructions
{
	public interface IOverridable<T>
	{
		bool CanOverride(T other);
	}
}
