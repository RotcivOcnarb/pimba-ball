using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;
using UnityEngine.UI;

public class FirebaseShopItemPopulator : MonoBehaviour
{

    public GameObject listPrefab;
    public GameObject itemPrefab;
    public GameObject content;
    public GameObject coinCounter;
    public GameObject canvas;
    bool loaded;

    Text coinText;

    float initialListY = 300;
    float listPeriod = 250;

    // Start is called before the first frame update
    void Start()
    {
        coinText = coinCounter.GetComponent<Text>();
        if (GlobalVars.shopCache == null)
            PopulateShopFromRTD();
        else 
            PopulateInterface();
        
    }

    public void PopulateShopFromRTD()
    {
        loaded = false;
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://pimba-ball.firebaseio.com/");
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("flamelink");

        //LÊ O SHOP DO FIREBASE
        reference.GetValueAsync().ContinueWith((System.Threading.Tasks.Task<DataSnapshot> task) =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError(task.Exception);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snap = task.Result;
                Dictionary<string, object> myDict = (Dictionary<string, object>)snap.Value;

                GlobalVars.shopCache = new ShopStructure(myDict, false);
                loaded = true;
            }
        });
    }

    //POPULA O SHOP JÁ CARREGADO NA INTERFACE
    public void PopulateInterface()
    {
        int i = 0;
        foreach (string key in GlobalVars.shopCache.lists.Keys) {
            ShopItemList list = GlobalVars.shopCache.lists[key];
            GameObject go = Instantiate(listPrefab);
            go.transform.SetParent(content.transform, false);

            ShopItemListUI listUI = go.GetComponent<ShopItemListUI>();
            listUI.listName = list.listName;

            RectTransform rect = go.GetComponent<RectTransform>();
            rect.offsetMin = new Vector2(0, -initialListY - 100 - listPeriod * i);
            rect.offsetMax = new Vector2(0, -initialListY + 100 - listPeriod * i);


            int j = 0;
            foreach (ShopItem item in list.items) {
                GameObject go2 = Instantiate(itemPrefab);
                go2.transform.SetParent(listUI.content.transform, false);

                ShopItemUI itemUI = go2.GetComponent<ShopItemUI>();
                itemUI.LoadShopItem(item);
                itemUI.canvas = canvas;

                RectTransform rect2 = go2.GetComponent<RectTransform>();
                rect2.offsetMin = new Vector2(j * 150, 50);
                rect2.offsetMax = new Vector2(150 + j * 150, -100);

                RectTransform listContentRect = listUI.content.GetComponent<RectTransform>();
                listContentRect.offsetMax = new Vector2(listContentRect.offsetMax.x + 100, 100);

                j++;
            }
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = "Coins: $" + GlobalVars.getPlayerProfile().coins;
        if (loaded)
        {
            loaded = false;
            PopulateInterface();
        }

    }
}
