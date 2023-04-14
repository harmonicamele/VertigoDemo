using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

namespace Game.Sprites
{
    public abstract class SpriteDataBase : ScriptableObject
    {
      
            protected abstract SpriteAtlas SpriteAtlas();
            [SerializeField] private string SpritesGroupName;
            [SerializeField] private List<string> SpriteNames = new List<string>();
            public virtual Sprite GetSprite(Enum type)
            {
                Type enumType = type.GetType();
                int index = (int)Enum.Parse(enumType, type.ToString());

                if (SpriteNames.Count != Enum.GetNames(enumType).Length)
                {
                    throw new Exception("SpriteNames Index was out of range.Check SpriteNames array .The corresponding enums count  must be equal to the number of its!");

                }
                else
                {
                    if (SpriteAtlas().GetSprite(SpriteNames[index]) == null)
                    {
                        throw new Exception("Sprite name is wrong or null! Please check it from  " + SpriteAtlas().name);
                    }
                    else
                    {
                        return SpriteAtlas().GetSprite(SpriteNames[index]);
                    }
                }


            }

    }
    

}