using System;
using System.Drawing;
using System.Windows.Forms;

namespace DocumentationCanvas.Objects.Layout
{
    internal class ControlButton : DocumentationObject<FrameLayout>
    {
        public string Text {  get; set; }

        public ControlButton(FrameLayout obj, string text) : base(obj)
        {
            Text = text;
        }

        protected override void CreateAttributes()
        {
            Attributes = new ControlButtonAttributes(this);
        }
    }
}