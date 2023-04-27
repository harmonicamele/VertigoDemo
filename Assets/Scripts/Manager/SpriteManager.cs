using System;
using System.Collections.Generic;
using Game.Data;
using Game.SingeltonSystem;
using UnityEngine;
namespace Game.Manager
{
    public class SpriteManager : MonoSingleton<SpriteManager>
    {
        public List<SpriteData> SpriteDataList = new List<SpriteData>();
        private Dictionary<int, SpriteData> dictionarylist = new Dictionary<int, SpriteData>();
        private Sprite sprite;

        public void Initialize()
        {
            foreach (var item in SpriteDataList)
            {
                dictionarylist.Add(item.SpriteId, item);
            }
        }
        public Sprite GetSprite(int id)
        {

            if (dictionarylist.ContainsKey(id))
            {
                sprite = dictionarylist[id].GetSprite();
            }
            else
            {
                throw new Exception("Sprite Id is wrong or null! Please check it from  " + typeof(SpriteData));
            }


            return sprite;
        }

    }
}



