using Grasshopper.Kernel;
using System;
using System.Drawing;

namespace DocumentationCanvas
{
    public class AssemblyInfo : GH_AssemblyInfo
    {
        public override string Name => EnvParam.PluginName;
        public override Bitmap Icon => null;
        public override string Description => "";

        public override Guid Id => new Guid("2c0a0db0-07d0-4675-84b7-e6a37ef076c3");
        public override string AuthorName => "Gaku Shinohara";
        public override string AuthorContact => "";
    }

    internal static class EnvParam
    {
        public static string PluginName => "DocumentationCanvas";
    }
}