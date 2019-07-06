using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopItemListUI : MonoBehaviour
{
    RectTransform rect;
    RectTransform contentRect;
    public string listName;
    public GameObject content;
    public GameObject titleObject;
    Text titleText;
    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
        contentRect = content.GetComponent<RectTransform>();
        titleText = titleObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        titleText.text = listName;
    }
}
