using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using WarUp.Core.Graphics.SwapChain;
using Windows.Foundation;
using Windows.Graphics.Display;
using Windows.UI;
using Windows.UI.Core;

namespace WarUp.Core.Graphics
{
	class Renderer
	{
		private DateTime LastRender;

		private CoreWindow Window;
		private CanvasDevice Device;
		private SwapChainManager SwapChainManager;
		private int count = 0;
		public float fps { get; private set; }


		CanvasRenderTarget[] AccumulationBuffers = new CanvasRenderTarget[2];
		private int CurrentBuffer;

		CanvasRenderTarget FrontBuffer => AccumulationBuffers[CurrentBuffer];
		CanvasRenderTarget BackBuffer => AccumulationBuffers[(CurrentBuffer + 1) % 2];

		public Renderer(CoreWindow window)
		{
			fps = 0;

			CurrentBuffer = 0;

			this.Window = window;

			this.Device = new CanvasDevice();
			this.SwapChainManager = new SwapChainManager(device: Device, window: window);

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

			SwapChainManager.EnsureMatchesWindow(Window);

			SwapAccumulationBuffers();
			EnsureCurrentBufferMatchesWindow();

			using (var ds = FrontBuffer.CreateDrawingSession())
			{
				ds.Clear(Colors.Black);

				foreach (var item in drawables)
				{
					if (item.ShouldBeDrawn())
						item.Draw(ds);
				}
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

		private void EnsureCurrentBufferMatchesWindow()
		{
			var bounds = Window.Bounds;

			Size windowSize = new Size(bounds.Width, bounds.Height);
			float dpi = DisplayInformation.GetForCurrentView().LogicalDpi;

			var buffer = AccumulationBuffers[CurrentBuffer];

			if (buffer == null || !(SwapChainManager.SizeEqualsWithTolerance(buffer.Size, windowSize)) || buffer.Dpi != dpi)
			{
				if (buffer != null)
				{
					buffer.Dispose();
				}

				buffer = new CanvasRenderTarget(Device, (float)windowSize.Width, (float)windowSize.Height, dpi);
				AccumulationBuffers[CurrentBuffer] = buffer;
			}
		}

		public void Trim()
		{
			Device.Trim();
		}
	}
}
