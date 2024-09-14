using System;
using WireEventImplementor;

namespace DocumentationCanvas.Objects.Layout
{
    internal class WireEventContent : Content
    {
        public WireStatus WireStatus { get; }
        public WireEventContent(FrameLayout timeline, WireStatus wireStatus)
            : base(timeline, "Wire from")
        {
            WireStatus = wireStatus;
        }
        protected override void CreateAttributes()
        {
            Attributes = new WireEventContentAttributes(this);
        }
    }
}