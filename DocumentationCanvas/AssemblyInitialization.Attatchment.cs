using Grasshopper.GUI;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Special;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DocumentationCanvas
{
    public partial class AssemblyInitialization : GH_AssemblyPriority
    {
        private void AttatchmentSetUp(GH_Canvas canvas)
        {
            canvas.CanvasPostPaintObjects += DrawAttatchment;
        }

        private bool IsApplyToObject(IGH_DocumentObject obj)
        {
            switch (obj)
            {
                case GH_Scribble _:
                case GH_Markup _:
                    return false;
            }
            return true;
        }

        private RectangleF CreateAttachmentRectangle(IGH_DocumentObject obj)
        {
            RectangleF objRect = obj.Attributes.Bounds;
            RectangleF attatchRect = new RectangleF(objRect.Left, objRect.Top - 15, 10, 10);

            return attatchRect;
        }

        private void DrawAttatchment(GH_Canvas sender)
        {
            if (sender.IsDocument)
            {
                foreach (IGH_DocumentObject obj in sender.Document.Objects)
                {
                    if (!IsApplyToObject(obj))
                        continue;

                    RectangleF rectangle = CreateAttachmentRectangle(obj);

                    GraphicsPath graphicsPath = GH_CapsuleRenderEngine.CreateRoundedRectangle(rectangle, 2);

                    sender.Graphics.FillPath(new SolidBrush(Color.LightGray), graphicsPath);
                    sender.Graphics.DrawPath(new Pen(Color.Black), graphicsPath);

                    string text_attatch = "-";
                    sender.Graphics.DrawString(text_attatch, GH_FontServer.Standard, new SolidBrush(Color.DarkSlateGray), rectangle, GH_TextRenderingConstants.CenterCenter);
                }
            }
        }
    }
}
