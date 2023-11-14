using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject optionsMenu;
    
    public void Play()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene("Game");
    }

    public void Options()
    {
        optionsMenu.SetActive(!optionsMenu.activeSelf);
        Debug.Log("Player enter game settings");
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Player quit game");
    }
}
