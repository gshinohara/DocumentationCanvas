using DocumentationCanvas.Objects.Layout;
using DocumentationCanvas.Objects.Layout.InputForm;
using System.Drawing;

namespace DocumentationCanvas.Objects
{
    internal class AttatchedFrame : DocumentationObject<Attatchment>
    {
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

            Note note_Created = new Note(TimeLine, "Created");
            TimeLine.Items.Add(note_Created);

            ControlPanel = new FrameLayout(this);
            FrameLayoutAttributes controlPanel_att = ControlPanel.Attributes as FrameLayoutAttributes;
            controlPanel_att.Size = new SizeF(20, 20);
            controlPanel_att.RelativeLocation = new SizeF(10, Attributes.Bounds.Height - 30);

            ControlButton button_Add = new ControlButton(ControlPanel, "+");
            ControlButtonAttributes button_Add_Attributes = button_Add.Attributes as ControlButtonAttributes;
            button_Add_Attributes.Size = new SizeF(ControlPanel.Attributes.Bounds.Height, 20);

            button_Add.MouseUp += AddToTimeLine;

            ControlPanel.Items.Add(button_Add);
        }

        private void AddToTimeLine(object sender, ControlButton.Canvas_MouseEventArg e)
        {
            CommentForm commentForm = new CommentForm(this);
            commentForm.Show();
        }
    }
}
