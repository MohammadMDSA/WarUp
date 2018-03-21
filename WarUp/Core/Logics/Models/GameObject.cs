using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using WarUp.Core.Logics.Models.Instructions;

namespace WarUp.Core.Logics.Models
{
    public abstract class GameObject : FrameworkObject
    {
        public Vector2 Position { get; set; }

		public Queue<InstructionSet> Instructions { get; }
    }
}
