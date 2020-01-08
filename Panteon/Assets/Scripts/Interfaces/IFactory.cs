namespace Assets.Scripts.Interfaces
{
    public interface IFactory
    {
        IBuildable Create(ProducibleType producibleType);
    }
}