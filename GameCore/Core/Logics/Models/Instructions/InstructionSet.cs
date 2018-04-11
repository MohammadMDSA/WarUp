using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarUp.Core.Utils;

namespace WarUp.Core.Logics.Models.Instructions
{
	[Serializable]
	public abstract class InstructionSet : IOverridable<InstructionSet>, ITickable
	{
		protected Queue<InstructionBase> Instructions;
		public bool Enabled { get; set; }
		public bool Done => IsDone();

		public InstructionSet()
		{
			Enabled = true;
			Instructions = new Queue<InstructionBase>();
		}

		public abstract bool CanOverride(InstructionSet other);

		public abstract void Tick();

		protected bool TryGetFirstUndone(out InstructionBase instruction)
		{
			InstructionBase current;
			instruction = null;
			if (!Instructions.TryPeek(out current))
				return false;
			while (current.Done)
			{
				Instructions.Dequeue();
				if (!Instructions.TryPeek(out current))
					return false; ;
			}
			instruction = current;
			instruction.Start();
			return true;
		}

		protected abstract bool IsDone();
	}
}
