using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFastForward : MonoBehaviour
{

    public PimbaBall pimbaBall;
    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableButton()
    {
        button.interactable = true;
    }
    public void DisableButton()
    {
        button.interactable = false;
    }

    public void FastForwadTime()
    {
        Rigidbody2D body = pimbaBall.GetComponent<Rigidbody2D>();
        if (body.velocity.magnitude >= 0.1)
        {
            Time.timeScale = 3;
            DisableButton();
        }
    }
}
