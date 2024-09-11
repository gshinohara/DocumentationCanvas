using CustomGrip.Sources;
using Grasshopper.GUI.Canvas;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DocumentationCanvas.TimeLineDashboard
{
    public class DisplayObjectAttributes : WiringObjectAttributes<DisplayTarget>
    {
        public DisplayObjectAttributes(DisplayObject owner) : base(owner)
        {
            RectangleF rect = Bounds;
            rect.Size = new Size(400, 500);
            Bounds = rect;

            new DisplayObjectInputGrip(this) { Direction = new SizeF(0, 50) };
        }

        public void SetGripPosition()
        {
            for (int i = 0; i < MyInputGrips.Count; i++)
            {
                PointF p = MyInputGrips[i].Position;
                p.X = Bounds.Left + Bounds.Width * i / MyInputGrips.Count + Bounds.Width / MyInputGrips.Count / 2;
                p.Y = Bounds.Bottom;
                MyInputGrips[i].Position = p;
            }
        }


        protected override void Layout()
        {
            base.Layout();

            SetGripPosition();
        }

        protected override void Render(GH_Canvas canvas, Graphics graphics, GH_CanvasChannel channel)
        {
            base.Render(canvas, graphics, channel);

            switch (channel)
            {
                case GH_CanvasChannel.Objects:
                    GraphicsPath graphicsPath = GH_CapsuleRenderEngine.CreateRoundedRectangle(Bounds, 2);
                    graphics.DrawPath(new Pen(Color.Black), graphicsPath);
                    break;
            }
        }
    }
}
