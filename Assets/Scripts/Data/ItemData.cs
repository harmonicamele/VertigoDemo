
using UnityEngine;
namespace Game.Data
{
    [CreateAssetMenu]
    public class ItemData : ScriptableObject
    {
        [SerializeField] private SpriteData item_sprite_data;
        [SerializeField] private int item_id_value;
        [SerializeField] UIItemsType item_icon_type;
        [SerializeField] private int item_amount_value;
        [SerializeField] private bool item_isBomb_value;

        private const int addindex = 13;
        public int Item_amount_value { get => item_amount_value; }
        public int Item_id_value { get => item_id_value; }
        public bool Item_isBomb_value { get => item_isBomb_value; }
        public int Item_sprite_index
        {
            get => (int)item_icon_type + addindex;
        }
    }
    public enum UIItemsType
    {
        Money = 0,
        Gold = 1,
        Electric = 2,
        M67 = 3,
        Snowball = 4,
        HealthShot2 = 5,
        Adrenaline = 6,
        Easter = 7,
        C4 = 8,
        Emp = 9

    }
}
