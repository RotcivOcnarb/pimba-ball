using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ShopItemList
{

    public string listName;
    public List<ShopItem> items;

    public ShopItemList(string name)
    {
        items = new List<ShopItem>();
        this.listName = name;
    }

    public void addShopItem(ShopItem item)
    {
        items.Add(item);
    }

    public override string ToString()
    {
        string res = "\tList count: " + items.Count + "\n";
        foreach(ShopItem si in items)
            res += "\t\t" + si.id + "\n";
        return res;
    }

}
