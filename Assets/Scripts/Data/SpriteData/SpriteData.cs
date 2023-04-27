using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
namespace Game.Data
{
    [CreateAssetMenu]
    public class SpriteData : SpriteDataBase
    {
        
        public int SpriteId;
        [SerializeField] private Sprite sprite;
        [SerializeField] private SpriteAtlas spriteAtlas;
        protected override Sprite Sprite()
        {
            return sprite;
        }

        protected override SpriteAtlas SpriteAtlas()
        {
            return spriteAtlas;
        }

    }
}


