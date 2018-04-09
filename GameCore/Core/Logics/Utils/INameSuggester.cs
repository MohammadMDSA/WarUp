using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore.Core.Logics.Utils
{
	interface INameSuggester
	{
		string SuggestName(IEnumerable<string> existings);
		bool RequiresName();
	}
}
