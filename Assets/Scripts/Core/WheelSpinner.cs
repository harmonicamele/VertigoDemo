using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game.EventSystem;
using Game.Sprites;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WheelSpinner : MonoBehaviour
{
    private const float ratioForSquare = 12f;
    private const float ratioForRectangle = 6.5f;
    private const int circleAngle = 360;
    private const int zoneSilverIndex = 5;
    private const int zoneGoldenIndex = 30;

    [SerializeField] private Transform wheelToRotate;
    public List<ItemPointer> ItemPoint = new List<ItemPointer>();

    public Button SpinButton;
    [Header("Image")]
    [Space(10)]
    [SerializeField] private Image ui_image_spin_Button;
    [SerializeField] private Image ui_image_spin_CurrentBg;
    [SerializeField] private Image ui_image_CurrentItem;
    [SerializeField] private Image ui_image_Spinner;
    [SerializeField] private Image ui_image_Indicator;
    [Space(10)]
    [Header("Text")]
    [SerializeField] private TextMeshProUGUI SpinText;
    [SerializeField] private TextMeshProUGUI CurrentValue;
   
    bool spinning;

    [Header("Data")]
    [SerializeField] private WheelSpinnerData SpinnerData;
    private ItemData SelectedItem;
    private List<ItemData> Itemlist = new List<ItemData>();

    private int numberofSlice => SpinnerData.NumberOfSlice;
    private EventBus eventBus;
    private float anglePerReward;  
    private int targetToStopOn;
    private void OnEnable()
    {
        eventBus.Subscribe<GameEvents.GiveUp>(Reset);
        eventBus.Subscribe<GameEvents.ZoneType>(SpinType);
        eventBus.Subscribe<GameEvents.SpinReady>(ReadySpin);    
    }  

    private void OnDisable()
    {
        eventBus.Unsubscribe<GameEvents.GiveUp>(Reset);
        eventBus.Unsubscribe<GameEvents.ZoneType>(SpinType);
        eventBus.Unsubscribe<GameEvents.SpinReady>(ReadySpin);
    }
   
    private void Reset()
    {
        SilveSpin();
        ItemGenerate();
    }
    private void Awake()
    {
        eventBus = EventBus.Instance;
        spinning = false;   
        anglePerReward = circleAngle / numberofSlice;
        turndirection = SpinnerData.TimedTurn ? -1 : 1;
        ui_image_spin_Button.sprite = SpriteManager.Instance.GetSprite(5);
        ui_image_spin_CurrentBg.sprite = SpriteManager.Instance.GetSprite(6);
        SpinButton.onClick.AddListener(OnclickSpin);
        SilveSpin();
        
        ItemGenerate();
    }
   

    private void SpinType(GameEvents.ZoneType zoneType)
    {
        var zoneIndex = zoneType.ZoneIndex;
        if (zoneIndex % zoneSilverIndex == 0 || zoneIndex == 1)
        {
            if (zoneIndex%zoneGoldenIndex==0)
            {
                Itemlist = SpinnerData.ItemDatalist(2);
                ItemGenerate();
                ui_image_Spinner.sprite = SpriteManager.Instance.GetSprite(7);
                ui_image_Indicator.sprite = SpriteManager.Instance.GetSprite(8);
                SpinText.text = "GOLDEN SPIN";
                SpinText.color = Color.yellow;

            }
            else
            {

                SilveSpin();
            }
        }
        else
        {
            Itemlist = SpinnerData.ItemDatalist(0);
            ItemGenerate();
            ui_image_Spinner.sprite = SpriteManager.Instance.GetSprite(9);
            ui_image_Indicator.sprite = SpriteManager.Instance.GetSprite(10);
            SpinText.text = "NORMAL SPIN";
            SpinText.color = Color.white;
        }

    }

   private void SilveSpin()
    {
        Itemlist = SpinnerData.ItemDatalist(1);
        ItemGenerate();
        ui_image_Spinner.sprite = SpriteManager.Instance.GetSprite(11);
        ui_image_Indicator.sprite = SpriteManager.Instance.GetSprite(12);
        SpinText.text = "SILVER SPIN";
        SpinText.color = Color.gray;
    }
  
    private void ItemGenerate()
    {
        for (int i = 0; i < numberofSlice; i++)
        {
            ItemPoint[i].ui_item_Icon.sprite = SpriteManager.Instance.GetSprite(Itemlist[i].Item_sprite_index ); 
            ItemScaleNativeSize(i);  
            if (!Itemlist[i].Item_isBomb_value)
            {
                ItemPoint[i].item_Text.text = "x" + Itemlist[i].Item_amount_value.ToString();
            }
            else
            {
                ItemPoint[i].item_Text.text = "";
                ItemPoint[i].item_Icon_rectTransform.sizeDelta=new Vector2(65,65);
                ItemPoint[i].item_Icon_rectTransform.anchoredPosition = new Vector2(0, 0);

            }
           
        }
    }
  
    private void OnclickSpin()
    {
       
        StartSpin();
        
    }
    bool indicaotoractive=true;
    int indexs;
    int turndirection=1;
    private void StartSpin()
    {
        if (!spinning)
        {
            targetToStopOn = UnityEngine.Random.Range(0, numberofSlice);
            
            float maxAngle =  circleAngle * SpinnerData.SpeedMultiplier + (targetToStopOn * anglePerReward);

            spinning = true;
            SpinButton.gameObject.SetActive(false);
          
            wheelToRotate.DORotate(new Vector3(0,0,maxAngle) * turndirection, SpinnerData.Duration, RotateMode.LocalAxisAdd).SetEase(SpinnerData.animationCurve).
                OnUpdate(() => {

                    OnUpdateSpin();
                }).OnComplete(() =>{ OnCompletedSpin();}
                ); ;



        }
    }

    private void OnUpdateSpin()
    {
        float angle = wheelToRotate.eulerAngles.z;
        var index = (int)(CurrentTarget(angle));
        ui_image_CurrentItem.sprite = SpriteManager.Instance.GetSprite(Itemlist[index].Item_sprite_index);
        if (!Itemlist[index].Item_isBomb_value)
        {
            CurrentValue.text = "X" + Itemlist[index].Item_amount_value.ToString();
        }
        else
        {
            CurrentValue.text = null;
        }
    }

    private void OnCompletedSpin()
    {
        indexs = CurrentTarget(wheelToRotate.eulerAngles.z + 10);

        SelectedItem = Itemlist[indexs];
        ui_image_CurrentItem.sprite = SpriteManager.Instance.GetSprite(SelectedItem.Item_sprite_index);

        if (!SelectedItem.Item_isBomb_value)
        {
            CurrentValue.text = "X" + SelectedItem.Item_amount_value.ToString();
        }
        else
        {
            CurrentValue.text = "Bomb";
        }



        spinning = false;

        if (!Itemlist[indexs].Item_isBomb_value)
        {
            eventBus.Fire(new GameEvents.Selected(SelectedItem));
        }
        else
        {
            eventBus.Fire(new GameEvents.Bomb());
            Invoke(nameof(ReadySpin), 1);

        }
    }
 

   private void ReadySpin()
    {
       SpinButton.gameObject.SetActive(true);
      
    }

    int selectedindex;
    private int CurrentTarget(float angle)
    {      
       int index = (int)(angle / (anglePerReward));       
        return index;
    }

    
    private void ItemScaleNativeSize(Index i)
    {
        ItemPoint[i].ui_item_Icon.SetNativeSize();
        
        if (ItemPoint[i].item_Icon_rectTransform.sizeDelta.x == ItemPoint[i].item_Icon_rectTransform.sizeDelta.y)
        {
            ItemPoint[i].item_Icon_rectTransform.sizeDelta = ItemPoint[i].item_Icon_rectTransform.sizeDelta / ratioForSquare;
        }
        else
        {
            ItemPoint[i].item_Icon_rectTransform.sizeDelta = ItemPoint[i].item_Icon_rectTransform.sizeDelta / ratioForRectangle;
        }
    }

}
