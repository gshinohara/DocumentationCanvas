using System.Collections.Generic;
using System.Windows.Forms;

namespace DocumentationCanvas.Objects.Layout
{
    internal class FrameLayout : DocumentationObject<AttatchedFrame>
    {
        public List<IDocumentationObject> Items { get; } = new List<IDocumentationObject>();

        public FrameLayout(AttatchedFrame obj) : base(obj)
        {
        }

        protected override void CreateAttributes()
        {
            Attributes = new FrameLayoutAttributes(this);
        }
    }
}
