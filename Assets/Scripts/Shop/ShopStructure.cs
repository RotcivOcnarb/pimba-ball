using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ShopStructure
{

    public SortedList<string, ShopItemList> lists;

    public int ToInt(object obj)
    {
        return int.Parse(obj.ToString());
    }

    public float ToFloat(object obj)
    {
        return float.Parse(obj.ToString());
    }

    public long ToLong(object obj)
    {
        return long.Parse(obj.ToString());
    }

    public ShopStructure(Dictionary<string, object> data, bool legacy)
    {
        lists = new SortedList<string, ShopItemList>();

        if (legacy)
            LegacyLoad(data);
        else
            LoadFromRTD(data);
    }

    public Dictionary<string, object> GetChildrenPath(object data, string path)
    {
        Dictionary<string, object> dict = (Dictionary<string, object>)data;

        Dictionary<string, object> res = dict;
        foreach (string s in path.Split('/'))
        {
            res = (Dictionary<string, object>)res[s];
        }

        return res;
    }

    public void LoadFromRTD(Dictionary<string, object> data)
    {
        //Carrega os arquivos de mídia
        Dictionary<string, object> media = GetChildrenPath(data, "media/files");

        //Carrega as listas de items da loja
        Dictionary<string, object> itemLists = GetChildrenPath(data, "environments/production/content/itemList/en-US");
        foreach (string key in itemLists.Keys)
        {
            Dictionary<string, object> listDict = (Dictionary<string, object>)itemLists[key];
            ShopItemList list = new ShopItemList((string)listDict["listName"]);
            string listID = ToLong(listDict["id"]) + "";
            lists.Add(listID, list);
        }

        //Carrega os items da loja e popula nas respectivas listas de items
        Dictionary<string, object> items = GetChildrenPath(data, "environments/production/content/items/en-US");
        foreach (string key in items.Keys)
        {
            Dictionary<string, object> itemDict = (Dictionary<string, object>)items[key];
            //Pucha a URL da imagem baseado no ID salvo (o flamelink é ruim nisso)

            string imageID = ToLong(((List<object>)itemDict["imageURL"])[0]) + "";
            string imageURL = "gs://pimba-ball.appspot.com/flamelink/media/" + (string)((Dictionary<string, object>)media[imageID])["file"];
            string listID = ToLong(itemDict["item-list"]) + "";

            ShopItem item = new ShopItem(
                (string)itemDict["displayName"],
                (string)itemDict["itemId"],
                ToInt(itemDict["precoInicial"]),
                (ShopItemUI.ItemProgression)int.Parse((string)itemDict["progression"]),
                ToFloat(itemDict["ratio"]),
                ToInt(itemDict["limite"]),
                imageURL,
                (string)itemDict["description"]
                );

            lists[listID].addShopItem(item);
        }
    }

    public void LegacyLoad(Dictionary<string, object> data)
    {
        

        List<object> ls = (List<object>)data["lists"];
        for (int i = 0; i < ls.Count; i++)
        {
            Dictionary<string, object> list = (Dictionary<string, object>)ls[i];
            ShopItemList sil = new ShopItemList((string)list["listName"]);
            List<object> items = (List<object>)list["items"];
            for (int j = 0; j < items.Count; j++)
            {
                Dictionary<string, object> item = (Dictionary<string, object>)items[j];

                string displayName = (string)item["displayName"];
                string id = (string)item["id"];
                int initialCost = ToInt(item["initialCost"]);
                ShopItemUI.ItemProgression progression = (ShopItemUI.ItemProgression)(ToInt(item["progression"]));
                float ratio = ToFloat(item["ratio"]);
                int limit = ToInt(item["limit"]);
                string imageURL = (string)item["imageURL"];
                string description = (string)item["description"];

                ShopItem si = new ShopItem(
                        displayName,
                        id,
                        initialCost,
                        progression,
                        ratio,
                        limit,
                        imageURL,
                        description
                        );
                sil.addShopItem(si);
            }

            lists.Add(sil.listName, sil);
        }
    }
}
