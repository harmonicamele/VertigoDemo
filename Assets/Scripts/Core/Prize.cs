using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Prize : MonoBehaviour
{
    public Image PrizeImage;
    public int itemvalue;
    public TextMeshProUGUI Text;
    public ItemData ItemData;

    public void Setvalue(int value)
    {
      
        itemvalue = itemvalue + value; 
        Text.text = itemvalue.ToString();
    

    }
    //public Prize(Sprite icon,int _itemvalue,ItemData _itemData)
    //{
    //    itemvalue = _itemvalue;
    //    ItemData = _itemData;
    //    PrizeImage.sprite = icon;
    //    Text.text = _itemData.ToString();
    //}
    
}
