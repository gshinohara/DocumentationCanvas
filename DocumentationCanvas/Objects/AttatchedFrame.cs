using DocumentationCanvas.Objects.Layout;
using DocumentationCanvas.Objects.Layout.InputForm;
using Grasshopper.Kernel;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DocumentationCanvas.Objects
{
    internal class AttatchedFrame : DocumentationObject<Attatchment>
    {
        public enum AddButtonMode
        {
            Comment,
            RichText,
            DisplayCapture,
        }

        public FrameLayout TimeLine { get; private set; }

        public FrameLayout ControlPanel { get; private set; }

        public AttatchedFrame(Attatchment obj) : base(obj)
        {
        }

        protected override void CreateAttributes()
        {
            Attributes = new AttatchedFrameAttributes(this);
        }

        protected override void AfterAttributesCreated()
        {
            base.AfterAttributesCreated();
            
            TimeLine = new FrameLayout(this);
            FrameLayoutAttributes timeLine_att = TimeLine.Attributes as FrameLayoutAttributes;
            timeLine_att.Size = Attributes.Bounds.Size - new SizeF(20, 20);
            timeLine_att.RelativeLocation = new SizeF(10, 10);
            TimeLine.Tag = 0;

            Note note_Created = new Note(TimeLine, "Created");
            TimeLine.Items.Add(note_Created);

            ControlPanel = new FrameLayout(this);
            FrameLayoutAttributes controlPanel_att = ControlPanel.Attributes as FrameLayoutAttributes;
            controlPanel_att.Size = new SizeF(TimeLine.Attributes.Bounds.Width, 20);
            controlPanel_att.RelativeLocation = new SizeF(10, Attributes.Bounds.Height - 30);

            AddButtonMode mode = (AddButtonMode)Enum.GetValues(typeof(AddButtonMode)).GetValue(0);
            ControlButton button_Add = new ControlButton(ControlPanel, GetAddButtonText(mode));
            button_Add.Tag = mode;
            ControlButtonAttributes button_Add_Attributes = button_Add.Attributes as ControlButtonAttributes;
            button_Add_Attributes.Size = new SizeF(ControlPanel.Attributes.Bounds.Width * 0.4f, ControlPanel.Attributes.Bounds.Height);

            button_Add.MouseUp += AddToTimeLine;
            button_Add.MouseUp += ChangeAddButtonMode;

            ControlButton button_ScrollUp = new ControlButton(ControlPanel, "UP");
            ControlButtonAttributes button_ScrollUp_Attributes = button_ScrollUp.Attributes as ControlButtonAttributes;
            button_ScrollUp_Attributes.Size = new SizeF(ControlPanel.Attributes.Bounds.Width * 0.25f, ControlPanel.Attributes.Bounds.Height);
            button_ScrollUp_Attributes.RelativeLocation = new SizeF(ControlPanel.Attributes.Bounds.Width * 0.45f, 0);

            button_ScrollUp.MouseUp += ScrollUp;

            ControlButton button_ScrollDown = new ControlButton(ControlPanel, "DOWN");
            ControlButtonAttributes button_ScrollDown_Attributes = button_ScrollDown.Attributes as ControlButtonAttributes;
            button_ScrollDown_Attributes.Size = new SizeF(ControlPanel.Attributes.Bounds.Width * 0.25f, ControlPanel.Attributes.Bounds.Height);
            button_ScrollDown_Attributes.RelativeLocation = new SizeF(ControlPanel.Attributes.Bounds.Width * 0.75f, 0);

            button_ScrollDown.MouseUp += ScrollDown;

            ControlPanel.Items.Add(button_Add);
            ControlPanel.Items.Add(button_ScrollUp);
            ControlPanel.Items.Add(button_ScrollDown);
        }

        private void AddToTimeLine(object sender, Canvas_MouseEventArg e)
        {
            if (e.Button == MouseButtons.Left && sender is ControlButton button && button.Tag is AddButtonMode mode)
            {
                switch (mode)
                {
                    case AddButtonMode.Comment:
                        new CommentForm(this).Show();
                        break;
                    case AddButtonMode.RichText:
                        new RichTextForm(this).Show();
                        break;
                    case AddButtonMode.DisplayCapture:
                        new CaptureForm(this).Show();
                        break;
                }
            }
        }

        private void ChangeAddButtonMode(object sender, Canvas_MouseEventArg e)
        {
            if (sender is ControlButton button && e.Button == MouseButtons.Right)
            {
                ContextMenuStrip menu = new ContextMenuStrip();

                foreach (AddButtonMode mode in Enum.GetValues(typeof(AddButtonMode)))
                {
                    string text = GetAddButtonText(mode);

                    EventHandler onClick = (sender_Click, e_Click) =>
                    {
                        button.Tag = mode;
                        button.Text = text;

                        e.Canvas.Refresh();
                    };

                    GH_DocumentObject.Menu_AppendItem(menu, text, onClick, true, button.Tag is AddButtonMode current && current == mode);
                }

                menu.Show(e.Canvas, e.Location);
            }
        }

        private static string GetAddButtonText(AddButtonMode mode)
        {
            string text;
            switch (mode)
            {
                case AddButtonMode.Comment:
                    text = "Short Comment";
                    break;
                case AddButtonMode.RichText:
                    text = "Long Comment";
                    break;
                case AddButtonMode.DisplayCapture:
                    text = "Image";
                    break;
                default:
                    text = string.Empty;
                    break;
            }
            return text;
        }

        private void ScrollUp(object sender, Canvas_MouseEventArg e)
        {
            if (e.Button == MouseButtons.Left)
            {
                TimeLine.Tag = (int)TimeLine.Tag + 1;
                SetScrollRelative();

                e.Canvas.Refresh();
            }
        }

        private void ScrollDown(object sender, Canvas_MouseEventArg e)
        {
            if (e.Button == MouseButtons.Left)
            {
                TimeLine.Tag = (int)TimeLine.Tag - 1;
                SetScrollRelative();
                
                e.Canvas.Refresh();
            }
        }

        private void SetScrollRelative()
        {
            int relative = (TimeLine.Tag is int) ? (int)TimeLine.Tag : 0;
            TimeLine.Tag = TimeLine.Items.Select(i => TimeLine.Items.IndexOf(i) - relative).OrderBy(i => Math.Abs(i)).FirstOrDefault() + relative;
        }
    }
}
