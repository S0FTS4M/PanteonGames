using System.Collections.Generic;

namespace Assets.Scripts.Interfaces
{
    public interface IProducer
    {
        Tile SpawnPointTile { get; }
        List<MoveableUnitBase> MoveableUnitPrototypes { get; }
        void SelectSpawnPoint(Border xBorders, Border yBorders);
        UnitBase Produce(int index);
    }
}
