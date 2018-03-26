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
		public Size RenderSize { get; set; }
		public Size DisplaySize { get; private set; }
		public float DPI { get; private set; }
		private bool UseCore { get; }
		public Rect SelectionRect { get; set; }

		public SwapChainManager(CoreWindow window, CanvasDevice device)
		{
			ConstructorHelper(device);
			SwapChain = CanvasSwapChain.CreateForCoreWindow(device, window, DPI);
			UseCore = true;
		}

		public SwapChainManager(CanvasDevice device)
		{
			ConstructorHelper(device);
			SwapChain = new CanvasSwapChain(device, (float)DisplaySize.Width, (float)DisplaySize.Height, DisplayInformation.GetForCurrentView().LogicalDpi);
			UseCore = false;
		}

		private void ConstructorHelper(CanvasDevice device)
		{
			this.Device = device;

			var info = DisplayInformation.GetForCurrentView();
			DPI = info.LogicalDpi;
			DisplaySize = new Size(info.ScreenWidthInRawPixels, info.ScreenHeightInRawPixels);

			SelectionRect = new Rect();
		}

		public void EnsureMatchesWindow()
		{
			if (UseCore)
			{
				var window = CoreWindow.GetForCurrentThread();
				var bound = window.Bounds;
				RenderSize = new Size(bound.Width, bound.Height);

			}
			if (!SizeEqualsWithTolerance(RenderSize, SwapChain.Size) || DPI != SwapChain.Dpi)
			{
				SwapChain.ResizeBuffers((float)RenderSize.Width, (float)RenderSize.Height, DPI);
			}
		}

		public void EnsureCurrentBufferMatchesWindow(CanvasRenderTarget[] accumulationBuffers, int currentBuffer)
		{
			if (UseCore)
			{
				var window = CoreWindow.GetForCurrentThread();
				var bound = window.Bounds;
				RenderSize = new Size(bound.Width, bound.Height);

			}

			var buffer = accumulationBuffers[currentBuffer];

			if (buffer == null || !(SwapChainManager.SizeEqualsWithTolerance(buffer.Size, RenderSize)) || buffer.Dpi != DPI)
			{
				if (buffer != null)
				{
					buffer.Dispose();
				}
				var wid = (float)RenderSize.Width;
				var hei = (float)RenderSize.Height;
				buffer = new CanvasRenderTarget(Device, wid, hei, DPI);
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
