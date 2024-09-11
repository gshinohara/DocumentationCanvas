﻿using DocumentationCanvas.Objects;
using DocumentationCanvas.TimeLineDashboard;
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

                foreach (IGH_DocumentObject obj in e.NewDocument.Objects)
                {
                    if (obj is DisplayObject displayObject)
                        (displayObject.TargetCollection as DisplayTargetCollection).AddRange(m_AttatchmentObjects.Select(o => new DisplayTarget(o)));
                }
            };

            canvas.Document_ObjectsAdded += (sender, e) =>
            {
                foreach (IGH_DocumentObject obj in e.Objects)
                {
                    AttatchmentObject attatchmentObject = new AttatchmentObject(obj) { IsValid = IsApplyToObject(obj) };

                    m_AttatchmentObjects.Add(attatchmentObject);

                    foreach (IGH_DocumentObject obj1 in sender.Objects)
                    {
                        if (obj1 is DisplayObject displayObject && !e.Objects.Contains(obj1))
                            (displayObject.TargetCollection as DisplayTargetCollection).Add(new DisplayTarget(attatchmentObject));
                    }
                }

                foreach (IGH_DocumentObject obj in e.Objects)
                {
                    if (obj is DisplayObject displayObject)
                        (displayObject.TargetCollection as DisplayTargetCollection).AddRange(m_AttatchmentObjects.Select(o => new DisplayTarget(o)));
                }
            };

            canvas.Document_ObjectsDeleted += (sender, e) =>
            {
                foreach (IGH_DocumentObject obj in e.Objects)
                {
                    AttatchmentObject attatchmentObject = m_AttatchmentObjects.FirstOrDefault(a => a.LinkedObject == obj);

                    m_AttatchmentObjects.Remove(attatchmentObject);

                    foreach (IGH_DocumentObject obj1 in sender.Objects)
                    {
                        if (obj1 is DisplayObject displayObject && !e.Objects.Contains(obj1))
                        {
                            DisplayTargetCollection collection = displayObject.TargetCollection as DisplayTargetCollection;
                            collection.Remove(collection.FirstOrDefault(target => target.Owner == attatchmentObject));
                        }
                    }
                }
            };

            canvas.CanvasPostPaintObjects += (sender) =>
            {
                foreach (AttatchmentObject attatchment in m_AttatchmentObjects)
                    attatchment.Attributes.ExpirePreview(sender);
            };

            canvas.MouseMove += (sender, e) =>
            {
                foreach (AttatchmentObject attatchment in m_AttatchmentObjects)
                    attatchment.Attributes.OnMouseMove(new Canvas_MouseEventArg(e, canvas));
            };

            canvas.MouseDown += (sender, e) =>
            {
                foreach (AttatchmentObject attatchment in m_AttatchmentObjects)
                    attatchment.Attributes.OnMouseDown(new Canvas_MouseEventArg(e, canvas));
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
