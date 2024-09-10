using Grasshopper.GUI.Canvas;
using System;
using System.Drawing;

namespace DocumentationCanvas.Objects
{
    public abstract class DocumentationObjectAttributes<T> : IDocumentationObjectAttributes where T : IDocumentationObject
    {

        public EventHandler<Canvas_MouseEventArg> MouseClick;

        public virtual bool IsVisible { get; set; } = false;

        public T Owner { get; }

        public abstract RectangleF Bounds { get; }

        public DocumentationObjectAttributes(T owner)
        {
            Owner = owner;
        }

        public abstract bool IsPickRegion(PointF point);

        public void ExpirePreview(GH_Canvas canvas)
        {
            if (IsVisible)
                Render(canvas);
        }

        public abstract void Render(GH_Canvas canvas);

        public void OnMouseClick(Canvas_MouseEventArg e)
        {
            if (IsPickRegion(e.CanvasLocation))
                MouseClick?.Invoke(this, e);
        }
    }
}