using System.Collections.Generic;
using System.Windows.Forms;

namespace DocumentationCanvas.Objects.Layout
{
    public class FrameLayout : DocumentationObject<AttatchedFrame>
    {
        public List<IDocumentationObject> Items { get; } = new List<IDocumentationObject>();

        public FrameLayout(AttatchedFrame obj) : base(obj)
        {
            PostValidityChanged += () =>
            {
                foreach (IDocumentationObject item in Items)
                    item.IsValid = IsValid;
            };

            Attributes.MouseUp += (sender, e) =>
            {
                foreach (IDocumentationObject item in Items)
                    item.Attributes.OnMouseUp(e);
            };

            Attributes.PostPaint += canvas =>
            {
                foreach (IDocumentationObject item in Items)
                    item.Attributes.ExpirePreview(canvas);
            };
        }

        protected override void CreateAttributes()
        {
            Attributes = new FrameLayoutAttributes(this);
        }
    }
}
