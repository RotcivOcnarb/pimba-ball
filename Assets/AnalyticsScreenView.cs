using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyticsScreenView : MonoBehaviour
{

    public GoogleAnalyticsV4 analyticsV4;
    public string screenName;

    // Start is called before the first frame update
    void Start()
    {
        analyticsV4.LogScreen(screenName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
