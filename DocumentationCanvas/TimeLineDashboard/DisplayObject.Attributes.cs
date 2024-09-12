using CustomGrip.Grips;
using CustomGrip.Sources;
using DocumentationCanvas.Objects.Layout;
using Grasshopper.GUI;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace DocumentationCanvas.TimeLineDashboard
{
    public class DisplayObjectAttributes : WiringObjectAttributes<DisplayTarget>
    {
        public DisplayObjectAttributes(DisplayObject owner) : base(owner)
        {
            RectangleF rect = Bounds;
            rect.Size = new Size(150, 300);
            Bounds = rect;

            new DisplayObjectInputGrip(this) { Direction = new SizeF(0, 50) };
        }

        public void SetGripPosition()
        {
            for (int i = 0; i < MyInputGrips.Count; i++)
            {
                PointF p = MyInputGrips[i].Position;
                p.X = Bounds.Left + Bounds.Width * i / MyInputGrips.Count + Bounds.Width / MyInputGrips.Count / 2;
                p.Y = Bounds.Bottom;
                MyInputGrips[i].Position = p;
            }
        }


        protected override void Layout()
        {
            base.Layout();

            SetGripPosition();
        }

        protected override void Render(GH_Canvas canvas, Graphics graphics, GH_CanvasChannel channel)
        {
            base.Render(canvas, graphics, channel);

            switch (channel)
            {
                case GH_CanvasChannel.Objects:
                    GraphicsPath graphicsPath = GH_CapsuleRenderEngine.CreateRoundedRectangle(Bounds, 3);
                    graphics.FillPath(new SolidBrush(Selected ? Color.PaleGreen : Color.GhostWhite), graphicsPath);
                    graphics.DrawPath(new Pen(Color.Black), graphicsPath);

                    foreach (Grip grip in MyInputGrips)
                    {
                        if (grip == MyInputGrips.FirstOrDefault())
                            continue;
                        PointF[] points = { new PointF(grip.Position.X - Bounds.Width / MyInputGrips.Count / 2, Bounds.Bottom), new PointF(grip.Position.X - Bounds.Width / MyInputGrips.Count / 2, Bounds.Top) };
                        byte[] bytes = { (byte)PathPointType.Line, (byte)PathPointType.Line };
                        GraphicsPath straight = new GraphicsPath(points, bytes);
                        graphics.DrawPath(new Pen(Color.Black), straight);
                    }
                    
                    List<dynamic> contents_with_grip = new List<dynamic>();
                    foreach (Grip grip in MyInputGrips)
                    {
                        foreach (DisplayTarget target in (grip as DisplayObjectInputGrip).TargetObjects)
                        {
                            foreach (var item in target.Owner.AttatchedFrame.TimeLine.Items)
                            {
                                if (item is Content content)
                                    contents_with_grip.Add(new { grip = grip, content = content });
                            }
                        }
                    }
                    contents_with_grip.Sort((a, b) => ((a.content as Content).TimeStamp - (b.content as Content).TimeStamp).Milliseconds);

                    int count = 0;
                    foreach (var group in contents_with_grip.GroupBy(g => g.content))
                    {
                        foreach (Grip grip in MyInputGrips)
                        {
                            RectangleF rect = new RectangleF { Width = Bounds.Width / MyInputGrips.Count, Height = 17, Location = grip.Position };
                            rect.Y -= Bounds.Height - 5 - (rect.Height + 5) * count;
                            rect.X -= rect.Width / 2;
                            rect.Inflate(-5, 0);

                            RectangleF rect_Icon = rect;
                            rect_Icon.Width = rect.Height;

                            RectangleF rect_Desc = rect;
                            rect_Desc.Width -= rect_Icon.Width;
                            rect_Desc.X = rect_Icon.Right;

                            rect_Icon.Inflate(-1, -1);

                            if (group.Any(g => g.grip == grip))
                            {
                                graphics.DrawPath(new Pen(Color.Black, 1), GH_CapsuleRenderEngine.CreateRoundedRectangle(rect, 0));
                                graphics.DrawImage(group.Key.LinkedObject.LinkedObject.LinkedObject.LinkedObject.Icon_24x24, rect_Icon);
                                graphics.DrawString(group.Key.ShortDescription, GH_FontServer.Standard, new SolidBrush(Color.Black), rect_Desc, GH_TextRenderingConstants.NearCenter);
                            }
                        }

                        count ++;
                    }

                    break;
            }
        }
    }
}
