using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class OptionsButtonClicks : MonoBehaviour
{
    public GameObject popupConfirm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeleteSaveDataButton()
    {
        popupConfirm.SetActive(true);
    }

    public void DeleteSaveConfirm()
    {
        GlobalVars.DeleteSaveGame();
        popupConfirm.SetActive(false);
    }

    public void DeleteSaveDeny()
    {
        popupConfirm.SetActive(false);
    }

    public void GoBack()
    {
        Debug.Log("Voltando");
        SceneManager.LoadScene(0);
    }
}
