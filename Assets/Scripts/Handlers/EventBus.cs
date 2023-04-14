using System;
using UnityEngine;
namespace Game.EventSystem
{
    public class EventBus : MonoBehaviour
    {
        public static Action<ItemData> Selected;
        public static Action SpinReady;
        public static Action SpinFinish;
        public static Action<int> ZoneType;
        public static Action GiveUp;
        public static Action Bomb;
    }
}
  


