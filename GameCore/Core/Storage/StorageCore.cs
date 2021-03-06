﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using WarUp.Core.Graphics;
using WarUp.Core.Logics;
using WarUp.Core.Logics.MapUtils;
using WarUp.Core.Logics.Models;
using WarUp.Core.Logics.Models.Instructions.Move;
using WarUp.Core.Logics.Utils;
using WarUp.Core.Utils;

namespace WarUp.Core.Storage
{
	[Serializable]
	public class StorageCore : IClonable<StorageCore>
	{
		private HashSet<FrameworkObject> Objects;

		public StorageCore()
		{
			Reset();
		}

		public void Reset()
		{
			var g = new GreenTile();
			this.Objects = new HashSet<FrameworkObject>();

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
			if (@object.Name == null || @object.Name == string.Empty)
			{
				@object.SetName(@object.SuggestName(this.GetNamesList()));
			}
			Objects.Add(@object);
		}

		public void AddObject(IEnumerable<FrameworkObject> objects)
		{
			foreach (var item in objects)
			{
				AddObject(item);
			}
		}

		public void RemoveObject(FrameworkObject @object)
		{
			Objects.Add(@object);
		}

		public void RemoveObject(IEnumerable<FrameworkObject> objects)
		{
			foreach (var item in objects)
			{
				RemoveObject(item);
			}
		}

		public void RemoveAll()
		{
			Objects.Clear();
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


			foreach (var item in Objects)
			{
				if (item is GameUtil)
					result.Add(item as GameUtil);
			}

			return result;
		}

		public IEnumerable<GameObject> GetGameObjects()
		{
			var result = new List<GameObject>();

			foreach (var item in Objects)
			{
				if (item is GameObject)
					result.Add(item as GameObject);
			}

			return result;
		}

		public IEnumerable<Waypoint> GetWaypoints()
		{
			var result = new List<Waypoint>();

			foreach (var item in Objects)
			{
				if (item is Waypoint)
					result.Add(item as Waypoint);
			}

			return result;
		}

		public IEnumerable<string> GetNamesList()
		{
			var result = new List<string>();
			foreach (var item in Objects)
			{
				result.Add(item.Name);
			}
			return result;
		}

		public StorageCore Clone()
		{
			using (var memoryStream = new MemoryStream())
			{
				var formatter = new BinaryFormatter();
				formatter.Serialize(memoryStream, this);
				memoryStream.Position = 0;

				return (StorageCore)formatter.Deserialize(memoryStream);
			}
		}
	}
}
