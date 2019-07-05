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

    public string displayName;
    public string id;
    public int initialCost;
    public ItemProgression progression;
    public float ratio;
    public int limit = 1;
    public string imageURL;
    public int buys = 0;

    public GameObject displayGO;
    public GameObject priceGO;
    public GameObject spriteGO;

    Text displayNameUI;
    Text priceUI;
    Image imageUI;

    RectTransform rect;

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void RefreshUI()
    {
        displayNameUI = displayGO.GetComponent<Text>();
        priceUI = priceGO.GetComponent<Text>();
        imageUI = spriteGO.GetComponent<Image>();

        displayNameUI.text = displayName;
        float val = initialCost;

        if(progression == ItemProgression.PA)
            for(int i = 0; i < buys; i++)
                val += ratio;
        else if(progression == ItemProgression.PG)
            for (int i = 0; i < buys; i++)
                val *= ratio;

        priceUI.text = "$" + (int)val;

        Firebase.Storage.StorageReference storageReference =
   Firebase.Storage.FirebaseStorage.DefaultInstance.GetReferenceFromUrl(imageURL);

        Debug.Log("Requesting Image");

        storageReference.GetBytesAsync(1024 * 1024).
    ContinueWith((System.Threading.Tasks.Task<byte[]> task) =>
    {
        if (task.IsFaulted || task.IsCanceled)
        {
            Debug.Log("Request Failed");
            Debug.Log(task.Exception.ToString());
        }
        else
        {
            Debug.Log("Request Successfull");
            byte[] fileContents = task.Result;
            Debug.Log("File contents");
            Texture2D texture = new Texture2D(100, 100);
            Debug.Log("new Texture");
            texture.LoadImage(fileContents);
            Debug.Log("Load Image");
            //if you need sprite for SpriteRenderer or Image
            Sprite sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width,
            texture.height), new Vector2(0.5f, 0.5f), 100.0f);
            Debug.Log("Sprite Create");
            imageUI.sprite = sprite;
            Debug.Log("Sprite set");
        }
    });

    }

    public void LoadShopItem(ShopItem item)
    {
        displayName = item.displayName;
        id = item.id;
        initialCost = item.initialCost;
        progression = item.progression;
        ratio = item.ratio;
        limit = item.limit;
        imageURL = item.imageURL;

        RefreshUI();
    }
}
