using Game.Data;
using UnityEngine;

namespace Game.EventSystem
{
    public class GameEvents : MonoBehaviour
    {      
        public struct Selected : IEvent
        {
            public ItemData ItemData;
            public Selected(ItemData itemData)
            {
                this.ItemData = itemData;
            }
        }
        public struct SpinReady : IEvent { }
        public struct SpinFinish : IEvent { }
        public struct ZoneType : IEvent
        {
            public int ZoneIndex;
            public ZoneType(int zoneIndex)
            {
                this.ZoneIndex = zoneIndex;
            }
        }
        public struct GiveUp : IEvent { }
        public struct Bomb : IEvent { }
    }

}
