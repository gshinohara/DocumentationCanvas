using Grasshopper;
using Grasshopper.Kernel;

namespace DocumentationCanvas.AssemblyPriority
{
    public class PluginStarter : GH_AssemblyPriority
    {
        public override GH_LoadingInstruction PriorityLoad()
        {
            LoadSelector selector = new LoadSelector();
            GH_LoadingInstruction result = selector.ShowModal(Rhino.UI.RhinoEtoApp.MainWindow);

            if (result == GH_LoadingInstruction.Proceed)
            {
                Instances.CanvasCreated += canvas =>
                {
                    new SetUpWireEvent().Subscribe(canvas);
                    new SetUpAttatchmentObject().Subscribe(canvas);
                    new SetUpTimeLineDashboard().Subscribe(canvas);
                };
            }

            return result;
        }
    }
}