using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using WarUp.Core.Logics.Models.Ability;

namespace WarUp.Core.Logics.Models.Instructions.Move
{
	public abstract class MoveInstructionBase : InstructionBase
	{
		public IMovable MovableTarget => TargetObject as IMovable;

		public MoveInstructionBase(IMovable gameObject) : base(gameObject as GameObject)
		{
			if (!(gameObject is IMovable)) throw new Exception("Cunstructor input must be a IMoveable");
		}

		protected bool MoveTowardPoint(Vector2 destination)
		{
			var speed = MovableTarget.GetSpeed();
			var dist = (TargetObject.Position - destination).Length();
			if(speed >= dist)
			{
				TargetObject.Position = new Vector2(destination.X, destination.Y);
				return true;
			}

			var NormilizedDirection = (destination - TargetObject.Position) / dist;
			TargetObject.Position += NormilizedDirection * speed;

			return false;
		}
	}
}
