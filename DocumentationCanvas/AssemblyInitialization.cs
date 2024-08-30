using Eto.Forms;
using Grasshopper;
using Grasshopper.Kernel;
using System;
using System.Threading.Tasks;

namespace DocumentationCanvas
{
    public partial class AssemblyInitialization : GH_AssemblyPriority
    {
        private class LoadSelector : Dialog<GH_LoadingInstruction>
        {
            private Label m_LabelCounting = new Label();

            public LoadSelector()
            {
                Title = "Load?";

                Padding = 5;
                Width = 200;
                AutoSize = true;
                Topmost = true;

                Location = (Eto.Drawing.Point)Screen.Bounds.Center;

                DynamicLayout layout = new DynamicLayout
                {
                    Padding = 3,
                    Spacing = new Eto.Drawing.Size(5, 10),
                };

                EventHandler<EventArgs> buttonEvent = (sender, e) => Close((GH_LoadingInstruction)(sender as Button).Tag);

                Button button_Yes = new Button(buttonEvent)
                {
                    Text = "Yes",
                    Tag = GH_LoadingInstruction.Proceed,
                };

                Button button_No = new Button(buttonEvent)
                {
                    Text = "No",
                    Tag = GH_LoadingInstruction.Abort,
                };

                layout.AddSeparateRow(new Label { Text = $"Do you want to load {EnvParam.PluginName} plug-in?", Wrap = WrapMode.Word });
                layout.AddSeparateRow(m_LabelCounting);
                layout.AddSeparateRow(button_Yes, null, button_No);

                Content = layout;
            }

            protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
            {
                if (e.Cancel)
                    Result = GH_LoadingInstruction.Abort;
                base.OnClosing(e);
            }

            protected override async void OnShown(EventArgs e)
            {
                base.OnShown(e);

                int count = 20;
                for (int i = 0; i < count; i++)
                {
                    m_LabelCounting.Text = $"If you wait for {count - i} seconds, the plug-in is loaded.";
                    await Task.Delay(1000);
                }
                Close(GH_LoadingInstruction.Proceed);
            }
        }

        public override GH_LoadingInstruction PriorityLoad()
        {
            LoadSelector form = new LoadSelector();
            GH_LoadingInstruction result = form.ShowModal(Rhino.UI.RhinoEtoApp.MainWindow);

            if (result == GH_LoadingInstruction.Proceed)
            {
                Instances.CanvasCreated += AttatchmentSetUp;
                Instances.CanvasCreated += SetUpControlButton;
            }

            return result;
        }
    }
}
