namespace Assets.Scripts.Interfaces
{
    public interface IBuildable : IProducible
    {

        BuildingType Type { get; }
        void Place(int x, int y);
    }
}