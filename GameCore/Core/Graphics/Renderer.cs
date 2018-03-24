using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using WarUp.GraphicEngine;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Graphics.Display;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;

namespace WarUp.Core.Graphics
{
	class Renderer
	{
		private DateTime LastRender;
		private Color AccentColor;
		
		private SwapChainManager SwapChainManager;
		private int count = 0;
		public float fps { get; private set; }


		CanvasRenderTarget[] AccumulationBuffers = new CanvasRenderTarget[2];
		private int CurrentBuffer;

		CanvasRenderTarget FrontBuffer => AccumulationBuffers[CurrentBuffer];
		CanvasRenderTarget BackBuffer => AccumulationBuffers[(CurrentBuffer + 1) % 2];

		public Renderer(SwapChainManager swapChainManager)
		{
			fps = 0;

			CurrentBuffer = 0;

			this.SwapChainManager = swapChainManager;

			//this.Window = window;
			
			//this.SwapChainManager = new SwapChainManager(device: Device, window: window);

			LastRender = DateTime.Now;
		}

		public void Render(IEnumerable<IDrawable> drawables)
		{
			count = (count + 1) % 20;

			long deltaTime = (DateTime.Now - LastRender).Milliseconds;

			LastRender = DateTime.Now;

			if (count == 0)
			{
				fps = 1000 / (deltaTime * 1.0f);

			}

			SwapChainManager.EnsureMatchesWindow();

			SwapAccumulationBuffers();
			SwapChainManager.EnsureCurrentBufferMatchesWindow(AccumulationBuffers, CurrentBuffer);

			using (var ds = FrontBuffer.CreateDrawingSession())
			{
				ds.Clear(Colors.Black);

				foreach (var item in drawables)
				{
					if (item.ShouldBeDrawn())
						item.Draw(ds);
				}

				ds.FillRectangle(SwapChainManager.SelectionRect, Color.FromArgb(127, 0, 127, 255));
				ds.DrawRectangle(SwapChainManager.SelectionRect, Colors.Blue);
			}

			using (var ds = SwapChainManager.SwapChain.CreateDrawingSession(Colors.CornflowerBlue))
			{
				ds.DrawImage(FrontBuffer);
			}

			SwapChainManager.SwapChain.Present();
		}

		private void SwapAccumulationBuffers()
		{
			CurrentBuffer = (CurrentBuffer + 1) % 2;
		}

		public void Trim()
		{
			SwapChainManager.Device.Trim();
		}
	}
}
