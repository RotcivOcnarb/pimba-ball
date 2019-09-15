using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public GameObject confirmWindow;
    public Image confirmImage;
    ShopItem item;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowConfirmWindow(Sprite itemSprite, ShopItem item){
        this.item = item;
        confirmWindow.SetActive(true);
        confirmImage.GetComponent<Image>().sprite = itemSprite;
        confirmWindow.transform.position = new Vector3(0, -7, 0);
    }

    public void CloseConfirmWindow(){
        confirmWindow.SetActive(false);
    }

    public void ConfirmPurchase(){

        CloseConfirmWindow();
    }
}
