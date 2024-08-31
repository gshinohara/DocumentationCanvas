using Eto.Forms;

namespace DocumentationCanvas.Objects.Layout
{
    internal class RichText : ContentWithExtension
    {
        private string m_Html;
        public RichText(FrameLayout obj, string shortDescription, string htmlBody) : base(obj, shortDescription)
        {
            m_Html = htmlBody;
            MouseDown += DisplayHtml;
        }

        private void DisplayHtml(object sender, Canvas_MouseEventArg e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && Attributes.Bounds.Contains(e.CanvasLocation))
            {
                Label label = new Label { Text = ShortDescription };

                WebView webBrowser = new WebView();
                webBrowser.LoadHtml(m_Html);

                DynamicLayout layout = new DynamicLayout
                {
                    Padding = 10,
                    Spacing = new Eto.Drawing.Size(10, 10),
                };
                layout.AddSeparateRow(label);
                layout.AddSeparateRow(webBrowser);

                Form form = new Form
                {
                    Title = "Long Comment",
                    AutoSize = true,
                    Content = layout,
                };
                form.Show();
            }
        }

        protected override void CreateAttributes()
        {
            Attributes = new RichTextAttributes(this);
        }
    }
}