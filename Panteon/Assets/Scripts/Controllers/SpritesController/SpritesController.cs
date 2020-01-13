using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class SpritesController : MonoBehaviour
{
    public static SpritesController Instance;

    Dictionary<string, Sprite> _nameToSpritesMap;

    // Start is called before the first frame update
    void OnEnable()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        _nameToSpritesMap = new Dictionary<string, Sprite>();
        LoadSprites();
    }

    void LoadSprites()
    {
        SpriteAtlas spriteAtlas = Resources.Load<SpriteAtlas>("Sprites/Sprites");
        Sprite[] sprites = new Sprite[spriteAtlas.spriteCount];
        spriteAtlas.GetSprites(sprites);
        for (int i = 0; i < sprites.Length; i++)
        {

            _nameToSpritesMap.Add(
                sprites[i].name.Replace("(Clone)", ""),
                sprites[i]);

        }
    }

    public Sprite GetSpriteByName(string spriteName)
    {
        if (_nameToSpritesMap.ContainsKey(spriteName))
            return _nameToSpritesMap[spriteName];


        return null;
    }
}
