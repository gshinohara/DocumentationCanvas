using Grasshopper.GUI.Canvas;
using System.Drawing;

namespace DocumentationCanvas.Objects
{
    internal class AttatchmentObjectAttributes : DocumentationObjectAttributes<AttatchmentObject>
    {
        public override RectangleF Bounds
        {
            get
            {
                RectangleF objRect = Owner.LinkedObject.Attributes.Bounds;

                RectangleF rect = new RectangleF { Width = 260, Height = 155 };
                rect.Location = new PointF(objRect.Left, objRect.Top - rect.Height - 5);

                return rect;
            }
        }

        public AttatchmentObjectAttributes(AttatchmentObject attatchmentObject) : base(attatchmentObject)
        {
        }

        public override bool IsPickRegion(PointF point)
        {
            return Bounds.Contains(point);
        }

        protected override void Render(GH_Canvas canvas)
        {
        }
    }
}
