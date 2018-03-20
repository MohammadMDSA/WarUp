using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Graphics.Canvas;
using WarUp.Core.Graphics;

namespace WarUp.Core.Logics.Models
{
    public abstract class FrameworkObject : IUpdatable, IDrawable
    {
        public abstract void Draw(CanvasDrawingSession session);
        public abstract void Update();
    }
}
