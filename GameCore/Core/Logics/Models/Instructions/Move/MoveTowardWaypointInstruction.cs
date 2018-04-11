using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using WarUp.Core.Logics.MapUtils;
using WarUp.Core.Logics.Models.Ability;

namespace WarUp.Core.Logics.Models.Instructions.Move
{
	[Serializable]
	public class MoveTowardWaypointInstruction : MoveInstructionBase
	{
		public readonly Waypoint TargetWaypoint;

		public MoveTowardWaypointInstruction(IMovable gameObject, Waypoint destination) : base(gameObject)
		{
			this.TargetWaypoint = destination;
		}

		public override void Tick()
		{
			if (!Started || Done) return;

			var result = MoveTowardPoint(TargetWaypoint.Position);
			Done = result;
		}
		
	}
}
