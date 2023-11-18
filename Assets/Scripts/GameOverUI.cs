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
        if (Input.GetKey(KeyCode.Return))
        {
            SceneManager.LoadScene("Main Menu");
            
        }
    }

    public void SetWinner(int winningTeam)
    {
        if (winningTeam == 1)
        {
            winnerText.SetText("Blue Win");
        }
        else if (winningTeam == 2)
        {
            winnerText.SetText("Red Win");
        }
    }
}
