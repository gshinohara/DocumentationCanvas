using Grasshopper.GUI;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DocumentationCanvas.Objects.Layout
{
    internal interface IContentAttributes : IDocumentationObjectAttributes
    {
        int GetPosition();
    }

    internal abstract class ContentAttributes<T> : DocumentationObjectAttributes<T>, IContentAttributes where T : Content
    {
        public override RectangleF Bounds
        {
            get
            {
                RectangleF rect = Owner.LinkedObject.Attributes.Bounds;
                rect.Height = 15;
                rect.Y += GetPosition() * (rect.Height + 5);
                
                return rect;
            }
        }

        public ContentAttributes(T owner) : base(owner)
        {
        }

        public int GetPosition()
        {
            int index = Owner.LinkedObject.Items.IndexOf(Owner);
            int relative = ((Owner.LinkedObject.Tag is int) ? (int)Owner.LinkedObject.Tag : 0);
            int position = index - relative;
            return position;
        }

        protected override void Render(GH_Canvas canvas)
        {
            RectangleF rect_Index = Bounds;
            rect_Index.Width = Bounds.Height;

            RectangleF rect_TimeStamp = Bounds;
            rect_TimeStamp.Width = 90;
            rect_TimeStamp.Offset(rect_Index.Width, 0);

            RectangleF rect_ShortDescription = Bounds;
            rect_ShortDescription.Width = Bounds.Width - rect_Index.Width - rect_TimeStamp.Width;
            rect_ShortDescription.Offset(Bounds.Width - rect_ShortDescription.Width, 0);

            foreach (RectangleF rect in new List<RectangleF> { rect_Index, rect_TimeStamp, rect_ShortDescription })
            {
                GraphicsPath graphicsPath = GH_CapsuleRenderEngine.CreateRoundedRectangle(rect, 0);

                canvas.Graphics.FillPath(new SolidBrush(Color.FromArgb(200, Color.LightGray)), graphicsPath);
                canvas.Graphics.DrawPath(new Pen(Color.Black), graphicsPath);
            }

            canvas.Graphics.DrawString((Owner.LinkedObject.Items.IndexOf(Owner) + 1).ToString(), GH_FontServer.Standard, new SolidBrush(Color.DarkSlateGray), rect_Index, GH_TextRenderingConstants.FarCenter);
            canvas.Graphics.DrawString(Owner.TimeStamp.ToString("g"), GH_FontServer.Standard, new SolidBrush(Color.DarkSlateGray), rect_TimeStamp, GH_TextRenderingConstants.NearCenter);
            canvas.Graphics.DrawString(Owner.ShortDescription, GH_FontServer.Standard, new SolidBrush(Color.DarkSlateGray), rect_ShortDescription, GH_TextRenderingConstants.NearCenter);
        }
    }
}