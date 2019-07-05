using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;

public class FirebaseShopItemPopulator : MonoBehaviour
{

    public GameObject listPrefab;
    public GameObject itemPrefab;
    public GameObject content;

    bool loaded;

    ShopStructure shop;

    // Start is called before the first frame update
    void Start()
    {
        loaded = false;
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://pimba-ball.firebaseio.com/");
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

        //LÊ O SHOP
        Debug.Log("Begin Fetch");
        reference.GetValueAsync().ContinueWith((System.Threading.Tasks.Task<DataSnapshot> task) =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError(task.Exception);
            }
            else if (task.IsCompleted)
            {
                Debug.Log("Fetched");
                DataSnapshot snap = task.Result;
                Dictionary<string, object> myDict = (Dictionary<string, object>)snap.Value;
                shop = new ShopStructure(myDict);

                loaded = true;
            }
        });

    
        
    }

    // Update is called once per frame
    void Update()
    {

        if (loaded)
        {
            //Carrega o shop na UI
            loaded = false;

            int i = 0;
            foreach (ShopItemList list in shop.lists)
            {
                GameObject go = Instantiate(listPrefab);
                go.transform.SetParent(content.transform, false);

                ShopItemListUI listUI = go.GetComponent<ShopItemListUI>();
                listUI.listName = list.listName;

                RectTransform rect = go.GetComponent<RectTransform>();
                rect.offsetMin = new Vector2(0, -220 - 155*i);
                rect.offsetMax = new Vector2(0, -70 - 155*i);

                int j = 0;
                foreach(ShopItem item in list.items)
                {
                    GameObject go2 = Instantiate(itemPrefab);
                    go2.transform.SetParent(listUI.content.transform, false);

                    ShopItemUI itemUI = go2.GetComponent<ShopItemUI>();
                    itemUI.LoadShopItem(item);

                    RectTransform rect2 = go2.GetComponent<RectTransform>();

                    rect2.offsetMin = new Vector2(j*100, 100);
                    rect2.offsetMax = new Vector2(100 + j*100, -100);

                    j++;
                }
                i++;
            }
        }

    }
}
