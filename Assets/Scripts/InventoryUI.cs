using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] GameObject itemList;
    [SerializeField] ItemSlotUI itemSlotUI;
    List<ItemSlotUI> slotUIList;
    Inventory inventory;
    private void Awake()
    {
        inventory = Inventory.GetInventory();
    }
    private void Start()
    {
        UpdateItemList();
    }
    private void Update()
    {
        UpdateItemList();
    }
    private void UpdateItemList()
    {
        foreach (Transform child in itemList.transform)
            Destroy(child.gameObject);
        slotUIList = new List<ItemSlotUI>();
        foreach(var itemSlot in inventory.Slots)
        {
           
            var slotUIObj = Instantiate(itemSlotUI, itemList.transform);
            slotUIObj.GetComponent<ItemSlotUI>().SetData(itemSlot);  
            slotUIList.Add(slotUIObj);
        }
    }
}
