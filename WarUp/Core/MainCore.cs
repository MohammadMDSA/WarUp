using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarUp.Core.Graphics;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

namespace WarUp.Core
{
	class MainCore : ITickable
	{
		public static MainCore Core { get; private set; }
		public Page ActivePage { get; internal set; }

		public IRenderable RenderDevice { get; internal set; }
		public MainCore()
		{
			MainCore.Core = this;
		}

		public void Tick()
		{
			RenderDevice.Render();
		}

		public void Suspend()
		{
		}
		
	}
}
