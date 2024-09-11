using Grasshopper.Kernel;

namespace DocumentationCanvas.Objects
{
    public class AttatchmentObject : DocumentationObject<IGH_DocumentObject>
    {
        public ActivationButton ActivationButton { get; }

        public AttatchedFrame AttatchedFrame { get; }

        public AttatchmentObject(IGH_DocumentObject obj) : base(obj)
        {
            ActivationButton = new ActivationButton(this);
            AttatchedFrame = new AttatchedFrame(this);

            Attributes.MouseUp += (s, e) =>
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                    ActivationButton.Attributes.OnMouseUp(e);
                AttatchedFrame.Attributes.OnMouseUp(e);
            };

            ActivationButton.Attributes.MouseUp += (s, e) =>
            {
                ActivationButton.IsOpen ^= true;
                AttatchedFrame.IsValid = ActivationButton.IsOpen;
            };

            Attributes.PostPaint += canvas =>
            {
                ActivationButton.Attributes.ExpirePreview(canvas);
                AttatchedFrame.Attributes.ExpirePreview(canvas);
            };

            PostValidityChanged += () =>
            {
                ActivationButton.IsValid = IsValid;
                AttatchedFrame.IsValid = IsValid;
            };
        }

        protected override void CreateAttributes()
        {
            Attributes = new AttatchmentObjectAttributes(this);
        }
    }
}
