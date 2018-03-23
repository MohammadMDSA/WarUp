using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;

namespace WarUp.Utils.Mouse
{
    interface IMouseFunction
    {
		void Moved(UIElement sender, PointerRoutedEventArgs e);
		void WheelChanged(UIElement sender, PointerRoutedEventArgs e);
		void PointerPressed(UIElement sender, PointerRoutedEventArgs e);
		void PointerReleased(UIElement sender, PointerRoutedEventArgs e);

    }
}
