using System.Collections;
using System.Collections.Generic;
using Game.Core;
using UnityEngine;
namespace Game.Data
{
    [CreateAssetMenu]
    public class WheelSpinnerData : ScriptableObject
    {
        [SerializeField] private List<ItemData> NormalItemList = new List<ItemData>();
        [SerializeField] private List<ItemData> SafeItemList = new List<ItemData>();
        [SerializeField] private List<ItemData> SuperItemList = new List<ItemData>();


        public int NumberOfSlice;
        [Range(1, 5)]
        public int SpeedMultiplier;
        [Range(2, 10)]
        public int Duration;
        public bool TimedTurn;
        public AnimationCurve animationCurve;

        public List<ItemData> ItemDatalist(int index)
        {
            if (index == 2)
            {
                return SuperItemList;
            }
            else if (index == 1)
            {
                return SafeItemList;
            }
            else
            {
                return NormalItemList;
            }
        }
    }
}

