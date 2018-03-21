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

		public void AddInstruction(InstructionSet instructionSet)
		{
			for (int i = Instructions.Count - 1; instructionSet.CanOverride(Instructions.ElementAt(i)); i--)
			{
				Instructions.ElementAt(i).Enabled = false;
			}

			Instructions.Enqueue(instructionSet);
		}
    }
}
