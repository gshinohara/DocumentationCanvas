using Eto.Forms;
using Grasshopper;
using System;

namespace DocumentationCanvas.Objects.Layout.InputForm
{
    internal class CaptureForm : Form
    {
        public CaptureForm(AttatchedFrame frame)
        {
            Title = "Capture";

            Padding = 15;
            AutoSize = true;
            Topmost = true;
            Resizable = false;

            Location = (Eto.Drawing.Point)Screen.Bounds.Center;

            DynamicLayout layout = new DynamicLayout
            {
                Padding = 5,
                Spacing = new Eto.Drawing.Size(5, 15),
            };

            TextBox textBox = new TextBox { PlaceholderText = "Write a short description." };

            Eto.Drawing.Image image = null;

            EventHandler<EventArgs> buttonEvent = (sender, e) =>
            {
                if ((bool)(sender as Button).Tag)
                {
                    DisplayCapture displayCapture = new DisplayCapture(frame.TimeLine, textBox.Text, image);
                    frame.TimeLine.Items.Add(displayCapture);
                    Instances.ActiveCanvas.Refresh();
                }

                Close();
            };

            Button button_OK = new Button(buttonEvent)
            {
                Text = "OK",
                Tag = true,
                Enabled = false,
            };

            Button button_Cancel = new Button(buttonEvent)
            {
                Text = "Cancel",
                Tag = false,
            };

            EventHandler<EventArgs> capture = (sender, e) =>
            {
                if (Clipboard.Instance.ContainsImage)
                {
                    image = Clipboard.Instance.Image;
                    button_OK.Enabled = true;
                }
            };
            Button button_Capture = new Button(capture)
            {
                Text = "Get an image from your clipboad",
            };

            layout.AddSeparateRow(textBox);
            layout.AddSeparateRow(button_Capture);
            layout.AddSeparateRow(null, button_OK, null, button_Cancel, null);

            Content = layout;
        }
    }
}