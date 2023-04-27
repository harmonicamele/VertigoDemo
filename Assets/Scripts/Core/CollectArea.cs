using System.Collections.Generic;
using Game.Data;
using Game.EventSystem;
using Game.Manager;
using UnityEngine;
using UnityEngine.UI;
namespace Game.Core
{
    public class CollectArea : MonoBehaviour
    {
        public Prize Prize;
        [Header(nameof(Image))]
        [SerializeField] private Image ui_image_collect_frame;
        [SerializeField] private Image ui_image_Exit_Button;
        [SerializeField] private Image ui_image_Exit_Button_frame;
        

        private List<int> itemDataId = new List<int>();
        private List<Prize> prizeList = new List<Prize>();
        private Prize obj;
        private EventBus eventBus;
        private const float prizeYDistance = -40f;

        private void Awake()
        {
            SetImage();
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

        private void SetImage()
        {
            eventBus = EventBus.Instance;
            ui_image_collect_frame.sprite = SpriteManager.Instance.GetSprite(2);
            ui_image_Exit_Button_frame.sprite = SpriteManager.Instance.GetSprite(2);
            ui_image_Exit_Button.sprite = SpriteManager.Instance.GetSprite(4);
        }

        private void Selected(GameEvents.Selected selected)
        {
            var data = selected.ItemData;
            if (!itemDataId.Contains(data.Item_id_value))
            {

                AddNewPrize(data);
            }
            else
            {

                foreach (var item in prizeList)
                {
                    if (item.ItemData.Item_id_value == data.Item_id_value)
                    {
                        item.Setvalue(data.Item_amount_value);
                        Invoke(nameof(Finish), 1);
                    }
                }

            }
        }

        private void AddNewPrize(ItemData data)
        {
            obj = Instantiate(Prize, this.transform);
            obj.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, itemDataId.Count * prizeYDistance);
            obj.PrizeImage.sprite = SpriteManager.Instance.GetSprite(data.Item_sprite_index);
            obj.Itemvalue = data.Item_amount_value;
            obj.Text.text = data.Item_amount_value.ToString();
            obj.ItemData = data;
            itemDataId.Add(data.Item_id_value);
            prizeList.Add(obj);
            Invoke(nameof(Finish), 1);
        }
        private void Finish()
        {
            eventBus.Fire(new GameEvents.SpinFinish());
            eventBus.Fire(new GameEvents.SpinReady());
        }
        public void Reset()
        {
            itemDataId.Clear();

            foreach (var item in prizeList)
            {
                Destroy(item.gameObject);
            }
            prizeList.Clear();
        }
    }
}

