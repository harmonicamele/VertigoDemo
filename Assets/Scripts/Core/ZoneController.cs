using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game.EventSystem;
using Game.Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Game.Core
{
    public class ZoneController : MonoBehaviour
    {
        public GameObject ZoneText;
        public RectTransform MidpointTransform;

        [Header("Text")]
        [SerializeField] private TextMeshProUGUI NextSafeZone;
        [SerializeField] private TextMeshProUGUI NextSuperZone;

        [Header("Image")]
        [SerializeField] private Image ui_image_zone_Bg;
        [SerializeField] private Image ui_image_zone_frame;
        [SerializeField] private Image ui_image_currentzone;
        [SerializeField] private Image ui_image_Exit_Button_frame;

        private Queue<GameObject> zoneTextList = new Queue<GameObject>();
        private EventBus eventBus;
        //Const Value
        private const int visibleZoneAmount = 15;
        private const float zoneIndexDistance = 55f;
        private const int zoneSuperIndex = 5;
        private const int zoneGoldenIndex = 30;

        private void Awake()
        {
            eventBus = EventBus.Instance;
            SetImage();
        }

        private void Start()
        {
            ZoneGenerate();
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
        private void SetImage()
        {
            ui_image_zone_Bg.sprite = SpriteManager.Instance.GetSprite(1);
            ui_image_zone_frame.sprite = SpriteManager.Instance.GetSprite(2);
            ui_image_currentzone.sprite = SpriteManager.Instance.GetSprite(3);
        }

        private void ZoneGenerate()
        {
            for (int i = 0; i < visibleZoneAmount; i++)
            {
                var obj = Instantiate(ZoneText, MidpointTransform);

                obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(zoneIndexDistance * i, MidpointTransform.anchoredPosition.y);
                obj.GetComponent<TextMeshProUGUI>().text = (1 + i).ToString();
                if ((i + 1) % zoneSuperIndex == 0 || i == 0)
                {

                    obj.GetComponent<TextMeshProUGUI>().color = Color.green;

                }
                zoneTextList.Enqueue(obj);
            }
            eventBus.Fire(new GameEvents.ZoneType(zoneIndex));

        }
        int zoneIndex = 1;
        int safezoneIndex = 1;
        int superzoneIndex = 1;
        int count;
        public void MoveZone()
        {
            zoneIndex++;
            MidpointTransform.DOLocalMoveX(MidpointTransform.anchoredPosition.x - zoneIndexDistance, 0.5f);

            ZoneIndexLoop();
            if (zoneIndex % zoneSuperIndex == 0 || zoneIndex == 1)
            {
                safezoneIndex++;
                SafeZoneView();
                if (zoneIndex % zoneGoldenIndex == 0)
                {
                    superzoneIndex++;
                    NextSuperZone.text = (zoneGoldenIndex * superzoneIndex).ToString();
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
                GameObject obj = zoneTextList.Dequeue();
                if (count == 0)
                {
                    obj.GetComponent<TextMeshProUGUI>().color = Color.white;
                }
                count++;
                obj.transform.DOLocalMoveX(770 + 55 * count, 0f);
                obj.GetComponent<TextMeshProUGUI>().text = (15 + count).ToString();
                zoneTextList.Enqueue(obj);
            }
        }
        private void SafeZoneView()
        {
            ui_image_currentzone.sprite = SpriteManager.Instance.GetSprite(3);
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
}

