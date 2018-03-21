using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarUp.Core.Logics.Models.Instructions
{
	public abstract class InstructionSet : IOverridable<InstructionSet>
	{
		public Queue<InstructionBase> Instructions;
		public bool Enabled { get; set; }

		public InstructionSet()
		{
			Enabled = true;
		}

		public abstract bool CanOverride(InstructionSet other);
		
	}
}
