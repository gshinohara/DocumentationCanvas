using DocumentationCanvas.WireGraph.AlertDialogue;
using Grasshopper.Kernel;
using System;
using System.Linq;
using WireEventImplementor;

namespace DocumentationCanvas.WireGraph
{
    internal static class WireChecker
    {
        public static void AlertHeavySolution(WireStatus wireStatus)
        {
            IGH_Param inputting = wireStatus.PreviousSideParam;

            if (wireStatus.SubsequentSideParam.Attributes.GetTopLevel.DocObject is GH_Component component)
            {
                foreach (IGH_Param param in component.Params.Input)
                {
                    if (param == wireStatus.SubsequentSideParam)
                    {
                        //check
                        bool isNoPaths = param.VolatileData.PathCount > 0;
                        bool isExistent = param.Sources.Contains(inputting);
                        bool isSafeWireSolution = !isExistent || wireStatus.LinkMode == LinkMode.Replace || wireStatus.LinkMode == LinkMode.Remove;
                        bool isTooSpilt = inputting.VolatileData.Paths.Count(p => !param.VolatileData.PathExists(p)) > 100;

                        //Alert merging datatree is not good.
                        if (!isNoPaths && !isSafeWireSolution || isTooSpilt)
                            new AlertPathMerging(wireStatus).ShowModal(Rhino.UI.RhinoEtoApp.MainWindow);
                    }
                    else
                    {
                        //Alert by each parameter access.
                    }
                }
            }
        }
    }
}
