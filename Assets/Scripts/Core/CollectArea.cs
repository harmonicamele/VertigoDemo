using System;
using System.Collections;
using System.Collections.Generic;
using Game.EventSystem;
using Game.Sprites;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectArea : MonoBehaviour
{
    
    [Header(nameof(Image))]
    [SerializeField] private Image ui_image_collect_frame;
    [SerializeField] private Image ui_image_Exit_Button;
    [SerializeField] private Image ui_image_Exit_Button_frame;

    
    public Prize Prize;
    private List<int> itemDataId = new List<int>();
    private List<Prize> Prizelist = new List<Prize>();

    Prize obj;
    private SpriteData SpriteData => SpriteManager.Instance.GetSpriteData(UiSpriteType.Other);
    private void Awake()
    {
        SetImage();
    }
   
    private void SetImage()
    {
        ui_image_collect_frame.sprite = SpriteData.GetSprite(UIOthers.UIFrame);
        ui_image_Exit_Button_frame.sprite = SpriteData.GetSprite(UIOthers.UIFrame);
        ui_image_Exit_Button.sprite = SpriteData.GetSprite(UIOthers.UIcardZonelWhite);
    }
    private void OnEnable()
    {
        EventBus.GiveUp += Reset;
        EventBus.Selected += Selected;
    }
    private void OnDisable()
    {
        EventBus.GiveUp -= Reset;
        EventBus.Selected -= Selected;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log(obj);
            
        }
    }

    private void Selected(ItemData data)
    {
        if (!itemDataId.Contains(data.Item_id_value))
        {
            obj = Instantiate(Prize, this.transform);
            obj.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, itemDataId.Count * -40f);
            obj.PrizeImage.sprite = data.Item_icon_value;
            obj.itemvalue = data.Item_amount_value;
            obj.Text.text = data.Item_amount_value.ToString();
            obj.ItemData = data;
            itemDataId.Add(data.Item_id_value);
            Prizelist.Add(obj);
            Invoke(nameof(Finish), 1);

        }
        else
        {
           
            foreach (var item in Prizelist)
            {
                if (item.ItemData.Item_id_value == data.Item_id_value)
                {
                    item.Setvalue(data.Item_amount_value);
                    Invoke(nameof(Finish), 1);
                }
            }


        }
       
        

    }
    private void Finish()
    {
        EventBus.SpinFinish?.Invoke();
        EventBus.SpinReady?.Invoke();

    }



    public void Reset()
    {
        itemDataId.Clear();
        
        foreach (var item in Prizelist)
        {
            Destroy(item.gameObject);
        }
        Prizelist.Clear();
    }
}
