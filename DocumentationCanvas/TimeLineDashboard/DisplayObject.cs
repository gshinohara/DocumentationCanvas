using CustomGrip;
using DocumentationCanvas.Objects;
using System;

namespace DocumentationCanvas.TimeLineDashboard
{
    public class DisplayObject : WiringObject<Attatchment>
    {
        public DisplayObject()
         : base("TimeLine Dashboard", "Dashboard", "Display timelines of connected objects.", "Params", "Util")
        {
        }

        public override Guid ComponentGuid => new Guid("30B7887D-E3EF-42BD-BF4C-5F03BEF1F221");

        public override void CreateAttributes()
        {
            Attributes = new DisplayObjectAttributes(this);
        }
    }
}
