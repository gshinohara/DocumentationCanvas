namespace DocumentationCanvas.Objects.Layout
{
    internal class Note : Content
    {
        public Note(FrameLayout obj, string shortDescription) : base(obj, shortDescription)
        {
        }

        protected override void CreateAttributes()
        {
            Attributes = new NoteAttributes(this);
        }
    }
}
