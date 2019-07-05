using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ShopItem
{
    public string displayName;
    public string id;
    public int initialCost;
    public ShopItemUI.ItemProgression progression;
    public float ratio;
    public int limit;
    public string imageURL;

    public ShopItem()
    {

    }

    public ShopItem(string displayName, string id, int initialCost, ShopItemUI.ItemProgression progression, float ratio, int limit, string imageURL)
    {
        this.displayName = displayName;
        this.id = id;
        this.initialCost = initialCost;
        this.progression = progression;
        this.ratio = ratio;
        this.limit = limit;
        this.imageURL = imageURL;
    }
}
