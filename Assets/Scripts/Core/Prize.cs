using System.Collections;
using System.Collections.Generic;
using Game.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Game.Core
{
    public class Prize : MonoBehaviour
    {
        public Image PrizeImage;
        public int Itemvalue;
        public TextMeshProUGUI Text;
        public ItemData ItemData;

        public void Setvalue(int value)
        {

            Itemvalue = Itemvalue + value;
            Text.text = Itemvalue.ToString();


        }
    }
}

