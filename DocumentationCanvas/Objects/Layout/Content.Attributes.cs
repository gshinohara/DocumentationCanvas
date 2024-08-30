using Grasshopper.GUI;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DocumentationCanvas.Objects.Layout
{
    internal abstract class ContentAttributes<T> : DocumentationObjectAttributes<T> where T :Content
    {
        public override RectangleF Bounds
        {
            get
            {
                RectangleF rect = Owner.LinkedObject.Attributes.Bounds;
                rect.Height = 15;
                rect.Y += Owner.LinkedObject.Items.IndexOf(Owner) * (rect.Height + 5);
                
                return rect;
            }
        }

        public ContentAttributes(T owner) : base(owner)
        {
        }

        public override void Render(GH_Canvas canvas)
        {
            RectangleF rect_TimeStamp = Bounds;
            rect_TimeStamp.Width = 90;

            RectangleF rect_ShortDescription = Bounds;
            rect_ShortDescription.Width = 150;
            rect_ShortDescription.Offset(90, 0);

            foreach (RectangleF rect in new List<RectangleF> { rect_TimeStamp, rect_ShortDescription })
            {
                GraphicsPath graphicsPath = GH_CapsuleRenderEngine.CreateRoundedRectangle(rect, 0);

                canvas.Graphics.FillPath(new SolidBrush(Color.FromArgb(200, Color.LightGray)), graphicsPath);
                canvas.Graphics.DrawPath(new Pen(Color.Black), graphicsPath);
            }

            canvas.Graphics.DrawString(Owner.TimeStamp.ToString("g"), GH_FontServer.Standard, new SolidBrush(Color.DarkSlateGray), rect_TimeStamp, GH_TextRenderingConstants.NearCenter);
            canvas.Graphics.DrawString(Owner.ShortDescription, GH_FontServer.Standard, new SolidBrush(Color.DarkSlateGray), rect_ShortDescription, GH_TextRenderingConstants.NearCenter);
        }
    }
}