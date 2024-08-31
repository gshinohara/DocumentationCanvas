using Eto.Forms;
using Grasshopper;
using System;

namespace DocumentationCanvas.Objects.Layout.InputForm
{
    internal class RichTextForm : Form
    {
        public RichTextForm(AttatchedFrame frame)
        {
            Title = "Long Comment";

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

            RichTextArea richText = new RichTextArea
            {
                Size = new Eto.Drawing.Size(300,100),
            };

            EventHandler<EventArgs> buttonEvent = (sender, e) =>
            {
                if ((bool)(sender as Button).Tag)
                {
                    frame.TimeLine.Items.Add(new Note(frame.TimeLine, textBox.Text));
                    Instances.ActiveCanvas.Refresh();
                }

                Close();
            };

            Button button_OK = new Button(buttonEvent)
            {
                Text = "OK",
                Tag = true,
            };

            Button button_Cancel = new Button(buttonEvent)
            {
                Text = "Cancel",
                Tag = false,
            };

            layout.AddSeparateRow(textBox);
            layout.AddSeparateRow(richText);
            layout.AddSeparateRow(null,button_OK, null, button_Cancel,null);

            Content = layout;
        }
    }
}
