using System;

namespace DocumentationCanvas.Objects.Layout
{
    internal abstract class Content : DocumentationObject<FrameLayout>
    {
        public DateTime TimeStamp { get; }

        public string ShortDescription { get; protected set; }

        public Content(FrameLayout obj, string shortDecription) : base(obj)
        {
            TimeStamp = DateTime.Now;
            ShortDescription = shortDecription;
        }
    }
}