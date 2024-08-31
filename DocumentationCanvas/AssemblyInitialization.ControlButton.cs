using DocumentationCanvas.Objects;
using DocumentationCanvas.Objects.Layout;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using System.Windows.Forms;

namespace DocumentationCanvas
{
    public partial class AssemblyInitialization : GH_AssemblyPriority
    {
        private void SetUpControlButton(GH_Canvas canvas)
        {
            canvas.MouseMove += ButtonMouseMove;
            canvas.MouseDown += ButtonMouseDown;
            canvas.MouseUp += ButtonMouseUp;
        }

        private void ButtonMouseMove(object sender, MouseEventArgs e)
        {
            Canvas_MouseEventArg arg = new Canvas_MouseEventArg(e, sender as GH_Canvas);

            foreach (Attatchment attatchment in m_Attatchments)
            {
                foreach (IDocumentationObject obj in attatchment.Frame.ControlPanel.Items)
                {
                    if (obj is ControlButton button)
                        button.OnMouseMove(arg);
                }
            }
        }

        private void ButtonMouseDown(object sender, MouseEventArgs e)
        {
            Canvas_MouseEventArg arg = new Canvas_MouseEventArg(e, sender as GH_Canvas);

            foreach (Attatchment attatchment in m_Attatchments)
            {
                foreach (IDocumentationObject obj in attatchment.Frame.ControlPanel.Items)
                {
                    if (obj is ControlButton button)
                        button.OnMouseDown(arg);
                }
            }
        }

        private void ButtonMouseUp(object sender, MouseEventArgs e)
        {
            Canvas_MouseEventArg arg = new Canvas_MouseEventArg(e, sender as GH_Canvas);

            foreach (Attatchment attatchment in m_Attatchments)
            {
                foreach (IDocumentationObject obj in attatchment.Frame.ControlPanel.Items)
                {
                    if (obj is ControlButton button)
                        button.OnMouseUp(arg);
                }
            }
        }
    }
}