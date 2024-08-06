using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    [SerializeField] Image iconItem;
    [SerializeField] Text countText;
    public void SetData(ItemSlot itemslot)
    {
        iconItem.sprite = itemslot.Item.Icon;
        countText.text = $"X {itemslot.Count}";
    }
}
