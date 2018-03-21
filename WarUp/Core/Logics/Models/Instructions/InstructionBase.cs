using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarUp.Core.Logics.Models.Instructions
{
	public abstract class InstructionBase
	{
		public GameObject TargetObject { get; }
		public bool Done { get; }
		public bool Started { get; }
	}
}
