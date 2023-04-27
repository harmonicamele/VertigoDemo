using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

namespace Game.Sprites
{
    public abstract class SpriteDataBase : ScriptableObject
    {

        protected abstract SpriteAtlas SpriteAtlas();
        protected abstract Sprite Sprite();

        public virtual Sprite GetSprite()
        {
            return SpriteAtlas().GetSprite(Sprite().name);

        }

    }

}