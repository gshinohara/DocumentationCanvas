namespace DocumentationCanvas.Objects
{
    internal abstract class DocumentationObject<T> : IDocumentationObject
    {
        public object Tag { get; set; }

        public T LinkedObject { get; }

        public IDocumentationObjectAttributes Attributes { get; protected set; }

        public DocumentationObject(T obj)
        {
            LinkedObject = obj;

            CreateAttributes();
            AfterAttributesCreated();
        }

        protected abstract void CreateAttributes();

        protected virtual void AfterAttributesCreated()
        {
        }
    }
}
