using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarUp.Core.Graphics;
using WarUp.Core.Logics;
using WarUp.Core.Logics.Models;

namespace WarUp.Core.Storage
{
	class StorageCore
	{
		public List<IFrameworkObject> Objects;

		public StorageCore()
		{
			this.Objects = new List<IFrameworkObject>();
			this.Objects.Add(new GreenTile());
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
