using Grasshopper.GUI;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using System.Drawing;

namespace DocumentationCanvas.Objects.Layout
{
    internal abstract class ContentWithExtensionAttributes<T> : ContentAttributes<T> where T : ContentWithExtension
    {
        public ContentWithExtensionAttributes(T owner) :base(owner)
        {
        }

        public override void Render(GH_Canvas canvas)
        {
            base.Render(canvas);

            canvas.Graphics.DrawString("expand", GH_FontServer.StandardBold, new SolidBrush(Color.Black), new PointF(Bounds.Right - Bounds.Height / 2, Bounds.Top + Bounds.Height / 2), GH_TextRenderingConstants.FarCenter);
        }
    }
}