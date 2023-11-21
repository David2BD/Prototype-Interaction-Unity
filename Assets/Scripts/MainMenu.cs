using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject optionsMenu;
    public GameObject mainMenu;
    public TextMeshProUGUI Player2Text;
    public TextMeshProUGUI Player2Button;

    void Start()
    {
        GameSettings.isPlayer2CPU = false;
        GameSettings.CPUDifficulty = 0;
    }




    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    public void Options()
    {
        optionsMenu.SetActive(!optionsMenu.activeSelf);
        mainMenu.SetActive(false);
        Debug.Log("Player enter game settings");
    }

    public void ChangePlayer2()
    {
        if (Player2Button.text == "CPU (easy)")
        {
            Player2Text.SetText("Player 2 : CPU (easy)");
            Player2Button.SetText("CPU (Hard)");
            GameSettings.isPlayer2CPU = true;
            GameSettings.CPUDifficulty = 1;
        }
        else if (Player2Button.text == "CPU (Hard)")
        {
            Player2Text.SetText("Player 2 : CPU (Hard)");
            Player2Button.SetText("Human");
            GameSettings.isPlayer2CPU = false;
            GameSettings.CPUDifficulty = 2;
        }
        else if (Player2Button.text == "Human")
        {
            Player2Text.SetText("Player 2 : Human");
            Player2Button.SetText("CPU (easy)");
            GameSettings.isPlayer2CPU = false;
            GameSettings.CPUDifficulty = 0;
        }
    }
    
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Player quit game");
    }
}
