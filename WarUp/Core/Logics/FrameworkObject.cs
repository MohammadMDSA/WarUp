using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarUp.Core.Graphics;

namespace WarUp.Core.Logics.Models
{
	public abstract class FrameworkObject : IUpdatable
	{
        public string Name { get; set; }

        public abstract void Update();
    }
}
