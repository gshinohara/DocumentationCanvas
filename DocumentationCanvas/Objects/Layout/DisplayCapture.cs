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
            MouseDown += DisplayImage;
        }

        private void DisplayImage(object sender, Canvas_MouseEventArg e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && Attributes.Bounds.Contains(e.CanvasLocation))
            {
                Label label = new Label { Text = ShortDescription };

                ImageView imageView = new ImageView { Image = m_Image };

                DynamicLayout layout = new DynamicLayout
                {
                    Padding = 10,
                    Spacing = new Size(10, 10),
                };
                layout.AddSeparateRow(label);
                layout.AddSeparateRow(imageView);

                Form form = new Form
                {
                    Title = "Display Capture",
                    AutoSize = true,
                    Content = layout,
                };
                form.Show();
            }
        }

        protected override void CreateAttributes()
        {
            Attributes = new DisplayCaptureAttributes(this);
        }
    }
}
