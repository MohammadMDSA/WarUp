using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore.Core.Utils
{
	interface IClonable<T>
	{
		T Clone();
	}
}
