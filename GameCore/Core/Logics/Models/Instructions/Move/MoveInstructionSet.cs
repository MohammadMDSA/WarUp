using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarUp.Core.Logics.Models.Instructions.Move
{
	[Serializable]
	public class MoveInstructionSet : InstructionSet
	{
		public MoveInstructionSet(MoveInstructionBase moveInstruction)
		{
			Instructions.Enqueue(moveInstruction);
		}

		public override bool CanOverride(InstructionSet other)
		{
			// Change to true
			return false;
		}

		public override void Tick()
		{
			InstructionBase current;
			if (!TryGetFirstUndone(out current)) return;
			current.Tick();
		}

		protected override bool IsDone()
		{
			if (Instructions.Count < 1) return true;
			return false;
		}
	}
}
