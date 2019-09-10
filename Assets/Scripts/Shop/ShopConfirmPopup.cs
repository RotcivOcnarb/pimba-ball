using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopConfirmPopup : MonoBehaviour
{

    public Sprite image;
    public ShopItem item;
    public Button yesButton;
    public Image imageComponent;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Updating popup " + (item.ratio));
        yesButton.interactable =
        GlobalVars.getPlayerProfile().coins >= item.GetCurrentCost() &&
        GlobalVars.getPlayerProfile().GetValue(item.id) < item.limit;
        imageComponent.sprite = image;
    }

    public void Deny(){
        Debug.Log("Destroying");
        Destroy(gameObject);
    }

    public void Accept(){
        GlobalVars.getPlayerProfile().coins -= item.GetCurrentCost();
        if(!GlobalVars.getPlayerProfile().upgrades.ContainsKey(item.id)){
            GlobalVars.getPlayerProfile().upgrades.Add(item.id, 0);
        }
        GlobalVars.getPlayerProfile().upgrades[item.id]++;
        Destroy(gameObject);
        //Also saves game
        GlobalVars.SaveGame();
    }
}
