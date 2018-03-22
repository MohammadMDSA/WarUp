using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarUp.Core.Logics.Models.Instructions
{
	public abstract class InstructionBase : ITickable
	{
		public readonly GameObject TargetObject;
		public bool Done { get; protected set; }
		public bool Started { get; protected set; }

		public InstructionBase(GameObject gameObject)
		{
			this.TargetObject = gameObject;
			Done = Started = false;
		}

		public virtual bool Start()
		{
			Started = true;
			return true;
		}

		public abstract void Tick();

	}
}
