using Eto.Drawing;
using Eto.Forms;

namespace DocumentationCanvas.Objects.Layout
{
    internal class DisplayCapture : ContentWithExtension
    {
        private Image m_Image;

        public DisplayCapture(FrameLayout obj, string shortDescription, Image image) : base(obj, shortDescription)
        {
            m_Image = image;
        }

        protected override void CreateAttributes()
        {
            Attributes = new DisplayCaptureAttributes(this, m_Image);
        }
    }
}
