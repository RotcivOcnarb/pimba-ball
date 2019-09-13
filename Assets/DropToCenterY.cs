﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropToCenterY : MonoBehaviour
{
    public Vector3 targetPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (targetPosition - transform.position) / 5f;
    }
}
