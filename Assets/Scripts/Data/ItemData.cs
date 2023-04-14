using System.Collections;
using System.Collections.Generic;
using Game.Sprites;
using UnityEngine;
[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    [SerializeField] private SpriteData item_sprite_data;
    [SerializeField] private int item_id_value;
    [SerializeField] UIItemsType item_icon_type;
    [SerializeField] private int item_amount_value;  
    [SerializeField] private bool item_isBomb_value;
  

   
    public Sprite Item_icon_value { get => item_sprite_data.GetSprite(item_icon_type);  }
    public int Item_amount_value { get => item_amount_value; }
    public int Item_id_value { get => item_id_value; }
    public bool Item_isBomb_value { get => item_isBomb_value;}
}
public enum UIItemsType
{
    Money,
    Gold,
    Electric,
    M67,
    Snowball,
    HealthShot2,
    Adrenaline,
    Easter,
    C4,
    Emp

}