using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Graphics.Display;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

namespace WarUp.GraphicEngine
{
	public class SwapChainManager
	{
		public CanvasSwapChain SwapChain { get; private set; }
		public CanvasDevice Device { get; private set; }
		private CoreWindow Window;

		public SwapChainManager(CoreWindow window, CanvasDevice device)
		{
			Device = device;
			Window = window;
			float currentDpi = DisplayInformation.GetForCurrentView().LogicalDpi;
			SwapChain = CanvasSwapChain.CreateForCoreWindow(device, window, currentDpi);
		}

		public SwapChainManager(CanvasDevice device, Page page)
		{
			SwapChain = new CanvasSwapChain(device, (float)page.DesiredSize.Width, (float)page.DesiredSize.Height, DisplayInformation.GetForCurrentView().LogicalDpi);
		}

		public void EnsureMatchesWindow()
		{
			var bounds = Window.Bounds;
			Size windowSize = new Size(bounds.Width, bounds.Height);
			float dpi = DisplayInformation.GetForCurrentView().LogicalDpi;

			if (!SizeEqualsWithTolerance(windowSize, SwapChain.Size) || dpi != SwapChain.Dpi)
			{
				SwapChain.ResizeBuffers((float)windowSize.Width, (float)windowSize.Height, dpi);
			}
		}

		public void EnsureCurrentBufferMatchesWindow(CanvasRenderTarget[] accumulationBuffers, int currentBuffer)
		{
			var bounds = Window.Bounds;

			Size windowSize = new Size(bounds.Width, bounds.Height);
			float dpi = DisplayInformation.GetForCurrentView().LogicalDpi;

			var buffer = accumulationBuffers[currentBuffer];

			if (buffer == null || !(SwapChainManager.SizeEqualsWithTolerance(buffer.Size, windowSize)) || buffer.Dpi != dpi)
			{
				if (buffer != null)
				{
					buffer.Dispose();
				}
				var wid = (float)windowSize.Width;
				var hei = (float)windowSize.Height;
				buffer = new CanvasRenderTarget(Device, wid, hei, dpi);
				accumulationBuffers[currentBuffer] = buffer;
			}
		}

		static public bool SizeEqualsWithTolerance(Size sizeA, Size sizeB)
		{
			const float tolerance = 0.1f;

			if (Math.Abs(sizeA.Width - sizeB.Width) > tolerance)
			{
				return false;
			}

			if (Math.Abs(sizeA.Height - sizeB.Height) > tolerance)
			{
				return false;
			}

			return true;
		}

	}
}
