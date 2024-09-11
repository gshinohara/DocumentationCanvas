using Eto.Forms;

namespace DocumentationCanvas.Objects.Layout
{
    internal class RichTextAttributes : ContentWithExtensionAttributes<RichText>
    {
        private string m_Html;

        public RichTextAttributes(RichText owner, string htmlBody) : base(owner)
        {
            m_Html = htmlBody;
        }

        private void DisplayHtml(object sender, Canvas_MouseEventArg e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && Bounds.Contains(e.CanvasLocation))
            {
                Label label = new Label { Text = Owner.ShortDescription };

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
    }
}