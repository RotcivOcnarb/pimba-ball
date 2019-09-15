using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[ExecuteInEditMode]
public class ItemCategoryUI : MonoBehaviour
{
    [Header("Properties")]
    public string title;
    [Header("UI")]
    public Text titleUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        titleUI.text = title;
    }

    public ItemCategoryUI Populate(string title){
        this.title = title;
        return this;
    }
}
