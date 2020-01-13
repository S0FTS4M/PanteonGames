using System.Collections;

namespace Assets.Scripts.Interfaces
{
    public interface IMoveable
    {

        IEnumerable Move(Tile destinationTile);
    }
}
