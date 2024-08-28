using System;

namespace DocumentationCanvas.Objects.Layout
{
    internal abstract class Content : DocumentationObject<AttatchedFrame>
    {
        public DateTime TimeStamp { get; }

        public string ShortDescription { get; protected set; }

        public Content(AttatchedFrame obj, string shortDecription) : base(obj)
        {
            TimeStamp = DateTime.Now;
            ShortDescription = shortDecription;
        }
    }
}