using Grasshopper.GUI.Canvas;
using System;
using System.Drawing;

namespace DocumentationCanvas.Objects
{
    public interface IDocumentationObjectAttributes
    {
        event EventHandler<Canvas_MouseEventArg> MouseMove;

        event EventHandler<Canvas_MouseEventArg> MouseDown;

        event EventHandler<Canvas_MouseEventArg> MouseUp;

        event PostPaintEventHandler PostPaint;

        RectangleF Bounds { get; }

        void ExpirePreview(GH_Canvas canvas);

        void OnMouseMove(Canvas_MouseEventArg e);

        void OnMouseDown(Canvas_MouseEventArg e);

        void OnMouseUp(Canvas_MouseEventArg e);
    }
}