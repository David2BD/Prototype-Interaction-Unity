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


    public TextMeshProUGUI Player2Button;
    public TextMeshProUGUI current;

    public TextMeshProUGUI player1_name;
    public TextMeshProUGUI player2_name;

    void Start()
    {
        //Player2Button = GetComponent<TextMeshProUGUI>();
        //current = GetComponent<TextMeshProUGUI>();
        //player1_name = GetComponent<TextMeshProUGUI>();
        //player2_name = GetComponent<TextMeshProUGUI>();
        
        
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
        if (player1_name != null && player2_name != null)
        {
            player1_name.SetText(GameManager.Instance.getName(1));
            player2_name.SetText(GameManager.Instance.getName(2));
        }
    }

    public void Play()
    {
        SceneManager.LoadScene("LoadingScreen");
    }

    public void Options()
    {
        optionsMenu.SetActive(!optionsMenu.activeSelf);
        mainMenu.SetActive(false);
        Debug.Log("Player enter game settings");
    }

    public void ChangePlayer2()
    {
        count = (count + 1) % 2;
        
        // le texte du bouton est le niveau/adversaire
        if (count == 1)
        {
            current.SetText("CPU (easy)");
            Player2Button.SetText("Change : Human");
            GameSettings.isPlayer2CPU = true;
            GameSettings.CPUDifficulty = 1;
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
