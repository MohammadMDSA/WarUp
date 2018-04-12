using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarUp.Core.Utils
{
	interface IClonable<T>
	{
		T Clone();
	}
}
