using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ShopStructure
{

    public List<ShopItemList> lists;

    public ShopStructure() {
        lists = new List<ShopItemList>();
    }

    public int ToInt(object obj)
    {
        return int.Parse(obj.ToString());
    }

    public float ToFloat(object obj)
    {
        return float.Parse(obj.ToString());
    }

    public ShopStructure(Dictionary<string, object> data)
    {
        lists = new List<ShopItemList>();

        List<object> ls = (List<object>) data["lists"];
        for(int i = 0; i < ls.Count; i ++)
        {
            Dictionary<string, object> list = (Dictionary<string, object>)ls[i];
            ShopItemList sil = new ShopItemList((string)list["listName"]);
            List<object> items = (List<object>)list["items"];
            for(int j = 0; j < items.Count; j++)
            {
                Dictionary<string, object> item = (Dictionary<string, object>)items[j];

                string displayName = (string)item["displayName"];
                string id = (string)item["id"];
                int initialCost = ToInt(item["initialCost"]);
                ShopItemUI.ItemProgression progression = (ShopItemUI.ItemProgression)(ToInt(item["progression"]));
                float ratio = ToFloat(item["ratio"]);
                int limit = ToInt(item["limit"]);
                string imageURL = (string)item["imageURL"];

                ShopItem si = new ShopItem(
                        displayName,
                        id,
                        initialCost,
                        progression,
                        ratio,
                        limit,
                        imageURL
                        );
                sil.addShopItem(si);
            }

            lists.Add(sil);
        }
    }

    public void addShopItemList(ShopItemList list)
    {
        lists.Add(list);
    }

    public override string ToString()
    {
        string res = "\nItem list count: " + lists.Count + "\n";
        foreach(ShopItemList l in lists)
            res += l.ToString();
        return res;
    }
}
