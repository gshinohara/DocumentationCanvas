using Grasshopper.GUI.Canvas;
using System.Drawing;

namespace DocumentationCanvas.Objects.Layout
{
    internal class FrameLayoutAttributes : DocumentationObjectAttributes<FrameLayout>
    {
        public override bool IsVisible
        {
            get => base.IsVisible;
            set
            {
                base.IsVisible = value;

                foreach (IDocumentationObject item in Owner.Items)
                    item.Attributes.IsVisible = value;
            }
        }

        public SizeF Size { get; set; } = SizeF.Empty;

        public SizeF RelativeLocation { get; set; } = SizeF.Empty;

        public override RectangleF Bounds
        {
            get
            {
                RectangleF rect = new RectangleF(Owner.LinkedObject.Attributes.Bounds.Location, Size);
                rect.Location += RelativeLocation;
                return rect;
            }
        }

        public FrameLayoutAttributes(FrameLayout owner) : base(owner)
        {
        }

        public override void Render(GH_Canvas canvas)
        {
            foreach (IDocumentationObject obj in Owner.Items)
                obj.Attributes.ExpirePreview(canvas);
        }
    }
}
