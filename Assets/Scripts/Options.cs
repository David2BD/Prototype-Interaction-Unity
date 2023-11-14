using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject controllerMenu;

    public void setControls()
    {
        controllerMenu.SetActive(!controllerMenu.activeSelf);
        Debug.Log("Player enter settings menu");
    }
    
    public void Return()
    {
        mainMenu.SetActive(!mainMenu.activeSelf);
        Debug.Log("Player go back to game menu");
    }
}
