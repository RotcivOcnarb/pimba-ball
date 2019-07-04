using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    Image bar;
    public PimbaBall pimbaBall;
    float initialWidth;
    float lifeTween;

    // Start is called before the first frame update
    void Start()
    {
        bar = GetComponent<Image>();
        initialWidth = bar.rectTransform.rect.width;
        lifeTween = pimbaBall.life;
    }

    // Update is called once per frame
    void Update()
    {
        lifeTween += (pimbaBall.life - lifeTween) / 5f;
        bar.rectTransform.offsetMax = new Vector2(-initialWidth * (1-(lifeTween / pimbaBall.maxLife)), 0);
    }
}
