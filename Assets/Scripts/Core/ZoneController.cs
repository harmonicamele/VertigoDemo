using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game.EventSystem;
using Game.Sprites;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ZoneController : MonoBehaviour
{
    
    
    private Queue<GameObject> ZoneTextList = new Queue<GameObject>();
    public GameObject ZoneText;
    public RectTransform MidpointTransform;

    [Header(nameof(Text))]
    [SerializeField] private TextMeshProUGUI NextSafeZone;
    [SerializeField] private TextMeshProUGUI NextSuperZone;

    
   
    [Header(nameof(Image))]
    [SerializeField] private Image ui_image_zone_Bg;
    [SerializeField] private Image ui_image_zone_frame;
    [SerializeField] private Image ui_image_currentzone;

    [SerializeField] private Image ui_image_Exit_Button_frame;
 //   private SpriteData SpriteData => SpriteManager.Instance.GetSpriteData(UiSpriteType.Other);
    private EventBus eventBus;
    private void Awake()
    {
        eventBus = EventBus.Instance;
       SetImage();
       
    }
    private void Start()
    {
        ZoneGenerate();


    }
    private void SetImage()
    {
        ui_image_zone_Bg.sprite = SpriteManager.Instance.GetSprite(1);
        ui_image_zone_frame.sprite = SpriteManager.Instance.GetSprite(2);
        ui_image_currentzone.sprite = SpriteManager.Instance.GetSprite(3);
    }
    private void OnEnable()
    {
        eventBus.Subscribe<GameEvents.GiveUp>(Reset);
        eventBus.Subscribe<GameEvents.SpinFinish>(MoveZone);
    }
    private void OnDisable()
    {
        eventBus.Unsubscribe<GameEvents.GiveUp>(Reset);
        eventBus.Unsubscribe<GameEvents.SpinFinish>(MoveZone);
    }

    private void ZoneGenerate()
    {
        for (int i = 0; i < 15; i++)
        {
            var obj = Instantiate(ZoneText, MidpointTransform);

            obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(55 * i, MidpointTransform.anchoredPosition.y);
            obj.GetComponent<TextMeshProUGUI>().text = (1 + i).ToString();
            if ((i + 1) % 5 == 0 || i == 0)
            {

                obj.GetComponent<TextMeshProUGUI>().color = Color.green;
              
            }
            ZoneTextList.Enqueue(obj);
        }
        eventBus.Fire(new GameEvents.ZoneType(zoneIndex));
        
    }
    int zoneIndex=1;
    int safezoneIndex=1;
    int superzoneIndex = 1;
    int count;
    public void MoveZone()
    {
        zoneIndex++;
        MidpointTransform.DOLocalMoveX(MidpointTransform.anchoredPosition.x - 55, 0.5f);

        ZoneIndexLoop();
        if (zoneIndex % 5 == 0 || zoneIndex==1)
        {            
            safezoneIndex++;
            SafeZoneView();
            if (zoneIndex%30==0)
            {
                superzoneIndex++;
                NextSuperZone.text = (30 * superzoneIndex).ToString();
            }
           
        }
        else
        {
            ui_image_currentzone.sprite = SpriteManager.Instance.GetSprite(4);
        }
        eventBus.Fire(new GameEvents.ZoneType(zoneIndex));
    }
  

   private void ResetZonetext()
    {
        for (int i = 0; i < MidpointTransform.childCount; i++)
        {
            MidpointTransform.transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition = new Vector2(55 * i, MidpointTransform.anchoredPosition.y);
            MidpointTransform.transform.GetChild(i).GetComponent<TextMeshProUGUI>().text = (1 + i).ToString();
        }
    }

    private void ZoneIndexLoop()
    {
        if (zoneIndex > 7)
        {
            GameObject obj = ZoneTextList.Dequeue();
            if (count==0)
            {
                obj.GetComponent<TextMeshProUGUI>().color = Color.white;
            }
            count++;
            obj.transform.DOLocalMoveX(770 + 55 * count, 0f);
            obj.GetComponent<TextMeshProUGUI>().text = (15 + count).ToString();
            ZoneTextList.Enqueue(obj);
        }
    }
    private void SafeZoneView()
    {
        ui_image_currentzone.sprite =SpriteManager.Instance.GetSprite(3);
        NextSafeZone.text = (5 * safezoneIndex).ToString();
    }
    
   


    public void Reset()
    {
        zoneIndex = 1;
        safezoneIndex = 1;
        superzoneIndex = 1;
        count = 0;
        SafeZoneView();
        ResetZonetext();
        MidpointTransform.DOLocalMoveX(0, 0.1f);
    }
}
