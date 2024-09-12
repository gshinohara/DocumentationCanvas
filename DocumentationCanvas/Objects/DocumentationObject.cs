namespace DocumentationCanvas.Objects
{
    public abstract class DocumentationObject<T> : IDocumentationObject
    {
        private bool m_IsValid;

        public bool IsValid
        {
            get => m_IsValid;
            set
            {
                m_IsValid = value;
                PostValidityChanged?.Invoke();
            }
        }

        public event PostValidityChangedEventHandler PostValidityChanged;

        public object Tag { get; set; }

        public T LinkedObject { get; }

        public IDocumentationObjectAttributes Attributes { get; protected set; }

        public DocumentationObject(T obj)
        {
            LinkedObject = obj;

            CreateAttributes();
        }

        protected abstract void CreateAttributes();
    }
}
