using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Game.Core
{
    public class ItemPointer : MonoBehaviour
    {
        [SerializeField] private Image uiItemIcon;
        [SerializeField] private RectTransform itemIconRectTransform;
        [SerializeField] private TextMeshProUGUI itemText;

        public RectTransform pointer_rectTransform => GetComponent<RectTransform>();
        public Image UiItemIcon => uiItemIcon;
        public RectTransform ItemIconRectTransform => itemIconRectTransform;
        public TextMeshProUGUI ItemText => itemText;
    }
}

