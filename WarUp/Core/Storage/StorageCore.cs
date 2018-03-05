﻿using System;
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
		}

		public IEnumerable<IDrawable> GetDrawables()
		{
			return null;
		}

		public IEnumerable<IUpdatable> GetUpdatables()
		{
			return null;
		}
	}
}
