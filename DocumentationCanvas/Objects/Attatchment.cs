using Grasshopper.Kernel;

namespace DocumentationCanvas.Objects
{
    internal class Attatchment : DocumentationObject<IGH_DocumentObject>
    {
        public bool IsOpen { get; set; } = false;

        public AttatchedFrame Frame { get; }

        public Attatchment(IGH_DocumentObject obj) : base(obj)
        {
            Frame = new AttatchedFrame(this);
        }

        protected override void CreateAttributes()
        {
            Attributes = new AttatchmentAttributes(this);
        }
    }
}
