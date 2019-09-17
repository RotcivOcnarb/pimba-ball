using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ProgressBar : MonoBehaviour
{
    public int maxValue;
    public int value;
    public GameObject slotPrefab;
    public HorizontalLayoutGroup grid;

    public Sprite spriteOn;
    public Sprite spriteOff;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        maxValue = Mathf.Max(1, maxValue);
        value = Mathf.Clamp(value, 0, maxValue);

        //Remove os que tão sobrando
        while(grid.transform.childCount > maxValue){
            DestroyImmediate(grid.transform.GetChild(grid.transform.childCount-1).gameObject);
        }

        //Criando os que tão faltando
        while(grid.transform.childCount < maxValue){
            GameObject slot = Instantiate(slotPrefab);
            slot.transform.SetParent(grid.transform, false);
        }

        grid.spacing = 100/(float)(maxValue + 4);

        //Atualiza os que já tão lá
        for(int i = 0; i < grid.transform.childCount; i ++){
            GameObject slot = grid.transform.GetChild(i).gameObject;
            if(i >= value)
                slot.GetComponent<Image>().sprite = spriteOff;
            else
                slot.GetComponent<Image>().sprite = spriteOn;
        }

        
        
    }
}
