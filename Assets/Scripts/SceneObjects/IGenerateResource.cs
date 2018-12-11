namespace SceneObjects
{
    public interface IGenerateResource
    {
        void GenerateResource(ResourceType type, float value);
    }

    public enum ResourceType
    {
        Gold,
        Brick,
        Science
    }
}