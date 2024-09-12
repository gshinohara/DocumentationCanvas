using Grasshopper.GUI.Canvas;
using System;
using System.Drawing;

namespace DocumentationCanvas.Objects
{

    internal abstract class DocumentationObjectAttributes<T> : IDocumentationObjectAttributes where T : IDocumentationObject
    {
        public event EventHandler<Canvas_MouseEventArg> MouseMove;

        public event EventHandler<Canvas_MouseEventArg> MouseDown;

        public event EventHandler<Canvas_MouseEventArg> MouseUp;

        public event PostPaintEventHandler PostPaint;

        public T Owner { get; }

        public abstract RectangleF Bounds { get; }

        public DocumentationObjectAttributes(T owner)
        {
            Owner = owner;
        }

        public virtual bool IsPickRegion(PointF point)
        {
            return Bounds.Contains(point);
        }

        public void ExpirePreview(GH_Canvas canvas)
        {
            if (Owner.IsValid)
                Render(canvas);
            PostPaint?.Invoke(canvas);
        }

        protected abstract void Render(GH_Canvas canvas);

        public void OnMouseMove(Canvas_MouseEventArg e)
        {
            if (Owner.IsValid && IsPickRegion(e.CanvasLocation))
            {
                MouseMove?.Invoke(this, e);
                e.Canvas.Refresh();
            }
        }

        public void OnMouseDown(Canvas_MouseEventArg e)
        {
            if (Owner.IsValid && IsPickRegion(e.CanvasLocation))
            {
                MouseDown?.Invoke(this, e);
                e.Canvas.Refresh();
            }
        }

        public void OnMouseUp(Canvas_MouseEventArg e)
        {
            if (Owner.IsValid && IsPickRegion(e.CanvasLocation))
            {
                MouseUp?.Invoke(this, e);
                e.Canvas.Refresh();
            }
        }
    }
}