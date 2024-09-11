using CustomGrip.Grips;
using CustomGrip.Targets;
using DocumentationCanvas.Objects;
using System.Drawing;

namespace DocumentationCanvas.TimeLineDashboard
{
    public class DisplayTarget : TargetObject<AttatchmentObject, DisplayTargetGrip>
    {
        public DisplayTarget(AttatchmentObject owner) : base(owner)
        {
        }

        public override RectangleF GetBounds()
        {
            return Owner.AttatchedFrame.Attributes.Bounds;
        }

        public override Grip GetGrip()
        {
            return new DisplayTargetGrip(this);
        }

        public override bool IsEqualOwner(ITargetObject other)
        {
            if (other is DisplayTarget displayTarget)
                return displayTarget.Owner.LinkedObject == Owner.LinkedObject;
            else
                return false;
        }
    }
}
