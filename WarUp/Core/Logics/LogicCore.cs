using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarUp.Core.Storage;

namespace WarUp.Core.Logics
{
	class LogicCore : ITickable
	{
		StorageCore Storage;

		public LogicCore(StorageCore storage)
		{
			this.Storage = storage;
		}

		public void Tick()
		{
			var updatables = Storage.GetUpdatables();

			foreach (var item in updatables)
			{
				item.Update();
			}
		}
	}
}
