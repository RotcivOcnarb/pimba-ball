using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasColliderFiller : MonoBehaviour
{

    BoxCollider2D boxCollider;
    RectTransform rect;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        boxCollider.size = rect.rect.size;
    }
}
