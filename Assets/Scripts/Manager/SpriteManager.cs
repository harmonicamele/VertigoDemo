using System.Collections;
using System.Collections.Generic;
using Game.SingeltonSystem;
using Game.Sprites;

using UnityEngine;

public class SpriteManager : MonoSingleton<SpriteManager>
{
    
    [SerializeField] SpriteData ui_sprite_SpinnerData;
    [SerializeField] SpriteData ui_sprite_ItemData;
    [SerializeField] SpriteData ui_sprite_OtherData;
    
    
    // Start is called before the first frame update
    void Awake()
    {
       
    }
  
  public  SpriteData GetSpriteData(UiSpriteType type )
    {
        SpriteData spriteData = null;
        switch (type)
        {
            case UiSpriteType.Spinner:
                spriteData = ui_sprite_SpinnerData;
                break;
            case UiSpriteType.Item:
                spriteData = ui_sprite_ItemData;
                break;
            case UiSpriteType.Other:
                spriteData = ui_sprite_OtherData;
                break;

        }
      
        return spriteData;
    }
}
public enum UiSpriteType{
    Spinner,
    Item,
    Other,

}
