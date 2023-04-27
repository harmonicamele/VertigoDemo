using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPointer : MonoBehaviour
{
    public RectTransform pointer_rectTransform=>GetComponent<RectTransform>();
    public Image ui_item_Icon=>transform.GetChild(0).GetComponent<Image>();
    public RectTransform item_Icon_rectTransform=> transform.GetChild(0).GetComponent<RectTransform>();
    public TextMeshProUGUI item_Text=> transform.GetChild(1).GetComponent<TextMeshProUGUI>();

}
