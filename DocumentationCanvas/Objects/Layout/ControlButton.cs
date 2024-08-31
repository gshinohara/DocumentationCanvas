using System;
using System.Drawing;
using System.Windows.Forms;

namespace DocumentationCanvas.Objects.Layout
{
    internal class ControlButton : DocumentationObject<FrameLayout>
    {
        public string Text {  get; set; }

        public event EventHandler<Canvas_MouseEventArg> MouseMove;

        public event EventHandler<Canvas_MouseEventArg> MouseDown;

        public event EventHandler<Canvas_MouseEventArg> MouseUp;

        public ControlButton(FrameLayout obj, string text) : base(obj)
        {
            Text = text;
        }

        protected override void CreateAttributes()
        {
            Attributes = new ControlButtonAttributes(this);
        }

        public void OnMouseMove(Canvas_MouseEventArg e)
        {
            ControlButtonAttributes att = Attributes as ControlButtonAttributes;
            Color color;
            if (e.Button == MouseButtons.Left)
                color = att.Color;
            else if (att.Bounds.Contains(e.CanvasLocation))
                color = Color.FromArgb(att.Color.A, Color.LightYellow);
            else
                color = Color.FromArgb(att.Color.A, Color.White);

            if (att.Color != color)
            {
                att.Color = color;
                e.Canvas.Refresh();
            }

            MouseMove?.Invoke(this, e);
        }

        public void OnMouseDown(Canvas_MouseEventArg e)
        {
            ControlButtonAttributes att = Attributes as ControlButtonAttributes;
            if (att.Bounds.Contains(e.CanvasLocation))
            {
                if (e.Button == MouseButtons.Left)
                {
                    att.Color = Color.FromArgb(att.Color.A, Color.DarkGray);
                    e.Canvas.Refresh();
                }

                MouseDown?.Invoke(this, e);
            }
        }

        public void OnMouseUp(Canvas_MouseEventArg e)
        {
            ControlButtonAttributes att = Attributes as ControlButtonAttributes;
            att.Color = Color.FromArgb(att.Color.A, Color.White);
            e.Canvas.Refresh();

            if (att.Bounds.Contains(e.CanvasLocation))
                MouseUp?.Invoke(this, e);
        }
    }
}