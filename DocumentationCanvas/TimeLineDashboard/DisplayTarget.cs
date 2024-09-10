using CustomGrip.Grips;
using CustomGrip.Targets;
using DocumentationCanvas.Objects;
using System;
using System.Drawing;

namespace DocumentationCanvas.TimeLineDashboard
{
    public class DisplayTarget : TargetObject<Attatchment, DisplayTargetGrip>
    {
        public DisplayTarget(Attatchment owner) : base(owner)
        {
        }

        public override RectangleF GetBounds()
        {
            return Owner.Frame.Attributes.Bounds;
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
