using CustomGrip.Sources;
using Grasshopper.Kernel;
using System;
using System.Linq;

namespace DocumentationCanvas.TimeLineDashboard
{
    public class DisplayObject : WiringObject<DisplayTarget>
    {
        public DisplayObject()
         : base("TimeLine Dashboard", "Dashboard", "Display timelines of connected objects.", "Params", "Util")
        {
        }

        public override void AddedToDocument(GH_Document document)
        {
            base.AddedToDocument(document);

            TargetCollection = new DisplayTargetCollection(document);
        }

        protected override void Document_ObjectsDeleted(object sender, GH_DocObjectEventArgs e)
        {
            foreach (var grip in (Attributes as DisplayObjectAttributes).MyInputGrips)
            {
                DisplayTarget target = grip.TargetObjects.FirstOrDefault(t => e.Objects.Contains(t.Owner.LinkedObject));
                grip.TargetObjects.Remove(target);
            }
        }

        public override Guid ComponentGuid => new Guid("30B7887D-E3EF-42BD-BF4C-5F03BEF1F221");

        public override void CreateAttributes()
        {
            Attributes = new DisplayObjectAttributes(this);
        }
    }
}
