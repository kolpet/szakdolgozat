namespace Szakdolgozat.Persistence.Structures
{
    public interface IAlgorithmSaveElement
    {
        void Accept(IAlgorithmSaveVisitor visitor);
    }
}