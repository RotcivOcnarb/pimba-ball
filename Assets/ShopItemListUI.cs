using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemListUI : MonoBehaviour
{
    RectTransform rect;
    public string listName;
    public GameObject content;
    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
