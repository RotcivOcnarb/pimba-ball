using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenDialog(){
        gameObject.SetActive(true);
        GetComponentInChildren<DropToCenterY>().transform.position = new Vector3(0, 7, 0);
    }

    public void CloseDialog(){
        gameObject.SetActive(false);
    }
}
