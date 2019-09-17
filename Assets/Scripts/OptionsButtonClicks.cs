using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class OptionsButtonClicks : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeleteSaveConfirm()
    {
        GlobalVars.DeleteSaveGame();
    }

    public void GoBack()
    {
        Debug.Log("Voltando");
        SceneManager.LoadScene(0);
    }
}
