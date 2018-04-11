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
	class MoveAlongWaypointRouteInstruction : MoveInstructionBase
	{
		private WaypointRoute Route;
		private bool OnRoute;
		private Waypoint StartPoint;
		[NonSerialized] private Random NextDestinationRandom;

		private Waypoint LastPoint;
		private Waypoint NextDestination;

		public MoveAlongWaypointRouteInstruction(IMovable gameObject, WaypointRoute route) : base(gameObject)
		{
			Route = route;
			OnRoute = false;
			NextDestinationRandom = new Random();
			NextDestination = null;
		}

		public override void Tick()
		{
			if (!Started || Done) return;

			if (!OnRoute)
			{
				if (StartPoint == null)
					StartPoint = FindNearestWaypoint();

				if (MoveTowardPoint(StartPoint.Position))
				{
					OnRoute = true;
					LastPoint = StartPoint;
				}
				return;
			}

			if (NextDestination == null)
			{
				NextDestination = FindNextDestination();
				if(NextDestination == null)
				{
					Done = true;
					return;
				}
			}

			if(MoveTowardPoint(NextDestination.Position))
			{
				LastPoint = NextDestination;
				NextDestination = null;
			}
		}

		private Waypoint FindNearestWaypoint()
		{
			float minLength = float.MaxValue;
			Waypoint res = null;

			IEnumerable<Waypoint> waypoints = Route.GetWaypoints();

			foreach (var node in waypoints)
			{
				if ((TargetObject.Position - node.Position).Length() < minLength)
				{
					res = node;
					minLength = (TargetObject.Position - node.Position).Length();
				}
			}
			return res;
		}

		private Waypoint FindNextDestination()
		{
			Waypoint res;
			IEnumerable<Waypoint> neighbours = Route.GetNeighboursOf(LastPoint);

			var neighboursArray = neighbours.ToArray();

			if (neighboursArray.Length == 0) return null;

			res = neighboursArray[NextDestinationRandom.Next(neighboursArray.Length)];
			return res;
		}

		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			NextDestinationRandom = new Random();
		}
	}
}
