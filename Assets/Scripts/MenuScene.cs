using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GlobalVars.shopCache == null)
            FirebaseShopItemPopulator.PopulateShopFromRTD();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
