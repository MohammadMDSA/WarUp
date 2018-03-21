using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using WarUp.Core.Graphics;
using WarUp.Core.Logics;
using WarUp.Core.Logics.MapUtils;
using WarUp.Core.Logics.Models;
using WarUp.Core.Logics.Models.Instructions.Move;

namespace WarUp.Core.Storage
{
	class StorageCore
	{
		public List<FrameworkObject> Objects;

		public StorageCore()
		{
			var g = new GreenTile();
			this.Objects = new List<FrameworkObject>();
			this.Objects.Add(g);
			Waypoint w1 = new Waypoint(new Vector2(150, 150));
			Waypoint w2 = new Waypoint(new Vector2(350, 450));
			Waypoint w3 = new Waypoint(new Vector2(250, 450));
			Waypoint w8 = new Waypoint(new Vector2(550, 150));
			Waypoint w4 = new Waypoint(new Vector2(550, 450));
			Waypoint w5 = new Waypoint(new Vector2(350, 150));
			Waypoint w6 = new Waypoint(new Vector2(550, 550));
			Waypoint w7 = new Waypoint(new Vector2(350, 550));
			this.Objects.Add(w1);
			this.Objects.Add(w2);
			this.Objects.Add(w3);
			this.Objects.Add(w4);
			this.Objects.Add(w5);
			this.Objects.Add(w6);
			this.Objects.Add(w7);
			this.Objects.Add(w8);
			WaypointRoute r = new WaypointRoute(w2);
			r.AddWayPoint(w1, w2);
			r.AddWayPoint(w3, w2);
			r.AddWayPoint(w4, w2);
			r.AddWayPoint(w5, w2);
			r.AddWayPoint(w6, w2);
			r.AddWayPoint(w7, w2);
			r.AddWayPoint(w8, w2);
			this.Objects.Add(r);
			g.AddInstructionSet(new MoveInstructionSet(new MoveTowardWaypointInstruction(g, w2)));
			g.AddInstructionSet(new MoveInstructionSet(new MoveTowardPointInstruction(g, new Vector2(500, 10))));
		}

		public IEnumerable<IDrawable> GetDrawables()
		{
			var result = new List<IDrawable>();
			foreach (var item in Objects)
			{
				if (item is IDrawable)
					result.Add(item as IDrawable);
			}
			return result;
		}

		public IEnumerable<IUpdatable> GetUpdatables()
		{
			return Objects;
		}
	}
}
