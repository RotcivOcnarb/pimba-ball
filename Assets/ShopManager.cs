using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public GameObject confirmWindow;
    public Image confirmImage;

    public Text coinsText;
    ShopItem item;
    ItemSlotUI slotUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        coinsText.text = "" + GlobalVars.getPlayerProfile().coins;
    }

    public void ShowConfirmWindow(Sprite itemSprite, ShopItem item, ItemSlotUI slotUI){
        this.item = item;
        this.slotUI = slotUI;
        confirmWindow.GetComponent<Dialog>().OpenDialog();
        confirmImage.GetComponent<Image>().sprite = itemSprite;
    }

    public void CloseConfirmWindow(){
        Debug.Log("Window closed");
        confirmWindow.GetComponent<Dialog>().CloseDialog();
    }

    public void ConfirmPurchase(){

        GlobalVars.getPlayerProfile().coins -= item.GetCurrentCost();
        if(!GlobalVars.getPlayerProfile().upgrades.ContainsKey(item.id)){
            GlobalVars.getPlayerProfile().upgrades.Add(item.id, 0);
        }
        GlobalVars.getPlayerProfile().upgrades[item.id]++;
        //Also saves game
        GlobalVars.SaveGame();
        Debug.Log("Item purchased successfully");

        slotUI.Refresh(GlobalVars.getPlayerProfile().upgrades[item.id], item.GetCurrentCost());
        CloseConfirmWindow();
    }
}
