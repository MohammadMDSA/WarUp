using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Shapes;

namespace WarUp.Core.Logics.MapUtils
{
    public class Waypoint
    {
        Vector2 Position { get; set; }

        public Waypoint(Vector2 position)
        {
            this.Position = position;
            
        }
    }
}
