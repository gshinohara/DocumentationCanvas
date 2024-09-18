using Eto.Forms;
using Grasshopper;
using Grasshopper.Kernel;
using Rhino.UI;
using System;
using WireEventImplementor;

namespace DocumentationCanvas.WireGraph.AlertDialogue
{
    internal abstract partial class AlertBase : Dialog
    {
        public AlertBase(WireStatus wireStatus)
        {
            AutoSize = true;

            DynamicLayout layout = new DynamicLayout
            {
                Width = 400,
                Spacing = new Eto.Drawing.Size(15, 15),
                Padding = 15,
            };

            Label label = new Label
            {
                Text = "Check your wiring. It can be dangerous.",
                Wrap = WrapMode.Word,
            };

            ImageView imageView = new ImageView
            {
                Image = CreateWireImage(wireStatus).ToEto(),
                Width = 300,
            };

            CheckBox checkBox_WireCancel = new CheckBox
            {
                Text = "Cancel wiring",
                Checked = false,
            };

            CheckBox checkBox_Lock = new CheckBox
            {
                Text = "Lock solution",
                Checked = !GH_Document.EnableSolutions,
            };

            EventHandler<EventArgs> onClick = (sender, e) =>
            {
                GH_Document.EnableSolutions = !(bool)checkBox_Lock.Checked;

                if ((bool)checkBox_WireCancel.Checked)
                    WireInstances.OnPostWired(wireStatus);
                else
                {
                    switch (wireStatus.LinkMode)
                    {
                        case LinkMode.Replace:
                            wireStatus.SubsequentSideParam.RemoveAllSources();
                            wireStatus.SubsequentSideParam.AddSource(wireStatus.PreviousSideParam);
                            break;
                        case LinkMode.Add:
                            wireStatus.SubsequentSideParam.AddSource(wireStatus.PreviousSideParam);
                            break;
                        case LinkMode.Remove:
                            wireStatus.SubsequentSideParam.RemoveSource(wireStatus.PreviousSideParam);
                            break;
                    }
                 
                    WireInstances.OnPostWired(wireStatus);
                }

                Close();
            };

            Button button_OK = new Button(onClick)
            {
                Text = "OK",
            };

            layout.AddSeparateRow(label);
            layout.AddSeparateRow(imageView);
            layout.AddSeparateRow(null,checkBox_WireCancel, null, checkBox_Lock,null);
            layout.AddSeparateRow(null, button_OK, null);
            layout.AddSeparateRow(null);

            Content = layout;
        }
    }
}
