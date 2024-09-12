using DocumentationCanvas.Objects.Layout;
using DocumentationCanvas.Objects.Layout.InputForm;
using Grasshopper.Kernel;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DocumentationCanvas.Objects
{
    public class AttatchedFrame : DocumentationObject<AttatchmentObject>
    {
        public enum AddButtonMode
        {
            Comment,
            RichText,
            DisplayCapture,
        }

        public FrameLayout TimeLine { get; }

        public FrameLayout ControlPanel { get; }

        public AttatchedFrame(AttatchmentObject obj) : base(obj)
        {
            #region Create layouts

            TimeLine = new FrameLayout(this);
            ControlPanel = new FrameLayout(this);

            Note note_Created = new Note(TimeLine, "Created");
            TimeLine.Items.Add(note_Created);

            FrameLayoutAttributes timeLine_att = TimeLine.Attributes as FrameLayoutAttributes;
            timeLine_att.Size = Attributes.Bounds.Size - new SizeF(20, 20);
            timeLine_att.RelativeLocation = new SizeF(10, 10);
            TimeLine.Tag = 0;

            FrameLayoutAttributes controlPanel_att = ControlPanel.Attributes as FrameLayoutAttributes;
            controlPanel_att.Size = new SizeF(TimeLine.Attributes.Bounds.Width, 20);
            controlPanel_att.RelativeLocation = new SizeF(10, Attributes.Bounds.Height - 30);

            #endregion

            #region Set buttons

            AddButtonMode mode = (AddButtonMode)Enum.GetValues(typeof(AddButtonMode)).GetValue(0);
            ControlButton button_Add = new ControlButton(ControlPanel, GetAddButtonText(mode));
            button_Add.Tag = mode;
            ControlButtonAttributes button_Add_Attributes = button_Add.Attributes as ControlButtonAttributes;
            button_Add_Attributes.Size = new SizeF(ControlPanel.Attributes.Bounds.Width * 0.4f, ControlPanel.Attributes.Bounds.Height);

            button_Add.Attributes.MouseUp += AddToTimeLine;
            button_Add.Attributes.MouseUp += ChangeAddButtonMode;

            ControlButton button_ScrollUp = new ControlButton(ControlPanel, "UP");
            ControlButtonAttributes button_ScrollUp_Attributes = button_ScrollUp.Attributes as ControlButtonAttributes;
            button_ScrollUp_Attributes.Size = new SizeF(ControlPanel.Attributes.Bounds.Width * 0.25f, ControlPanel.Attributes.Bounds.Height);
            button_ScrollUp_Attributes.RelativeLocation = new SizeF(ControlPanel.Attributes.Bounds.Width * 0.45f, 0);

            button_ScrollUp.Attributes.MouseUp += ScrollUp;

            ControlButton button_ScrollDown = new ControlButton(ControlPanel, "DOWN");
            ControlButtonAttributes button_ScrollDown_Attributes = button_ScrollDown.Attributes as ControlButtonAttributes;
            button_ScrollDown_Attributes.Size = new SizeF(ControlPanel.Attributes.Bounds.Width * 0.25f, ControlPanel.Attributes.Bounds.Height);
            button_ScrollDown_Attributes.RelativeLocation = new SizeF(ControlPanel.Attributes.Bounds.Width * 0.75f, 0);

            button_ScrollDown.Attributes.MouseUp += ScrollDown;

            #endregion

            #region Add buttons to ControlPanel

            ControlPanel.Items.Add(button_Add);
            ControlPanel.Items.Add(button_ScrollUp);
            ControlPanel.Items.Add(button_ScrollDown);

            #endregion

            #region Subscribe events of layouts

            Attributes.PostPaint += canvas =>
            {
                TimeLine.Attributes.ExpirePreview(canvas);
                ControlPanel.Attributes.ExpirePreview(canvas);
            };

            Attributes.MouseMove += (s, e) =>
            {
                TimeLine.Attributes.OnMouseMove(e);
                ControlPanel.Attributes.OnMouseMove(e);
            };

            Attributes.MouseDown += (s, e) =>
            {
                TimeLine.Attributes.OnMouseDown(e);
                ControlPanel.Attributes.OnMouseDown(e);
            };

            Attributes.MouseUp += (s, e) =>
            {
                TimeLine.Attributes.OnMouseUp(e);
                ControlPanel.Attributes.OnMouseUp(e);
            };

            PostValidityChanged += () =>
            {
                TimeLine.IsValid = IsValid;
                ControlPanel.IsValid = IsValid;
            };

            #endregion
        }

        protected override void CreateAttributes()
        {
            Attributes = new AttatchedFrameAttributes(this);
        }

        private void AddToTimeLine(object sender, Canvas_MouseEventArg e)
        {
            if (e.Button == MouseButtons.Left && sender is ControlButtonAttributes buttonAtt && buttonAtt.Owner.Tag is AddButtonMode mode)
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

                SetButtonValidity();
            }
        }
        
        private void SetButtonValidity()
        {
            foreach (IDocumentationObject item in TimeLine.Items)
            {
                if (item.Attributes is IContentAttributes contentAttr)
                    item.IsValid = TimeLine.IsValid && contentAttr.GetPosition() >= 0 && contentAttr.GetPosition() < 5;
            }
        }

        private void ChangeAddButtonMode(object sender, Canvas_MouseEventArg e)
        {
            if (e.Button == MouseButtons.Right && sender is ControlButtonAttributes buttonAtt)
            {
                ContextMenuStrip menu = new ContextMenuStrip();

                foreach (AddButtonMode mode in Enum.GetValues(typeof(AddButtonMode)))
                {
                    string text = GetAddButtonText(mode);

                    EventHandler onClick = (sender_Click, e_Click) =>
                    {
                        buttonAtt.Owner.Tag = mode;
                        buttonAtt.Owner.Text = text;

                        e.Canvas.Refresh();
                    };

                    GH_DocumentObject.Menu_AppendItem(menu, text, onClick, true, buttonAtt.Owner.Tag is AddButtonMode current && current == mode);
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

            SetButtonValidity();
        }
    }
}