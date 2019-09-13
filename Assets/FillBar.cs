using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FillBar : MonoBehaviour
{
    [Range(0, 100)]
    public float percentage;

    public RectTransform fill;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        fill.offsetMin = new Vector2(0, 0);
        fill.offsetMax = new Vector2(0, -(100-percentage)/100f * GetComponent<RectTransform>().rect.height);
    }
}
