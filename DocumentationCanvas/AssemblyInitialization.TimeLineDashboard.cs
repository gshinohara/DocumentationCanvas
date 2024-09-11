using CustomGrip.Grips;
using DocumentationCanvas.TimeLineDashboard;
using Grasshopper.GUI;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace DocumentationCanvas
{
    public partial class AssemblyInitialization
    {
        private void SetUpTimeLineDashboard(GH_Canvas canvas)
        {
            Action<RectangleF, string> draw = (rect, text) =>
            {
                GraphicsPath path = GH_CapsuleRenderEngine.CreateRoundedRectangle(rect, 3);
                canvas.Graphics.FillPath(new SolidBrush(Color.LightGray), path);
                canvas.Graphics.DrawPath(new Pen(Color.DarkGray, 3), path);

                Font font = new Font(GH_FontServer.Standard.FontFamily, 15, FontStyle.Bold);
                canvas.Graphics.DrawString(text, font, new SolidBrush(Color.Black), rect, GH_TextRenderingConstants.CenterCenter);
            };

            Func<DisplayObjectAttributes, RectangleF> button_plus = att =>
            {
                RectangleF rect = new RectangleF { Width = 20, Height = 20 };
                rect.X = att.Bounds.Right + 20;
                rect.Y= (att.Bounds.Top + att.Bounds.Bottom) / 2;
                return rect;
            };

            Func<DisplayObjectInputGrip, RectangleF> button_minus = grip =>
            {
                RectangleF rect = new RectangleF { Width = 20, Height = 20 };
                rect.X = grip.Position.X - rect.Width / 2;
                rect.Y = grip.Parent.Bounds.Top - 40;
                return rect;
            };

            canvas.CanvasPostPaintGroups += sender =>
            {
                if (canvas.Viewport.Zoom < 1)
                    return;

                if (sender.Document is GH_Document document)
                {
                    foreach (IGH_DocumentObject obj in document.Objects) 
                    {
                        if (obj is DisplayObject displayObject)
                        {
                            DisplayObjectAttributes att = displayObject.Attributes as DisplayObjectAttributes;

                            draw(button_plus(att), "+");
                            foreach (Grip grip in att.MyInputGrips)
                            {
                                if (att.MyInputGrips.Count > 1)
                                    draw(button_minus(grip as DisplayObjectInputGrip), "-");
                            }
                        }
                    }
                }
            };

            canvas.MouseClick += (s, e) =>
            {
                if (canvas.Viewport.Zoom < 1)
                    return;

                if (e.Button == System.Windows.Forms.MouseButtons.Left && canvas.Document is GH_Document document)
                {
                    foreach (IGH_DocumentObject obj in document.Objects)
                    {
                        if (obj is DisplayObject displayObject)
                        {
                            DisplayObjectAttributes att = displayObject.Attributes as DisplayObjectAttributes;

                            if (button_plus(att).Contains(canvas.Viewport.UnprojectPoint(e.Location)))
                            {
                                RectangleF rect = att.Bounds;
                                rect.Width += rect.Width / att.MyInputGrips.Count;
                                att.Bounds = rect;

                                DisplayObjectInputGrip grip = new DisplayObjectInputGrip(att) { Direction = new SizeF(0, 50) };
                            }

                            else if (att.MyInputGrips.Count > 1 && att.MyInputGrips.FirstOrDefault(g => button_minus(g as DisplayObjectInputGrip).Contains(canvas.Viewport.UnprojectPoint(e.Location))) is DisplayObjectInputGrip grip)
                            {
                                RectangleF rect = att.Bounds;
                                rect.Width -= rect.Width / att.MyInputGrips.Count;
                                att.Bounds = rect;

                                att.MyInputGrips.Remove(grip);
                            }

                            att.SetGripPosition();
                        }
                    }
                }
            };
        }
    }
}
