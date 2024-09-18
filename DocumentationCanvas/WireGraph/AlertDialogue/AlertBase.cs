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
                Spacing = new Eto.Drawing.Size(15, 15),
                Padding = 15,
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
                    wireStatus.SubsequentSideParam.AddSource(wireStatus.PreviousSideParam);
                    WireInstances.OnPostWired(wireStatus);
                }

                Close();
            };

            Button button_OK = new Button(onClick)
            {
                Text = "OK",
            };

            layout.AddSeparateRow(imageView);
            layout.AddSeparateRow(null,checkBox_WireCancel, null, checkBox_Lock,null);
            layout.AddSeparateRow(null, button_OK, null);
            layout.AddSeparateRow(null);

            Content = layout;
        }
    }
}
