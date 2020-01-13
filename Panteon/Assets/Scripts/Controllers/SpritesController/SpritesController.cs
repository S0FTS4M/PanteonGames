using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class SpritesController : MonoBehaviour
{
    public static SpritesController instance;

    Dictionary<string, Sprite> _nameToSpritesMap;

    // Start is called before the first frame update
    void OnEnable()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        _nameToSpritesMap = new Dictionary<string, Sprite>();
        LoadSprites();
    }

    void LoadSprites()
    {
        SpriteAtlas spriteAtlas = Resources.Load<SpriteAtlas>("Sprites/Sprites");
        Sprite[] sprites = new Sprite[spriteAtlas.spriteCount];
        spriteAtlas.GetSprites(sprites);

        foreach (var sprite in sprites)
        {
            _nameToSpritesMap.Add(
                sprite.name.Replace("(Clone)", ""),
                sprite);
        }
    }

    public Sprite GetSpriteByName(string spriteName)
    {
        return _nameToSpritesMap.ContainsKey(spriteName) ? _nameToSpritesMap[spriteName] : null;
    }
}
