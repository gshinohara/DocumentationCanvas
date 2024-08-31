using CustomGrip;
using DocumentationCanvas.Objects;
using System;
using System.Drawing;

namespace DocumentationCanvas
{
    public class DisplayObjectInputGrip : WiringObjectInputGrip<Attatchment>
    {
        public DisplayObjectInputGrip() : base(0, 180f)
        {
        }
    }
}
