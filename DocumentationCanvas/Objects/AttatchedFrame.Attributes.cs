using Grasshopper.GUI.Canvas;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DocumentationCanvas.Objects
{
    internal class AttatchedFrameAttributes : DocumentationObjectAttributes<AttatchedFrame>
    {
        public override RectangleF Bounds
        {
            get
            {
                RectangleF objRect = Owner.LinkedObject.Attributes.Bounds;
                RectangleF attatchRect = new RectangleF(objRect.Left, objRect.Top - 150, 260, 140);

                return attatchRect;
            }
        }

        public AttatchedFrameAttributes(AttatchedFrame owner) : base(owner)
        {
        }

        public override void Render(GH_Canvas canvas)
        {
            if (!Owner.LinkedObject.IsOpen)
                return;

            GraphicsPath graphicsPath = GH_CapsuleRenderEngine.CreateRoundedRectangle(Bounds, 2);

            canvas.Graphics.FillPath(new SolidBrush(Color.FromArgb(200, Color.LightGray)), graphicsPath);
            canvas.Graphics.DrawPath(new Pen(Color.Black), graphicsPath);

            foreach (IDocumentationObject item in Owner.TimeLine.Items)
                item.Attributes.Render(canvas);

            foreach (IDocumentationObject item in Owner.ControlPanel.Items)
                item.Attributes.Render(canvas);
        }
    }
}
