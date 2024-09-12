using Grasshopper;
using Grasshopper.Kernel;
using System.Drawing;

namespace DocumentationCanvas.AssemblyPriority
{
    public class SetUpWireEvent : WireEventImplementor.WireAssemblyPriority
    {
        public override GH_LoadingInstruction PriorityLoad()
        {
            Instances.CanvasCreated += canvas =>
            {
                canvas.CanvasPaintEnd += Canvas_CanvasPostPaintWires;
            };

            return base.PriorityLoad();
        }

        private void Canvas_CanvasPostPaintWires(Grasshopper.GUI.Canvas.GH_Canvas sender)
        {
            if (WireTarget == null)
                return;

            PointF center = (bool)IsDragFromInput ? WireTarget.Attributes.OutputGrip : WireTarget.Attributes.InputGrip;

            RectangleF rect = new RectangleF { Width = 30, Height = 30 };
            rect.X = center.X - rect.Width / 2;
            rect.Y = center.Y - rect.Height / 2;

            sender.Graphics.FillEllipse(new SolidBrush(Color.FromArgb(100, Color.Red)), rect);
        }
    }
}
