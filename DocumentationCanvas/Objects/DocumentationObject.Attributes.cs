using Grasshopper.GUI.Canvas;
using System.Drawing;

namespace DocumentationCanvas.Objects
{
    internal abstract class DocumentationObjectAttributes<T> : IDocumentationObjectAttributes where T : IDocumentationObject
    {
        public virtual bool IsVisible { get; set; } = false;

        public T Owner { get; }

        public abstract RectangleF Bounds { get; }

        public DocumentationObjectAttributes(T owner)
        {
            Owner = owner;
        }

        public void ExpirePreview(GH_Canvas canvas)
        {
            if (IsVisible)
                Render(canvas);
        }

        public abstract void Render(GH_Canvas canvas);
    }
}