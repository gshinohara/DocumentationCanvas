namespace DocumentationCanvas.Objects
{
    internal abstract class DocumentationObject<T> : IDocumentationObject
    {
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
