using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;

namespace WarUp.Utils.Mouse
{
	abstract class BaseMouseFunction : IMouseFunction
	{
		private Mouse Mouse;

		public BaseMouseFunction(Mouse mouse)
		{
			this.Mouse = mouse;
		}

		void IMouseFunction.Moved(UIElement sender, PointerRoutedEventArgs e)
		{
			throw new NotImplementedException();
		}

		void IMouseFunction.PointerPressed(UIElement sender, PointerRoutedEventArgs e)
		{
			throw new NotImplementedException();
		}

		void IMouseFunction.PointerReleased(UIElement sender, PointerRoutedEventArgs e)
		{
			throw new NotImplementedException();
		}

		void IMouseFunction.WheelChanged(UIElement sender, PointerRoutedEventArgs e)
		{
			throw new NotImplementedException();
		}
	}
}
