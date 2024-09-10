using Grasshopper.Kernel;

namespace DocumentationCanvas.Objects
{
    public class Attatchment : DocumentationObject<IGH_DocumentObject>
    {
        private bool m_IsOpen;

        public bool IsOpen
        {
            get => m_IsOpen;
            set
            {
                m_IsOpen = value;
                Frame.Attributes.IsVisible = value;
            }
        }

        public AttatchedFrame Frame { get; }

        public Attatchment(IGH_DocumentObject obj) : base(obj)
        {
            Frame = new AttatchedFrame(this);
            Attributes.IsVisible = true;
            IsOpen = false;
        }

        protected override void CreateAttributes()
        {
            Attributes = new AttatchmentAttributes(this);
        }
    }
}
