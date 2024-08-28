using DocumentationCanvas.Objects;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Special;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DocumentationCanvas
{
    public partial class AssemblyInitialization : GH_AssemblyPriority
    {
        private List<Attatchment> m_Attatchments = new List<Attatchment>();

        private void AttatchmentSetUp(GH_Canvas canvas)
        {
            canvas.DocumentChanged += InitialSetUp;
            canvas.CanvasPostPaintObjects += DrawAttatchment;
            canvas.MouseClick += ClickAttatchment;
        }

        private bool IsApplyToObject(IGH_DocumentObject obj)
        {
            switch (obj)
            {
                case GH_Scribble _:
                case GH_Markup _:
                    return false;
            }
            return true;
        }

        private void InitialSetUp(GH_Canvas sender, GH_CanvasDocumentChangedEventArgs e)
        {
            m_Attatchments.Clear();
            m_Attatchments.AddRange(e.NewDocument.Objects.Select(o => new Attatchment(o)));

            e.OldDocument.ObjectsAdded -= ObjectsAdded;
            e.OldDocument.ObjectsDeleted -= ObjectsDeleted;

            e.NewDocument.ObjectsAdded += ObjectsAdded;
            e.NewDocument.ObjectsDeleted += ObjectsDeleted;
        }

        private void ObjectsAdded(object sender, GH_DocObjectEventArgs e)
        {
            foreach (IGH_DocumentObject obj in e.Objects)
                m_Attatchments.Add(new Attatchment(obj));
        }

        private void ObjectsDeleted(object sender, GH_DocObjectEventArgs e)
        {
            foreach (IGH_DocumentObject obj in e.Objects)
                m_Attatchments.Remove(m_Attatchments.FirstOrDefault(a => a.LinkedObject == obj));
        }

        private void DrawAttatchment(GH_Canvas sender)
        {
            if (sender.IsDocument)
            {
                foreach (Attatchment attatchment in m_Attatchments)
                {
                    if (!IsApplyToObject(attatchment.LinkedObject))
                        continue;
                    attatchment.Attributes.Render(sender);
                }
            }
        }

        private void ClickAttatchment(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && sender is GH_Canvas canvas && canvas.IsDocument)
            {
                foreach (Attatchment attatchment in m_Attatchments)
                {
                    if (!IsApplyToObject(attatchment.LinkedObject))
                        continue;

                    if (attatchment.Attributes.Bounds.Contains(canvas.Viewport.UnprojectPoint(e.Location)))
                    {
                        attatchment.IsOpen ^= true;
                        return;
                    }
                }
            }
        }
    }
}
