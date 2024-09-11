namespace DocumentationCanvas.Objects
{
    public class ActivationButton : DocumentationObject<AttatchmentObject>
    {
        public bool IsOpen { get; set; }

        public ActivationButton(AttatchmentObject obj) : base(obj)
        {
            IsOpen = false;
        }

        protected override void CreateAttributes()
        {
            Attributes = new ActivationButtonAttributes(this);
        }
    }
}
