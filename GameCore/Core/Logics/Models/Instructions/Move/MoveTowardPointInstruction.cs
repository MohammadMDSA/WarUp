using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using WarUp.Core.Logics.Models.Ability;

namespace WarUp.Core.Logics.Models.Instructions.Move
{
	[Serializable]
	public class MoveTowardPointInstruction : MoveInstructionBase
	{
		[NonSerialized] private Vector2 Target;
		private float _TargetX, _TargetY;

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

		[OnSerializing]
		private void OnSerializing(StreamingContext context)
		{
			_TargetX = Target.X;
			_TargetY = Target.Y;
		}

		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			Target = new Vector2(_TargetX, _TargetY);
		}
	}
}
