using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public TextMeshProUGUI winnerText;

    public void Update()
    {
        if (Input.GetKey(GameManager.Instance.generalActions[GeneralAction.Quit]))
        {
            SceneManager.LoadScene("Main Menu");
            
        }
    }

    public void SetWinner(int winningTeam)
    {
        if (winningTeam == 1)
        {
            winnerText.SetText(GameManager.Instance.getName(1).ToString() + " win" +
                               "\nPress " + GameManager.Instance.generalActions[GeneralAction.Quit].ToString() + " to go back.");
        }
        else if (winningTeam == 2)
        {
            winnerText.SetText(GameManager.Instance.getName(2).ToString() + " win " + 
                               "\nPress " + GameManager.Instance.generalActions[GeneralAction.Quit].ToString() + " to go back.");
        }
    }
}
