using Eto.Forms;

namespace DocumentationCanvas.Objects.Layout
{
    internal class RichText : ContentWithExtension
    {
        private string m_Html;

        public RichText(FrameLayout obj, string shortDescription, string htmlBody) : base(obj, shortDescription)
        {
            m_Html = htmlBody;
        }

        protected override void CreateAttributes()
        {
            Attributes = new RichTextAttributes(this, m_Html);
        }
    }
}