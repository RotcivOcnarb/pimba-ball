using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ItemSlotUI : MonoBehaviour
{

    [Header("Properties")]
    //Populatable variables
    public int purchases;
    public int limit;
    public int cost;
    public Sprite icon;
    public string displayName;
    public string description;
    public bool unlocked = true;

    public ShopItem item;

    [Header("UI")]

    //UI
    public Text limitText;
    public Text costText;
    public Image itemImage;
    public Text nameText;
    public Text descriptionText;
    public Image lockImage;

    [Header("Manager")]
    public ShopManager manager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        limitText.text = purchases + "/" + limit;
        costText.text = "" + cost;
        itemImage.sprite = icon;
        nameText.text = displayName;
        descriptionText.text = description;

        unlocked = item.GetCurrentCost() < GlobalVars.getPlayerProfile().coins;

        if(unlocked){
            lockImage.color = new Color(1, 1, 1, 0);
        }
        else{
            lockImage.color = new Color(1, 1, 1, 0.5f);
        }
    }

    public void Populate(ShopItem item, int purchases, int cost, Sprite icon){
        this.purchases = purchases;
        this.limit = item.limit;
        this.cost = cost;
        if(icon != null)
            this.icon = icon;
        this.displayName = item.displayName;
        this.description = item.description;
        this.item = item;
    }

    public void OpenPurchaseMenu(){
        if(unlocked)
        manager.ShowConfirmWindow(this.icon, item, this);
    }

    public void Refresh(int purchases, int cost){
        this.purchases = purchases;
        this.cost = cost;

        limitText.text = purchases + "/" + limit;
        costText.text = "" + cost;
    }
}
