using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore.Core.Utils
{
	interface ICollectionSynchronizedModifier<T>
	{
		void AddObject(T @object);
		void RemoveObject(T @object);
	}
}
