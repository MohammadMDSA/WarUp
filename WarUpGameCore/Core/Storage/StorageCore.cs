﻿using System;
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

			Waypoint[,] net = new Waypoint[3, 3];

			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					net[i, j] = new Waypoint(new Vector2(150 + j * 100, 200 + i * 100));
					this.Objects.Add(net[i, j]);
				}
			}

			var fin = new Waypoint(new Vector2(700, 550));

			this.Objects.Add(fin);
			
			WaypointRoute r = new WaypointRoute(net[1, 1]);

			r.AddWaypoint(net[1, 1], net[0, 0]);
			r.AddWaypoint(net[1, 1], net[0, 2]);
			r.AddWaypoint(net[1, 1], net[2, 2]);
			r.AddWaypoint(net[1, 1], net[2, 0]);

			r.AddWaypoint(net[0, 0], net[0, 1]);
			r.AddWaypoint(net[0, 2], net[1, 2]);
			r.AddWaypoint(net[2, 2], net[2, 1]);
			r.AddWaypoint(net[2, 0], net[1, 0]);

			r.AddWaypoint(net[0, 1], net[1, 1]);
			r.AddWaypoint(net[1, 2], net[1, 1]);
			r.AddWaypoint(net[2, 1], net[1, 1]);
			r.AddWaypoint(net[1, 0], net[1, 1]);

			r.AddWaypoint(net[2, 2], fin);

			this.Objects.Add(r);
			var i1 = new MoveTowardWaypointInstruction(g, net[1, 1]);
			var m1 = new MoveInstructionSet(i1);

			g.AddInstructionSet(m1);
			g.AddInstructionSet(new MoveInstructionSet(new MoveTowardPointInstruction(g, new Vector2(500, 10))));

			g.AddInstructionSet(new MoveInstructionSet(new MoveAlongWaypointRouteInstruction(g, r)));

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
