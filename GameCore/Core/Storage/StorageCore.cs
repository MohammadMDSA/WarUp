using GameCore.Core.Utils;
using System;
using System.Collections.Concurrent;
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
using WarUp.Core.Utils;

namespace WarUp.Core.Storage
{
	public class StorageCore : ITickable, ICollectionSynchronizedModifier<FrameworkObject>
	{
		private List<FrameworkObject> Objects;
		private ConcurrentQueue<FrameworkObject> AddObjects;
		private ConcurrentQueue<FrameworkObject> RemoveObjects;

		public StorageCore()
		{
			Reset();
		}

		public void Reset()
		{
			var g = new GreenTile();
			this.Objects = new List<FrameworkObject>();
			this.AddObjects = new ConcurrentQueue<FrameworkObject>();
			this.RemoveObjects = new ConcurrentQueue<FrameworkObject>();

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

		public void AddObject(FrameworkObject @object)
		{
			AddObjects.Enqueue(@object);
		}

		public void RemoveObject(FrameworkObject @object)
		{
			RemoveObjects.Enqueue(@object);
		}

		public IEnumerable<IDrawable> GetDrawables()
		{
			return Objects.ToList();
		}

		public IEnumerable<IUpdatable> GetUpdatables()
		{
			return Objects.ToList();
		}

		public IEnumerable<FrameworkObject> GetFrameworkObjects()
		{
			return Objects.ToList();
		}

		public IEnumerable<GameUtil> GetUtils()
		{
			var result = new List<GameUtil>();


			foreach (var item in Objects.ToList())
			{
				if (item is GameUtil)
					result.Add(item as GameUtil);
			}

			return result;
		}

		public IEnumerable<GameObject> GetGameObjects()
		{
			var result = new List<GameObject>();

			foreach (var item in Objects.ToList())
			{
				if (item is GameObject)
					result.Add(item as GameObject);
			}

			return result;
		}

		public IEnumerable<Waypoint> GetWaypoints()
		{
			var result = new List<Waypoint>();

			foreach (var item in Objects.ToList())
			{
				if (item is Waypoint)
					result.Add(item as Waypoint);
			}

			return result;
		}

		public void Tick()
		{
			foreach (var item in AddObjects)
			{
				Objects.Add(item);
			}
			AddObjects.Clear();

			foreach (var item in RemoveObjects)
			{
				Objects.Remove(item);
			}
			RemoveObjects.Clear();
		}
	}
}
