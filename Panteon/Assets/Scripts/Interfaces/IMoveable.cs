using System.Collections;

namespace Assets.Scripts.Interfaces
{
    public interface IMoveable
    {
        IEnumerable Move(int x, int y);
    }
}
