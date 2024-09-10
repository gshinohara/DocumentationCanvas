using CustomGrip.Grips;
using System.Drawing;

namespace DocumentationCanvas.TimeLineDashboard
{
    public class DisplayObjectInputGrip : WiringObjectInputGrip<DisplayTarget>
    {
        public override SizeF TargetDirection => new SizeF(0, -50);

        public DisplayObjectInputGrip(DisplayObjectAttributes parent) : base(parent, 0, 180f)
        {
        }
    }
}
