namespace Assets.Scripts.Interfaces
{
    public interface IFactory
    {
        UnitBase Create(UnitType unitType);
    }
}