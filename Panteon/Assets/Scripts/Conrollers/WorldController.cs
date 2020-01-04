using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    [SerializeField]
    Sprite tileSprite;

    [SerializeField]
    Transform worldParent;

    public World World { get; private set; }



    void Awake()
    {
        World = new World();

        //create a gameobject for every tile data and set the position.
        for (int x = 0; x < World.Width; x++)
        {
            for (int y = 0; y < World.Height; y++)
            {
                Tile tile = World.GetTileAt(x, y);
                GameObject tileGO = new GameObject($"tile_{x}_{y}");
                tileGO.transform.SetParent(worldParent, true);
                tileGO.transform.position = new Vector3(x, y, 0);

                //we may wanna do this somewhere else 
                SpriteRenderer tile_sp = tileGO.AddComponent<SpriteRenderer>();
                tile_sp.sprite = tileSprite;

            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
