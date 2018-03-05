using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarUp.Core.Graphics;
using Windows.UI.Core;

namespace WarUp.Core
{
	class MainCore : ITickable
	{
		private Renderer Renderer;

		public MainCore(CoreWindow window)
		{
			Renderer = new Renderer(window);
		}

		public void Tick()
		{
			Renderer.Render();
		}

		public void Suspend()
		{
			Renderer.Trim();
		}
		
	}
}
