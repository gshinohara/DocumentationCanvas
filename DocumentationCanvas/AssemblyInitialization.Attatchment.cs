using DocumentationCanvas.Objects;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Special;
using System.Collections.Generic;
using System.Linq;

namespace DocumentationCanvas
{
    public partial class AssemblyInitialization : GH_AssemblyPriority
    {
        private List<AttatchmentObject> m_AttatchmentObjects = new List<AttatchmentObject>();

        private void AttatchmentSetUp(GH_Canvas canvas)
        {
            canvas.DocumentChanged += (sender, e) =>
            {
                m_AttatchmentObjects.Clear();
                foreach (IGH_DocumentObject obj in e.NewDocument.Objects)
                    m_AttatchmentObjects.Add(new AttatchmentObject(obj) { IsValid = IsApplyToObject(obj) });
            };

            canvas.Document_ObjectsAdded += (sender, e) =>
            {
                foreach (IGH_DocumentObject obj in e.Objects)
                    m_AttatchmentObjects.Add(new AttatchmentObject(obj) { IsValid = IsApplyToObject(obj) });
            };

            canvas.Document_ObjectsDeleted += (sender, e) =>
            {
                foreach (IGH_DocumentObject obj in e.Objects)
                    m_AttatchmentObjects.Remove(m_AttatchmentObjects.FirstOrDefault(a => a.LinkedObject == obj));
            };

            canvas.CanvasPostPaintObjects += (sender) =>
            {
                foreach (AttatchmentObject attatchment in m_AttatchmentObjects)
                    attatchment.Attributes.ExpirePreview(sender);
            };

            canvas.MouseUp += (sender, e) =>
            {
                foreach (AttatchmentObject attatchment in m_AttatchmentObjects)
                    attatchment.Attributes.OnMouseUp(new Canvas_MouseEventArg(e, canvas));
            };
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
    }
}
