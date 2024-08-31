using Grasshopper.GUI.Canvas;
using System.Drawing;

namespace DocumentationCanvas.Objects
{
    internal interface IDocumentationObjectAttributes
    {
        bool IsVisible { get; set; }

        RectangleF Bounds { get; }

        void ExpirePreview(GH_Canvas canvas);

        void Render(GH_Canvas canvas);
    }
}