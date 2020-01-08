namespace Assets.Scripts.Interfaces
{
    public interface IBuildable : IProducible
    {

        void Place(int x, int y);
    }
}