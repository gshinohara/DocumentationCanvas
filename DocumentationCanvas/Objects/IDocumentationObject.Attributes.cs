using Grasshopper.GUI.Canvas;
using System.Drawing;

namespace DocumentationCanvas.Objects
{
    internal interface IDocumentationObjectAttributes
    {
        RectangleF Bounds { get; }

        void Render(GH_Canvas canvas);
    }
}