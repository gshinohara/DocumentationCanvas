using Eto.Drawing;
using Eto.Forms;

namespace DocumentationCanvas.Objects.Layout
{
    internal class DisplayCaptureAttributes : ContentWithExtensionAttributes<DisplayCapture>
    {
        private Image m_Image;

        public DisplayCaptureAttributes(DisplayCapture owner, Image image) : base(owner)
        {
            m_Image = image;
        }

        private void DisplayImage(object sender, Canvas_MouseEventArg e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && Bounds.Contains(e.CanvasLocation))
            {
                Label label = new Label { Text = Owner.ShortDescription };

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
    }
}
