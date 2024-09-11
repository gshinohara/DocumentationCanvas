using Eto.Drawing;
using Eto.Forms;

namespace DocumentationCanvas.Objects.Layout
{
    internal class DisplayCapture : ContentWithExtension
    {
        public Image Image { get; }

        public DisplayCapture(FrameLayout obj, string shortDescription, Image image) : base(obj, shortDescription)
        {
            Image = image;
        }

        protected override void CreateAttributes()
        {
            Attributes = new DisplayCaptureAttributes(this);
        }
    }
}
