namespace Assets.Scripts.Interfaces
{
    public interface ICanProduce
    {
        IProducible producible { get; }
        IMoveable Produce();
    }
}
