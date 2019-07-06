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

    public int GetCurrentCost(){

        float val = initialCost;

        if(GlobalVars.getPlayerProfile().upgrades.ContainsKey(id)){

            int cont = GlobalVars.getPlayerProfile().upgrades[id];

            for(int i = 0; i < cont; i ++){
                if(progression == ShopItemUI.ItemProgression.PA){
                    val += ratio;
                }
                else if(progression == ShopItemUI.ItemProgression.PG){
                    val *= ratio;
                }
                else{
                    return initialCost;
                }
            }

            return (int)val;
        }
        return initialCost;
    }
}
