using Grasshopper.GUI.Canvas;
using System;
using System.Drawing;
using WireEventImplementor;

namespace DocumentationCanvas.AssemblyPriority
{
    public class SetUpWireEvent
    {
        private WireStatus WireStatus { get; set; }

        public void Subscribe(GH_Canvas canvas)
        {
            WireInstances.SetUp(canvas);

            canvas.CanvasPaintEnd += sender =>
            {
                if (WireStatus == null)
                    return;

                Func<PointF, RectangleF> getHighlight = center =>
                {
                    RectangleF rect = new RectangleF { Width = 30, Height = 30, Location = center };
                    rect.Offset(-rect.Width / 2, -rect.Height / 2);
                    return rect;
                }; 

                canvas.Graphics.FillEllipse(new SolidBrush(Color.FromArgb(150, Color.Orange)), getHighlight(WireStatus.PreviousSideParam.Attributes.OutputGrip));
                canvas.Graphics.FillEllipse(new SolidBrush(Color.FromArgb(150, Color.Red)), getHighlight(WireStatus.SubsequentSideParam.Attributes.InputGrip));
            };

            WireInstances.Wiring += status =>
            {
                WireStatus = null;
            };

            WireInstances.PreWired += status =>
            {
                WireStatus = status;
            };

            WireInstances.PostWired += status =>
            {
                WireStatus = null;
            };
        }
    }
}
