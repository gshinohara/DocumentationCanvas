using Grasshopper.Kernel;

namespace DocumentationCanvas.Objects
{
    internal class Attatchment
    {
        public IGH_DocumentObject LinkedObject { get; }

        public bool IsOpen { get; set; }

        public AttatchmentAttributes Attributes { get; private set; }

        public Attatchment(IGH_DocumentObject obj)
        {
            LinkedObject = obj;
            IsOpen = false;

            CreateAttributes();
        }

        private void CreateAttributes()
        {
            Attributes = new AttatchmentAttributes(this);
        }
    }
}
