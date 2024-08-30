using Grasshopper.GUI;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DocumentationCanvas.Objects.Layout
{
    internal class ControlButtonAttributes : DocumentationObjectAttributes<ControlButton>
    {
        public SizeF Size { get; set; } = SizeF.Empty;

        public SizeF RelativeLocation { get; set; } = SizeF.Empty;

        public override RectangleF Bounds
        {
            get
            {
                RectangleF rect = new RectangleF(Owner.LinkedObject.Attributes.Bounds.Location, Size);
                rect.Location += RelativeLocation;
                return rect;
            }
        }

        public ControlButtonAttributes(ControlButton button) : base(button)
        {
        }

        public override void Render(GH_Canvas canvas)
        {
            GraphicsPath graphicsPath = GH_CapsuleRenderEngine.CreateRoundedRectangle(Bounds, 2);

            canvas.Graphics.FillPath(new SolidBrush(Color.FromArgb(200, Color.LightGray)), graphicsPath);
            canvas.Graphics.DrawPath(new Pen(Color.Black), graphicsPath);

            canvas.Graphics.DrawString(Owner.Text, GH_FontServer.Standard, new SolidBrush(Color.DarkSlateGray), Bounds, GH_TextRenderingConstants.CenterCenter);
        }
    }
}
