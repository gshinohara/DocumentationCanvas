using System;

namespace DocumentationCanvas.Objects.Layout
{
    internal abstract class ContentWithExtension : Content
    {
        public event EventHandler<Canvas_MouseEventArg> MouseDown;

        public ContentWithExtension(FrameLayout obj, string shortDescription) : base(obj, shortDescription)
        {
        }

        public void OnMouseDown(Canvas_MouseEventArg e)
        {
            if (Attributes.Bounds.Contains(e.CanvasLocation))
                MouseDown?.Invoke(this, e);
        }
    }
}