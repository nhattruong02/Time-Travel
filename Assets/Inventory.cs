using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour,ISavable
{
    [SerializeField] List<ItemSlot> slots;
    public List<ItemSlot> Slots => slots;
    public event Action OnUpdated;
    List<List<ItemSlot>> allSlots;
    private void Awake()
    {
        allSlots = new List<List<ItemSlot>>() { slots };
    }
    public static Inventory GetInventory() {
        try
        {
            return FindObjectOfType<PlayerController>().GetComponent<Inventory>();
        } catch (NullReferenceException)
        {
            return null;
        }
    }
    public void AddItem(Item newitem, int count = 1)
    {
        if (slots.Count == 0) {
            slots.Add(new ItemSlot
            {
                Item = newitem,
                Count = 1
            });
        }
        else
        {
            for (int i = 0; i < slots.Count; i++)
            {
                Debug.Log(newitem);
                var itemSlot = slots.FirstOrDefault(slots => slots.Item == newitem);
             /*   Debug.Log(itemSlot.ToString());*/
                if (itemSlot != null)
                {
                    slots[i].Count += count;
                    Debug.Log(newitem.Name);
                    Debug.Log(slots[i].Item.Name);
                }
                else
                {
                    slots.Add(new ItemSlot
                    {
                        Item = newitem,
                        Count = 1
                    });
                    Debug.Log(1);
                }
            }
        }
    }
    public void RemoveItem(Item item)
    {
        var itemSlot = slots.First(slots => slots.Item == item);
        itemSlot.Count--;
        if(itemSlot.Count == 0)
        {
            slots.Remove(itemSlot);
        }
    }
    public void CheckItem()
    {
        if(slots.Count == 5)
        {
            SceneManager.LoadScene(8);
        }
    }
    public bool HasItem(Item item)
    {
        return slots.Exists(slots => slots.Item.name.Equals(item.name));
    }
    public object CaptureState()
    {
        var saveData = new InventorySaveData()
        {
            items = slots.Select(i => i.GetDataSave()).ToList(),
        };
        return saveData;
    }

    public void RestoreState(object state)
    {
        var saveData = state as InventorySaveData;
        try
        {
            slots = saveData.items.Select(i => new ItemSlot(i)).ToList();
        }
        catch (NullReferenceException)
        {

        }
        allSlots = new List<List<ItemSlot>>() { slots};
        OnUpdated?.Invoke();
    }
}
[Serializable]
public class ItemSlot
{
    [SerializeField] Item item;
    [SerializeField] int count;
    public ItemSlot()
    {

    }
    public ItemSlot(ItemSaveData saveData) {
        item = ItemDB.GetItemByName(saveData.name);
        count = saveData.count;

    }
    public ItemSaveData GetDataSave()
    {
        var saveData = new ItemSaveData()
        {   
            name = item.Name,
            count = count,

        };
        return saveData;
    }
    public Item Item { get => item; set => item = value; }
    public int Count {
        get => count;
        set => count = value;
     }

}
[Serializable]
public class ItemSaveData
{
    public string name;
    public int count;
    public Sprite icon;
}
[Serializable]
public class InventorySaveData
{
    public List<ItemSaveData> items;
}
