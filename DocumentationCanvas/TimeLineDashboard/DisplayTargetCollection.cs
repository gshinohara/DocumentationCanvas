using CustomGrip.Targets;
using DocumentationCanvas.Objects;
using Grasshopper.Kernel;
using System.Collections.Generic;
using System.Drawing;

namespace DocumentationCanvas.TimeLineDashboard
{
    internal class DisplayTargetCollection : TargetCollection<DisplayTarget>
    {
        private List<DisplayTarget> m_Targets { get; } = new List<DisplayTarget>();

        protected override IEnumerable<DisplayTarget> Targets
        {
            get
            {
                foreach (DisplayTarget target in m_Targets)
                    yield return target;
            }
        }

        public DisplayTargetCollection(GH_Document document) : base(document)
        {
        }

        public void Add(DisplayTarget target)
        {
            m_Targets.Add(target);
        }

        public void AddRange(IEnumerable<DisplayTarget> targets)
        {
            m_Targets.AddRange(targets);
        }

        public void Remove(DisplayTarget target)
        {
            m_Targets.Remove(target);
        }

        public override DisplayTarget Find(PointF point)
        {
            foreach(DisplayTarget target in this)
            {
                if (target.Owner.IsValid && target.Owner.ActivationButton.IsOpen && target.Owner.Attributes.Bounds.Contains(point))
                    return target;
                else if (target.Owner.IsValid && !target.Owner.ActivationButton.IsOpen && target.Owner.ActivationButton.Attributes.Bounds.Contains(point))
                    return target;
            }
            return null;
        }
    }
}
