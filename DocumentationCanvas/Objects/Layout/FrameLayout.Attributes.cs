using DocumentationCanvas.Objects.Layout;
using Grasshopper.GUI.Canvas;
using System.Drawing;

namespace DocumentationCanvas.Objects
{
    internal class FrameLayoutAttributes: DocumentationObjectAttributes<FrameLayout>
    {
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
            foreach (Content content in Owner.Items)
                content.Attributes.Render(canvas);
        }
    }
}
