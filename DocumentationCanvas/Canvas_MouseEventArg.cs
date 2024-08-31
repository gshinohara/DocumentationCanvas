using Grasshopper.GUI.Canvas;
using System.Drawing;
using System.Windows.Forms;

namespace DocumentationCanvas
{
    internal class Canvas_MouseEventArg : MouseEventArgs
    {
        public GH_Canvas Canvas { get; set; }

        public PointF CanvasLocation => Canvas.Viewport.UnprojectPoint(Location);

        public Canvas_MouseEventArg(MouseButtons button, int clicks, int x, int y, int delta, GH_Canvas canvas) : base(button, clicks, x, y, delta)
        {
            Canvas = canvas;
        }

        public Canvas_MouseEventArg(MouseEventArgs arg, GH_Canvas canvas) : base(arg.Button, arg.Clicks, arg.X, arg.Y, arg.Delta)
        {
            Canvas = canvas;
        }
    }

}