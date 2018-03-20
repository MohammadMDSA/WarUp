using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Composition;

namespace WarUp.Core.Logics.Models.Ability
{
	interface IMovable : IAbility
	{
        void Move();
	}
}
