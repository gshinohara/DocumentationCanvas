using Eto.Forms;

namespace DocumentationCanvas.Objects.Layout
{
    internal class RichText : ContentWithExtension
    {
        public string Html { get; }

        public RichText(FrameLayout obj, string shortDescription, string htmlBody) : base(obj, shortDescription)
        {
            Html = htmlBody;
        }

        protected override void CreateAttributes()
        {
            Attributes = new RichTextAttributes(this);
        }
    }
}