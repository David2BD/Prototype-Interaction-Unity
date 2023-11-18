using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject controllerMenu;
    public GameObject optionMenu;

    public void setControls()
    {
        controllerMenu.SetActive(!controllerMenu.activeSelf);
        optionMenu.SetActive(false);
        Debug.Log("Player enter settings menu");
    }
    
    public void Return()
    {
        mainMenu.SetActive(!mainMenu.activeSelf);
        optionMenu.SetActive(false);
        Debug.Log("Player go back to game menu");
    }
}
