using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;

namespace DocumentationCanvas.WireGraph.AlertDialogue
{
    internal abstract partial class AlertBase
    {
        private static Bitmap CreateWireImage(WireEventImplementor.WireStatus wireStatus)
        {
            GH_Canvas canvas = Grasshopper.Instances.ActiveCanvas;
            
            Rectangle rect = GH_Convert.ToRectangle(
                RectangleF.Union(
                    wireStatus.WireTarget.Attributes.GetTopLevel.Bounds,
                    wireStatus.WireSource.Attributes.GetTopLevel.Bounds
                    )
                );
            rect.Inflate(20, 20);

            Bitmap image = new Bitmap(rect.Width, rect.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            Graphics graphics = Graphics.FromImage(image);
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            graphics.Clear(Color.White);
            graphics.TranslateTransform(-rect.X, -rect.Y);

            if (wireStatus.SubsequentSideParam.Attributes.GetTopLevel.DocObject is GH_Component component)
            {
                foreach (IGH_Param input in component.Params.Input)
                {
                    if (input == wireStatus.SubsequentSideParam)
                        continue;

                    foreach (IGH_Param source in input.Sources)
                    {
                        if (source.VolatileDataCount == 0)
                            continue;
                        GraphicsPath path_source = GH_Painter.ConnectionPath(input.Attributes.InputGrip, source.Attributes.OutputGrip, GH_WireDirection.left, GH_WireDirection.right);
                        graphics.DrawPath(new Pen(Color.Gray, 1), path_source);
                    }
                }
            }

            foreach(IGH_Param source in wireStatus.SubsequentSideParam.Sources)
            {
                if (source == wireStatus.PreviousSideParam)
                    continue;
                if (source.VolatileDataCount == 0)
                    continue;
                GraphicsPath path_source = GH_Painter.ConnectionPath(wireStatus.SubsequentSideParam.Attributes.InputGrip, source.Attributes.OutputGrip, GH_WireDirection.left, GH_WireDirection.right);
                graphics.DrawPath(new Pen(Color.Black, 2), path_source);
            }

            GraphicsPath path = GH_Painter.ConnectionPath(wireStatus.SubsequentSideParam.Attributes.InputGrip, wireStatus.PreviousSideParam.Attributes.OutputGrip, GH_WireDirection.left, GH_WireDirection.right);
            graphics.DrawPath(new Pen(Color.Red, 5), path);

            object[] arguments = { canvas, graphics, GH_CanvasChannel.Objects };

            System.Action<IGH_Param> render = param =>
            {
                IGH_Attributes att = param.Attributes.GetTopLevel;
                MethodInfo renderMethod = att.GetType().GetMethod("Render", BindingFlags.Instance | BindingFlags.NonPublic);
                renderMethod.Invoke(att, arguments);
            };

            render(wireStatus.WireSource);
            render(wireStatus.WireTarget);

            return image;
        }
    }
}