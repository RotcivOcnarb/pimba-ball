using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelCounter : MonoBehaviour
{

    [Range(0, 10)]
    public int value;
    float valueTween;

    List<Image> bars;
    Color defRed;
    Color offRed;

    // Start is called before the first frame update
    void Start()
    {
        bars = new List<Image>();
        defRed = new Color(.9f, .3f, .3f, 1);
        offRed = new Color(.9f, .3f, .3f, 0);
        float step = GetComponent<RectTransform>().rect.size.x / 10f;
        for(int i = 0; i < 10; i ++){
            GameObject go = new GameObject();
            RectTransform rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0, 0);
            rect.anchorMax = new Vector2(0, 1);
            rect.offsetMin = new Vector2(i*step +step*.1f, step*.1f);
            rect.offsetMax = new Vector2((i+1)*step -step*.1f, -step*.1f);
            go.transform.SetParent(transform, false);
            Image img = go.AddComponent<Image>();
            img.color = offRed;
            bars.Add(img);
        }
    }

    // Update is called once per frame
    void Update()
    {
        valueTween += ((value+0.1f) - valueTween) / 10f;

        for(int i = 0; i < 10; i ++){
            bars[i].color = offRed;
        }

        for(int i = 0; i < (int)valueTween; i ++){
            bars[i].color = defRed;
        }
        if((int)valueTween < 10){
            bars[(int)valueTween].color = Color.Lerp(offRed, defRed, valueTween - (int)valueTween);
        }
    }
}
