using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using WarUp.Core.Logics.Models.Ability;

namespace WarUp.Core.Logics.Models.Instructions.Move
{
	public class MoveTowardPointInstruction : MoveInstructionBase
	{
		private readonly Vector2 Target;

		public MoveTowardPointInstruction(IMovable gameObject, Vector2 destination) : base(gameObject)
		{
			Target = destination;
		}

		public override void Tick()
		{
			if (!Started || Done) return;

			var result = MoveTowardPoint(Target);
			Done = result;
		}
	}
}
