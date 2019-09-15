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

    public ShopItem item;

    [Header("UI")]

    //UI
    public Text limitText;
    public Text costText;
    public Image itemImage;
    public Text nameText;
    public Text descriptionText;

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
        manager.ShowConfirmWindow(this.icon, item);
    }
}
