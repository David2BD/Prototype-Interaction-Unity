using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class Options : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject controllerMenu;
    public GameObject optionMenu;

    private int currentPlayer = 1;
    public TextMeshProUGUI textOptions;
    
    public TMP_InputField player1;
    public TMP_InputField player2;

    public void Update()
    {
        player1.text = GameManager.Instance.getName(1);
        player2.text = GameManager.Instance.getName(2);
        
        switch (currentPlayer)
        {
            case 1:
                textOptions.SetText(GameManager.Instance.getName(1) + " updating controls.");
                break;
            case 2:
                textOptions.SetText(GameManager.Instance.getName(2) + " updating controls.");
                break;
        }
    }

    public void setControls()
    {
        controllerMenu.SetActive(!controllerMenu.activeSelf);
        optionMenu.SetActive(false);
        Debug.Log("Player enter settings menu");
    }

    public void changeName(int player)
    {
        if (player == 1 && player1.text != null)
        {
            GameManager.Instance.setName(1, player1.text.ToString());
        }
        else if (player == 2 && player2.text != null)
        {
            GameManager.Instance.setName(2, player2.text.ToString());
        }
    }

    public void updateControlQWERTY()
    {
        controllerMenu.GetComponent<InputManager>().setQWERTY(currentPlayer);
    }
    
    public void updateControlArrow()
    {
        controllerMenu.GetComponent<InputManager>().setArrows(currentPlayer);
    }

    public void changePlayer()
    {
        switch (currentPlayer)
        {
            case 1 :
                currentPlayer = 2;
                break;
            case 2 :
                currentPlayer = 1;
                break;
        }
    }
    
    public void Return()
    {
        mainMenu.SetActive(!mainMenu.activeSelf);
        optionMenu.SetActive(false);
        Debug.Log("Player go back to game menu");
    }
}
