using DocumentationCanvas.Objects.Layout;
using System.Collections.Generic;

namespace DocumentationCanvas.Objects
{
    internal class AttatchedFrame : DocumentationObject<Attatchment>
    {
        public List<Content> Contents { get; } = new List<Content>();

        public AttatchedFrame(Attatchment obj) : base(obj)
        {
            Note note_Created = new Note(this, "Created");
            Contents.Add(note_Created);
        }

        protected override void CreateAttributes()
        {
            Attributes = new AttatchedFrameAttributes(this);
        }
    }
}
