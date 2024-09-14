using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using System.Drawing;

namespace DocumentationCanvas.Objects.Layout
{
    internal class WireEventContentAttributes : ContentAttributes<WireEventContent>
    {
        public WireEventContentAttributes(WireEventContent content) : base(content)
        {
        }

        protected override void Render(GH_Canvas canvas)
        {
            base.Render(canvas);

            RectangleF sourceIconBounds = new RectangleF { Width = Bounds.Height, Height = Bounds.Height, Location = Bounds.Location };
            sourceIconBounds.X += Bounds.Width - 80;
            sourceIconBounds.Inflate(-1, -1);

            canvas.Graphics.DrawImage(Owner.WireStatus.PreviousSideParam.Attributes.GetTopLevel.DocObject.Icon_24x24, sourceIconBounds);
        }
    }
}
