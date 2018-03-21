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
		public Vector2 Position { get; protected set; }

		public Queue<InstructionSet> Instructions { get; }

		public GameObject()
		{
			Position = new Vector2(0f);
			Instructions = new Queue<InstructionSet>();
		}

		public void SetPosition(float x, float y)
		{
			SetPosition(new Vector2(x, y));
		}

		public void SetPosition(Vector2 position)
		{
			this.Position = position;
		}

		public void AddInstructionSet(InstructionSet instructionSet)
		{
			for (int i = Instructions.Count - 1; i >= 0 && instructionSet.CanOverride(Instructions.ElementAt(i)); i--)
			{
				Instructions.ElementAt(i).Enabled = false;
			}

			Instructions.Enqueue(instructionSet);
		}

		protected bool GetFirstEnableInstruction(out InstructionSet instruction)
		{

			InstructionSet current;
			instruction = null;
			if (!Instructions.TryPeek(out current))
				return false;
			while (!current.Enabled)
			{
				Instructions.Dequeue();
				if (!Instructions.TryPeek(out current))
					return false;
			}

			instruction = current;
			return true;
		}
	}
}
