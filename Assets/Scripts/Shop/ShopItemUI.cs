using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{

    public enum ItemProgression
    {
        PA,
        PG,
        NONE
    }

    public ShopItem item;

    public GameObject displayGO;
    public GameObject priceGO;
    public GameObject spriteGO;
    public GameObject confirmPopup;
    public GameObject canvas;

    Text displayNameUI;
    Text priceUI;
    Image imageUI;

    RectTransform rect;

    byte[] imageData;
    bool loaded;

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(loaded){
            loaded = false;
            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(imageData);
            Sprite sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width,
            texture.height), new Vector2(0.5f, 0.5f), 100.0f);
            imageUI.sprite = sprite;
        }

        priceUI = priceGO.GetComponent<Text>();
        priceUI.text = "$" + item.GetCurrentCost();

    }

    public void ButtonClick(){

        //TODO:

        //Cria o popup em cima desse canvas
                    //Aparecer um popup "Deseja comprar este item? SIM, NAO"
        GameObject popup = Instantiate(confirmPopup);

        popup.transform.SetParent(canvas.transform, false);
        RectTransform rect = popup.GetComponent<RectTransform>();
        popup.GetComponent<ShopConfirmPopup>().item = item;
        popup.GetComponent<ShopConfirmPopup>().image = imageUI.sprite;

        rect.anchorMin = new Vector2(0, 0f);
        rect.anchorMax = new Vector2(1f, 1f);
        rect.offsetMax = new Vector2(0, 0);
        rect.offsetMin = new Vector2(0, 0);
        
        Canvas canvasPop = popup.GetComponent<Canvas>();
        canvasPop.overrideSorting = true;
        canvasPop.sortingOrder = 10;

    }

    void RefreshUI()
    {
        displayNameUI = displayGO.GetComponent<Text>();
        
        imageUI = spriteGO.GetComponent<Image>();

        displayNameUI.text = item.displayName;


        
        Firebase.Storage.StorageReference storageReference =
   Firebase.Storage.FirebaseStorage.DefaultInstance.GetReferenceFromUrl(item.imageURL);
        loaded = false;

        storageReference.GetBytesAsync(1024 * 1024).
    ContinueWith((System.Threading.Tasks.Task<byte[]> task) =>
    {
        if (task.IsFaulted || task.IsCanceled)
        {
            Debug.Log(task.Exception.ToString());
        }
        else
        {
            imageData = task.Result;
            loaded = true;
        }
    });

    }

    public void LoadShopItem(ShopItem item)
    {
        this.item = item;
        RefreshUI();
    }
}
