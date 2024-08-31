using DocumentationCanvas.Objects;
using DocumentationCanvas.Objects.Layout;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using System.Windows.Forms;

namespace DocumentationCanvas
{
    public partial class AssemblyInitialization : GH_AssemblyPriority
    {
        private void SetUpContentWithExtension(GH_Canvas canvas)
        {
            canvas.MouseDown += ContentMouseDown;
        }

        private void ContentMouseDown(object sender, MouseEventArgs e)
        {
            Canvas_MouseEventArg arg = new Canvas_MouseEventArg(e, sender as GH_Canvas);

            foreach (Attatchment attatchment in m_Attatchments)
            {
                foreach (IDocumentationObject obj in attatchment.Frame.TimeLine.Items)
                {
                    if (obj is ContentWithExtension content)
                        content.OnMouseDown(arg);
                }
            }
        }
    }
}