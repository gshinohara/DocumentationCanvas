using CustomGrip.Grips;
using System.Drawing;

namespace DocumentationCanvas.TimeLineDashboard
{
    public class DisplayTargetGrip : Grip
    {
        public DisplayTargetGrip(DisplayTarget target) : base(180f, 180f)
        {
            RectangleF ownerRect = target.Owner.Frame.Attributes.Bounds;
            Position = new PointF((ownerRect.Left + ownerRect.Right) / 2, ownerRect.Top);

            Direction = new SizeF(0, -50);
        }
    }
}
