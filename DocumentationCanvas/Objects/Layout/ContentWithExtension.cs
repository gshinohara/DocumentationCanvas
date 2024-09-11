using System;

namespace DocumentationCanvas.Objects.Layout
{
    internal abstract class ContentWithExtension : Content
    {
        public ContentWithExtension(FrameLayout obj, string shortDescription) : base(obj, shortDescription)
        {
        }
    }
}