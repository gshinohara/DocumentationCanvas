namespace DocumentationCanvas.Objects
{
    internal class AttatchedFrame : DocumentationObject<Attatchment>
    {
        public AttatchedFrame(Attatchment obj) : base(obj)
        {
        }

        protected override void CreateAttributes()
        {
            Attributes = new AttatchedFrameAttributes(this);
        }
    }
}
