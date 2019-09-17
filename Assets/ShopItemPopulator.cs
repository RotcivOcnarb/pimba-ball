using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemPopulator : MonoBehaviour
{
    public GameObject grid;
    public GameObject itemCategoryPrefab;
    public GameObject itemPrefab;
    public GameObject loadingSprite;

    Queue actions;

    bool shopLoaded = false;
    // Start is called before the first frame update
    void Start()
    {  
        actions = new Queue();
        //Procura o shop
        GlobalVars.LoadShop(this);
    }

    public void CreateUI(){
        //Popula o grid
        foreach (string key in GlobalVars.shopCache.lists.Keys) {
            ShopItemList list = GlobalVars.shopCache.lists[key];

            GameObject categoryLabel = Instantiate(itemCategoryPrefab);
            categoryLabel.transform.SetParent(grid.transform, false);
            categoryLabel.GetComponent<ItemCategoryUI>().Populate(list.listName);

            foreach(ShopItem item in list.items){
                GameObject itemSlot = Instantiate(itemPrefab);
                itemSlot.transform.SetParent(grid.transform, false);

                Sprite itemSprite = null;

                if (!GlobalVars.GetShopImagesCache().ContainsKey(item.imageURL))
                    GlobalVars.LoadImageFromStorage(item.imageURL, (byte[] data) => {

                        //Já que eu não posso dar texture.LoadImage fora da Thread principal
                        //do Unity, eu coloco ele numa lista de próximas coisas a serem
                        //Executadas na main Thread (no metodo Update desse objeto)
                        QueueToExecute(() => {
                            Texture2D texture = new Texture2D(1, 1);
                            texture.LoadImage(data);
                            Sprite sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width,
                            texture.height), new Vector2(0.5f, 0.5f), 100.0f);
                            itemSlot.GetComponent<ItemSlotUI>().icon = sprite;
                        });
                        
                    });
                else
                    itemSprite = GlobalVars.GetShopImagesCache()[item.imageURL];

                itemSlot.GetComponent<ItemSlotUI>().Populate(
                    item,
                    GlobalVars.getPlayerProfile().GetValue(item.id),
                    item.GetCurrentCost(),
                    itemSprite
                );

                itemSlot.GetComponent<ItemSlotUI>().manager = GetComponent<ShopManager>();
            }
        }

        //Pega o 2o white space e joga pro final
        grid.transform.GetChild(1).transform.SetAsLastSibling();
    }

    // Update is called once per frame
    void Update()
    {
        if(!shopLoaded && GlobalVars.shopCache != null){
            shopLoaded = true;
            Destroy(loadingSprite);
            CreateUI();
        }

        while(actions.Count > 0){
            (actions.Dequeue() as System.Action)();
        }
    }

    public void QueueToExecute(System.Action func){
        actions.Enqueue(func);
    }
}
