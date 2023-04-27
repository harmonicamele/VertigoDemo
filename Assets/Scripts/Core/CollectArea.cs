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
    private EventBus eventBus;
   // private SpriteData SpriteData => SpriteManager.Instance.GetSpriteData(UiSpriteType.Other);
    private void Awake()
    {
        SetImage();
    }
   
    private void SetImage()
    {
        eventBus = EventBus.Instance;
        ui_image_collect_frame.sprite = SpriteManager.Instance.GetSprite(2);
        ui_image_Exit_Button_frame.sprite = SpriteManager.Instance.GetSprite(2);
        ui_image_Exit_Button.sprite = SpriteManager.Instance.GetSprite(4);
    }
    private void OnEnable()
    {
        eventBus.Subscribe<GameEvents.GiveUp>(Reset);
        eventBus.Subscribe<GameEvents.Selected>(Selected);
       
    }
    private void OnDisable()
    {
        eventBus.Unsubscribe<GameEvents.GiveUp>(Reset);
        eventBus.Unsubscribe<GameEvents.Selected>(Selected);
    }

   

    private void Selected(GameEvents.Selected selected)
    {
        var data = selected.ItemData;
        if (!itemDataId.Contains(data.Item_id_value))
        {
            obj = Instantiate(Prize, this.transform);
            obj.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, itemDataId.Count * -40f);
            obj.PrizeImage.sprite = SpriteManager.Instance.GetSprite(data.Item_sprite_index);
            obj.Itemvalue = data.Item_amount_value;
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
        eventBus.Fire(new GameEvents.SpinFinish());
        eventBus.Fire(new GameEvents.SpinReady());
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
