using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDB : ScriptableObject
{
    static Dictionary<string, Item> items;
    public static void Init()
    {
        items = new Dictionary<string, Item>();
        var itemList = Resources.LoadAll<Item>("");
        foreach (var item in itemList)
        {
            if(items.ContainsKey(item.Name) )
            {
                continue;
            }
            items[item.Name] = item;
        }
    }
    public static Item GetItemByName(string name)
    {
        if(!items.ContainsKey(name))
        {
            return null;
        }   
        return items[name];
    }
}
