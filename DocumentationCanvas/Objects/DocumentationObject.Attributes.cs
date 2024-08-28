using Grasshopper.GUI.Canvas;
using System.Drawing;

namespace DocumentationCanvas.Objects
{
    internal abstract class DocumentationObjectAttributes<T> : IDocumentationObjectAttributes where T : IDocumentationObject
    {
        public T Owner { get; }

        public abstract RectangleF Bounds { get; }

        public DocumentationObjectAttributes(T owner)
        {
            Owner = owner;
        }

        public abstract void Render(GH_Canvas canvas);
    }
}