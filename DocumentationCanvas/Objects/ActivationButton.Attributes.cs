using Grasshopper.GUI;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DocumentationCanvas.Objects
{
    internal class AttatchmentAttributes : DocumentationObjectAttributes<Attatchment>
    {
        public override bool IsVisible
        {
            get => base.IsVisible;
            set
            {
                base.IsVisible = value;
                Owner.Frame.Attributes.IsVisible = value;
            }
        }

        public override RectangleF Bounds
        {
            get
            {
                RectangleF objRect = Owner.LinkedObject.Attributes.Bounds;
                RectangleF attatchRect = new RectangleF(objRect.Left, objRect.Top - 15, 10, 10);

                return attatchRect;
            }
        }

        public AttatchmentAttributes(Attatchment owner) : base(owner)
        {
        }

        public override void Render(GH_Canvas canvas)
        {
            Owner.Frame.Attributes.ExpirePreview(canvas);

            GraphicsPath graphicsPath = GH_CapsuleRenderEngine.CreateRoundedRectangle(Bounds, 2);

            canvas.Graphics.FillPath(new SolidBrush(Color.LightGray), graphicsPath);
            canvas.Graphics.DrawPath(new Pen(Color.Black), graphicsPath);

            string text_attatch = Owner.IsOpen ? "-" : "/";
            canvas.Graphics.DrawString(text_attatch, GH_FontServer.Standard, new SolidBrush(Color.DarkSlateGray), Bounds, GH_TextRenderingConstants.CenterCenter);
        }
    }
}
