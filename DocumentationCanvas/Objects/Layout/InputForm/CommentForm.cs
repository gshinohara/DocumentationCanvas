using Eto.Forms;
using Grasshopper;
using System;

namespace DocumentationCanvas.Objects.Layout.InputForm
{
    internal class CommentForm : Form
    {
        public CommentForm(AttatchedFrame frame)
        {
            Title = "Input comment";

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

            TextBox textBox = new TextBox { PlaceholderText = "Write your comment." };

            EventHandler<EventArgs> buttonEvent = (sender, e) =>
            {
                if ((bool)(sender as Button).Tag)
                {
                    Note note = new Note(frame.TimeLine, textBox.Text) { IsValid = frame.IsValid };
                    frame.TimeLine.Items.Add(note);
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
            layout.AddSeparateRow(button_OK, null, button_Cancel);

            Content = layout;
        }
    }
}
