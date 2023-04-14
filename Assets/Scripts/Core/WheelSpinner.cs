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
     private SpriteData SpriteData=>SpriteManager.Instance.GetSpriteData(UiSpriteType.Spinner);
    private ItemData SelectedItem;
    private List<ItemData> Itemlist = new List<ItemData>();



    private float anglePerReward;  
    private int targetToStopOn;
    private void OnEnable()
    {
        EventBus.GiveUp += Reset;
        EventBus.ZoneType += SpinType;
        EventBus.SpinReady += ReadySpin;
    }  

    private void OnDisable()
    {
        EventBus.GiveUp -= Reset;
        EventBus.ZoneType -= SpinType;
        EventBus.SpinReady -= ReadySpin;
    }
   
    private void Reset()
    {
        SpinType(1);
        ItemGenerate();
    }
    private void Awake()
    {
        spinning = false;   
        anglePerReward = 360 / 8;
        turndirection = SpinnerData.timedTurn ? -1 : 1;
        ui_image_spin_Button.sprite = SpriteData.GetSprite(UISpinner.SpinBtn);
        ui_image_spin_CurrentBg.sprite = SpriteData.GetSprite(UISpinner.CurrentBg);
        SpinButton.onClick.AddListener(OnclickSpin);
        SpinType(1);
        
        ItemGenerate();
    }
    

    private void SpinType(int zoneIndex)
    {
        if (zoneIndex % 5 == 0 || zoneIndex == 1)
        {
            if (zoneIndex%30==0)
            {
                Itemlist = SpinnerData.ItemDatalist(2);
                ItemGenerate();
                ui_image_Spinner.sprite = SpriteData.GetSprite(UISpinner.GoldSpin);
                ui_image_Indicator.sprite = SpriteData.GetSprite(UISpinner.GoldIndicator);
                SpinText.text = "GOLDEN SPIN";
                SpinText.color = Color.yellow;

            }
            else
            {

                Itemlist = SpinnerData.ItemDatalist(1);
                ItemGenerate();
                ui_image_Spinner.sprite = SpriteData.GetSprite(UISpinner.SilverSpin);
                ui_image_Indicator.sprite = SpriteData.GetSprite(UISpinner.SilverIndicator);
                SpinText.text = "SILVER SPIN";
                SpinText.color = Color.gray;
            }
        }
        else
        {
            Itemlist = SpinnerData.ItemDatalist(0);
            ItemGenerate();
            ui_image_Spinner.sprite = SpriteData.GetSprite(UISpinner.BronzSpin);
            ui_image_Indicator.sprite = SpriteData.GetSprite(UISpinner.BronzIndicator);
            SpinText.text = "NORMAL SPIN";
            SpinText.color = Color.white;
        }

    }
   
  
    private void ItemGenerate()
    {
        for (int i = 0; i < 8; i++)
        {
            ItemPoint[i].ui_item_Icon.sprite = Itemlist[i].Item_icon_value;
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
            targetToStopOn = UnityEngine.Random.Range(0, 8);
            
            float maxAngle =  360 * SpinnerData.speedMultiplier + (targetToStopOn * anglePerReward);

            spinning = true;
            SpinButton.gameObject.SetActive(false);
          
            wheelToRotate.DORotate(new Vector3(0,0,maxAngle) * turndirection, SpinnerData.duration, RotateMode.LocalAxisAdd).SetEase(SpinnerData.animationCurve).
                OnUpdate(() => {
                    float angle = wheelToRotate.eulerAngles.z;
                    var index = (int)(CurrentTarget(angle));
                   
                    ui_image_CurrentItem.sprite = Itemlist[index].Item_icon_value;
                    if (!Itemlist[index].Item_isBomb_value)
                    {
                        CurrentValue.text = "X" + Itemlist[index].Item_amount_value.ToString();
                    }
                    else
                    {
                        CurrentValue.text = null;
                    }
                    
                }).
                    OnComplete(() =>
                    {

                        
                        indexs = CurrentTarget(wheelToRotate.eulerAngles.z+10);
                 
                        SelectedItem = Itemlist[indexs];
                        ui_image_CurrentItem.sprite = SelectedItem.Item_icon_value;
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
                           
                           EventBus.Selected?.Invoke(SelectedItem);
                          
                        }
                        else
                        {
                            Debug.Log("Bomb");
                            EventBus.Bomb?.Invoke();
                            Invoke(nameof(ReadySpin), 1);

                        }
                       
                     
                    }
                    );;

           
        }
    }


 

   private void ReadySpin()
    {
       SpinButton.gameObject.SetActive(true);
      
    }

    int selectedindex;
    private int CurrentTarget(float angle)
    {
        float index = angle switch
        {
            float n when n>= 0 && n <45 => 0,
            float n when n >= 45 && n < 90 => 1,
            float n when n >= 90 && n < 135 => 2,
            float n when n >= 135 && n < 180 => 3,
            float n when n >= 180 && n < 225 => 4,
            float n when n >= 225 && n < 270 => 5,
            float n when n >= 270 && n < 315 => 6,
            _ => 7
        };
        return (int)index;
    }

    private void ItemScaleNativeSize(Index i)
    {
        ItemPoint[i].ui_item_Icon.SetNativeSize();
        
        if (ItemPoint[i].item_Icon_rectTransform.sizeDelta.x == ItemPoint[i].item_Icon_rectTransform.sizeDelta.y)
        {
            ItemPoint[i].item_Icon_rectTransform.sizeDelta = ItemPoint[i].item_Icon_rectTransform.sizeDelta / 12f;
        }
        else
        {
            ItemPoint[i].item_Icon_rectTransform.sizeDelta = ItemPoint[i].item_Icon_rectTransform.sizeDelta / 6.5f;
        }
    }

}
