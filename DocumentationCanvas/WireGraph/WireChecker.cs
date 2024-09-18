using DocumentationCanvas.WireGraph.AlertDialogue;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Data;
using Grasshopper.Kernel.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using WireEventImplementor;

namespace DocumentationCanvas.WireGraph
{
    internal static class WireChecker
    {
        public static void AlertHeavySolution(WireStatus wireStatus)
        {
            const int pathCountAlerted = 100;

            bool isNoPaths_Left = wireStatus.PreviousSideParam.VolatileData.PathCount == 0;
            bool isNoPaths_Right = wireStatus.SubsequentSideParam.VolatileData.PathCount == 0;
            bool isExistent = wireStatus.SubsequentSideParam.Sources.Contains(wireStatus.PreviousSideParam);

            bool isSafeMatching = false;
            string message = string.Empty;
            switch (wireStatus.LinkMode)
            {
                case LinkMode.Replace:
                    isSafeMatching = true;
                    break;
                case LinkMode.Add:
                    int count_Add = wireStatus.SubsequentSideParam.VolatileData.Paths.Union(wireStatus.PreviousSideParam.VolatileData.Paths).Distinct().Count() - wireStatus.SubsequentSideParam.VolatileData.PathCount;
                    isSafeMatching = isNoPaths_Left || isNoPaths_Right || isExistent;
                    isSafeMatching = isSafeMatching && count_Add < pathCountAlerted;
                    message = $"{count_Add} of paths are going to be added.";
                    break;
                case LinkMode.Remove:
                    List<GH_Path> list_before = new List<GH_Path>();
                    List<GH_Path> list_after = new List<GH_Path>();
                    foreach (IGH_Param source in wireStatus.SubsequentSideParam.Sources)
                    {
                        list_before.AddRange(source.VolatileData.Paths);
                        if (source != wireStatus.PreviousSideParam)
                        list_after.AddRange(source.VolatileData.Paths);
                    }
                    int count_Remove = list_before.Distinct().Count() - list_after.Distinct().Count();
                    isSafeMatching = !isExistent || (isExistent && count_Remove < pathCountAlerted);
                    message = $"{count_Remove} of paths are going to be removed.";
                    break;
            }

            //Alert merging datatree is not good.
            if (!isSafeMatching)
                new AlertPathMerging(wireStatus, message).ShowModal(Rhino.UI.RhinoEtoApp.MainWindow);

            //Alert when inputting to a component.
            if (wireStatus.SubsequentSideParam.Attributes.GetTopLevel.DocObject is GH_Component component)
            {

            }
        }
    }
}
