using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using UnityEngine;

public class MoveableUnitcontroller : MonoBehaviour, IMoveable
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerable Move(Tile destinationTile)
    {
        float x, y;
        x = destinationTile.X - transform.position.x;
        y = destinationTile.Y - transform.position.y;
        while (x != 0 && y != 0)
        {
            if (x == 0)
                transform.Translate(new Vector3(0, Time.deltaTime, 0));
            else if (y == 0)
                transform.Translate(new Vector3(Time.deltaTime, 0, 0));
            yield return null;

        }
    }
}
