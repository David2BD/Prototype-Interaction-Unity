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

    private int count = 0;
    
    public TextMeshProUGUI Player2Button = new TextMeshProUGUI();
    public TextMeshProUGUI current = new TextMeshProUGUI();

    public TextMeshProUGUI player1_name = new TextMeshProUGUI();
    public TextMeshProUGUI player2_name = new TextMeshProUGUI();

    void Start()
    {
        GameSettings.isPlayer2CPU = false;
        GameSettings.CPUDifficulty = 0;

        if (GameManager.Instance != null)
        {
            current.SetText("Human");
            Player2Button.SetText("Change : CPU (easy)");
        }
    }

    private void Update()
    {
        player1_name.SetText(GameManager.Instance.getName(1));
        player2_name.SetText(GameManager.Instance.getName(2));
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
        count = (count + 1) % 3;
        
        // le texte du bouton est le niveau/adversaire
        if (count == 1)
        {
            current.SetText("CPU (easy)");
            Player2Button.SetText("Change : CPU (hard)");
            GameSettings.isPlayer2CPU = true;
            GameSettings.CPUDifficulty = 1;
        }
        else if (count == 2)
        {
            current.SetText("CPU (hard)");
            Player2Button.SetText("Change : Human");
            GameSettings.isPlayer2CPU = false;
            GameSettings.CPUDifficulty = 2;
        }
        else if (count == 0)
        {
            current.SetText("Human");
            Player2Button.SetText("Change : CPU (easy)");
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
