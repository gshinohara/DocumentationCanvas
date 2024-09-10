using CustomGrip.Sources;
using Grasshopper.GUI.Canvas;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace DocumentationCanvas.TimeLineDashboard
{
    public class DisplayObjectAttributes : WiringObjectAttributes<DisplayTarget>
    {
        public DisplayObjectAttributes(DisplayObject owner) : base(owner)
        {
            DisplayObjectInputGrip grip1 = new DisplayObjectInputGrip(this) { Name = "1" };
            DisplayObjectInputGrip grip2 = new DisplayObjectInputGrip(this) { Name = "2" };
        }

        protected override void Layout()
        {
            base.Layout();

            RectangleF rect = Bounds;
            rect.Size = new Size(400, 500);
            Bounds = rect;

            DisplayObjectInputGrip grip1 = MyInputGrips.FirstOrDefault(g => g.Name == "1") as DisplayObjectInputGrip;
            grip1.Position = new PointF(Bounds.Left + 50, Bounds.Bottom);
            grip1.Direction = new SizeF(0, 50);

            DisplayObjectInputGrip grip2 = MyInputGrips.FirstOrDefault(g => g.Name == "2") as DisplayObjectInputGrip;
            grip2.Position = new PointF(Bounds.Right - 50, Bounds.Bottom);
            grip2.Direction = new SizeF(0, 50);
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
