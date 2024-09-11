using CustomGrip.Targets;
using DocumentationCanvas.Objects;
using Grasshopper.Kernel;
using System.Collections.Generic;
using System.Drawing;

namespace DocumentationCanvas.TimeLineDashboard
{
    internal class DisplayTargetCollection : TargetCollection<DisplayTarget>
    {
        protected override IEnumerable<DisplayTarget> Targets
        {
            get
            {
                foreach (IGH_DocumentObject obj in Document.Objects)
                    yield return new DisplayTarget(new AttatchmentObject(obj));
            }
        }

        public DisplayTargetCollection(GH_Document document) : base(document)
        {
        }

        public override DisplayTarget Find(PointF point)
        {
            foreach(DisplayTarget target in this)
            {
                if (target.Owner.AttatchedFrame.Attributes.Bounds.Contains(point)) 
                    return target;
            }
            return null;
        }
    }
}
