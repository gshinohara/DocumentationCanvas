namespace DocumentationCanvas.Objects
{
    public interface IDocumentationObject
    {
        event PostValidityChangedEventHandler PostValidityChanged;

        bool IsValid { get; set; }

        IDocumentationObjectAttributes Attributes { get; }
    }
}