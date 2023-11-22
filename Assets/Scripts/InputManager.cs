using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public GameObject settingsMenu;
    public GameObject inputMenu;

    public Transform player1;
    public Transform player2;
    
    private Dictionary<PlayerAction, KeyCode> playerBlue = new Dictionary<PlayerAction, KeyCode>{};
    private Dictionary<PlayerAction, KeyCode> playerRed = new Dictionary<PlayerAction, KeyCode>{};
    private Dictionary<GeneralAction, KeyCode> generalActions = new Dictionary<GeneralAction, KeyCode>{};
    
    private int playerKey = 1;
    private int childKey = 0;
    private bool gettingKey = true;
    private bool waitingKey = false;
    private KeyCode newKey = KeyCode.None;

    public TextMeshProUGUI keyUpdate;

    public TMP_InputField pause;
    public TMP_InputField confirm;
    public TMP_InputField quit;
    
    public enum PlayerAction
    {
        MoveLeft,
        MoveRight,
        EnterAimingMode,
        AimHigher,
        AimLower,
        Shoot,
        Jump
    }

    public enum GeneralAction
    {
        Confirm,
        Pause,
        Quit
    }

    public void Update()
    {
        // player names
        player1.GetComponentInChildren<TextMeshProUGUI>().SetText(GameManager.Instance.getName(1));
        player2.GetComponentInChildren<TextMeshProUGUI>().SetText(GameManager.Instance.getName(2));
        
        // get key values
        getKeyValues(player1, 1);
        getKeyValues(player2, 2);

        pause.text = GameManager.Instance.generalActions[GeneralAction.Pause].ToString();
        confirm.text = GameManager.Instance.generalActions[GeneralAction.Confirm].ToString();
        quit.text = GameManager.Instance.generalActions[GeneralAction.Quit].ToString();

        switch (playerKey)
        {
            case 1:
                keyUpdate.SetText(GameManager.Instance.getName(1) + " updating key.");
                break;
            case 2:
                keyUpdate.SetText(GameManager.Instance.getName(2) + " updating key.");
                break;
            case 3:
                keyUpdate.SetText( "Updating general controls.");
                break;
        }
        
        // when setting a new keyCode
        if (waitingKey)
        {
            if (playerKey == 3)
            { 
                if (Input.anyKey) 
                { 
                    foreach (KeyCode k in System.Enum.GetValues(typeof(KeyCode))) 
                    { 
                        if (Input.GetKeyDown(k) && k != KeyCode.Mouse0) 
                        { 
                            newKey = k; 
                            updateGeneral(childKey);
                            waitingKey = false;
                            newKey = KeyCode.None;
                            break;
                        }
                    }
                }
            }
            else if (playerKey != 3 && childKey != 0)
            {
                if (Input.anyKey)
                { 
                    foreach (KeyCode k in System.Enum.GetValues(typeof(KeyCode))) 
                    { 
                        if (Input.GetKeyDown(k) && k != KeyCode.Mouse0) 
                        { 
                            newKey = k; 
                            updateKeyAction(playerKey); 
                            waitingKey = false; 
                            newKey = KeyCode.None; 
                            break;
                        } 
                    }
                }
            }
        }
    }

    public void setControls()
    {
        setInitialConfiguration(playerBlue);
        setInitialConfiguration(playerRed);
        setGeneralConfiguration();
        GameManager.Instance.InputManagerUpdate(playerBlue, playerRed, generalActions);
    }
    

    private void setInitialConfiguration(Dictionary<PlayerAction, KeyCode> player)
    {
        player.Add(PlayerAction.MoveLeft, KeyCode.LeftArrow);
        player.Add(PlayerAction.MoveRight, KeyCode.RightArrow);
        player.Add(PlayerAction.Jump, KeyCode.UpArrow);
        player.Add(PlayerAction.EnterAimingMode, KeyCode.Z);
        player.Add(PlayerAction.AimHigher, KeyCode.UpArrow);
        player.Add(PlayerAction.AimLower, KeyCode.DownArrow);
        player.Add(PlayerAction.Shoot, KeyCode.Space);
    }

    private void setGeneralConfiguration()
    {
        generalActions.Add(GeneralAction.Confirm, KeyCode.Return);
        generalActions.Add(GeneralAction.Pause, KeyCode.P);
        generalActions.Add(GeneralAction.Quit, KeyCode.Escape);
    }

    public void setArrows(int player)
    {
        if (player == 1)
        {
            setArrowsKeys(playerBlue);
        }
        else
        {
            setArrowsKeys(playerRed);
        }
        GameManager.Instance.InputManagerUpdate(playerBlue, playerRed, generalActions);
    }
    
    private void setArrowsKeys(Dictionary<PlayerAction, KeyCode> dict)
    {
        dict[PlayerAction.MoveLeft] = KeyCode.LeftArrow;
        dict[PlayerAction.MoveRight] = KeyCode.RightArrow;
        dict[PlayerAction.Jump] = KeyCode.UpArrow;
        dict[PlayerAction.EnterAimingMode] = KeyCode.Z;
        dict[PlayerAction.AimHigher] = KeyCode.UpArrow;
        dict[PlayerAction.AimLower] = KeyCode.DownArrow;
        dict[PlayerAction.Shoot] = KeyCode.Space;
    }

    public void setQWERTY(int player)
    {
        if (player == 1)
        {
            setQWERTY_Keys(playerBlue);
        }
        else
        {
            setQWERTY_Keys(playerRed);
        }
        GameManager.Instance.InputManagerUpdate(playerBlue, playerRed, generalActions);
    }

    private void setQWERTY_Keys(Dictionary<PlayerAction, KeyCode> dict)
    {
        dict[PlayerAction.MoveLeft] = KeyCode.A;
        dict[PlayerAction.MoveRight] = KeyCode.D;
        dict[PlayerAction.Jump] = KeyCode.W;
        dict[PlayerAction.EnterAimingMode] = KeyCode.Q;
        dict[PlayerAction.AimHigher] = KeyCode.W;
        dict[PlayerAction.AimLower] = KeyCode.S;
        dict[PlayerAction.Shoot] = KeyCode.E;
    }
    
    public Dictionary<PlayerAction, KeyCode> getPlayerBlue()
    {
        return playerBlue;
    }
   
    public Dictionary<PlayerAction, KeyCode> getPlayerRed()
    {
        return playerRed;
    }
    
    
    private void updateKeyAction(int p)
    {
        if (newKey != KeyCode.None)
        {
            switch (childKey)
            {
                case 1:
                    GameManager.Instance.getPlayerKeys(p)[PlayerAction.MoveLeft] = newKey;
                    break;
                case 2:
                    GameManager.Instance.getPlayerKeys(p)[PlayerAction.MoveRight] = newKey;
                    break;
                case 3:
                    GameManager.Instance.getPlayerKeys(p)[PlayerAction.EnterAimingMode] = newKey;
                    break;
                case 4:
                    GameManager.Instance.getPlayerKeys(p)[PlayerAction.AimHigher] = newKey;
                    break;
                case 5:
                    GameManager.Instance.getPlayerKeys(p)[PlayerAction.AimLower] = newKey;
                    break;
                case 6:
                    GameManager.Instance.getPlayerKeys(p)[PlayerAction.Shoot] = newKey;
                    break;
                case 7:
                    GameManager.Instance.getPlayerKeys(p)[PlayerAction.Jump] = newKey;
                    break;
            }
        }
    }

    public void changePlayer()
    {
        switch (playerKey)
        {
            case 1 :
                playerKey = 2;
                break;
            case 2 :
                playerKey = 3;
                break;
            case 3 :
                playerKey = 1;
                break;
        }
    }

    
    private void getKeyValues(Transform player, int p)
    {
        gettingKey = true;
        // key values player 1
        for (int i = 1; i < player.childCount; i++)
        {
            if (player.GetChild(i).name == "GoLeft")
            {
                player.GetChild(i).GetComponent<TMP_InputField>().text = 
                    GameManager.Instance.getPlayerKeys(p)[PlayerAction.MoveLeft].ToString();
            }
            else if (player.GetChild(i).name == "GoRight")
            {
                player.GetChild(i).GetComponent<TMP_InputField>().text = 
                    GameManager.Instance.getPlayerKeys(p)[PlayerAction.MoveRight].ToString();
            }
            else if (player.GetChild(i).name == "EnterAim")
            {
                player.GetChild(i).GetComponent<TMP_InputField>().text = 
                    GameManager.Instance.getPlayerKeys(p)[PlayerAction.EnterAimingMode].ToString();
            }
            else if (player.GetChild(i).name == "AimHigh")
            {
                player.GetChild(i).GetComponent<TMP_InputField>().text = 
                    GameManager.Instance.getPlayerKeys(p)[PlayerAction.AimHigher].ToString();
            }
            else if (player.GetChild(i).name == "AimLow")
            {
                player.GetChild(i).GetComponent<TMP_InputField>().text = 
                    GameManager.Instance.getPlayerKeys(p)[PlayerAction.AimLower].ToString();
            }
            else if (player.GetChild(i).name == "Shoot")
            {
                player.GetChild(i).GetComponent<TMP_InputField>().text = 
                    GameManager.Instance.getPlayerKeys(p)[PlayerAction.Shoot].ToString();
            }
            else if (player.GetChild(i).name == "Jump")
            {
                player.GetChild(i).GetComponent<TMP_InputField>().text = 
                    GameManager.Instance.getPlayerKeys(p)[PlayerAction.Jump].ToString();
                
            }
        }

        gettingKey = false;
    }
    
    public void StartWaitingForKey( int child)
    {
        if (!gettingKey)
        {
            waitingKey = true;
            childKey = child;
        }
    }

    public void updateGeneral(int i)
    {
        switch (i)
        {
            case 1:
                GameManager.Instance.generalActions[GeneralAction.Pause] = newKey;
                break;
            case 2:
                GameManager.Instance.generalActions[GeneralAction.Confirm] = newKey;
                break;
            case 3:
                GameManager.Instance.generalActions[GeneralAction.Quit] = newKey;
                break;
        }
    }
    
    public void Return()
    {
        settingsMenu.SetActive(!settingsMenu.activeSelf);
        inputMenu.SetActive(false);
        Debug.Log("Player go back to settings menu");
    }

    public void resetGeneral()
    {
        GameManager.Instance.generalActions[GeneralAction.Pause] = KeyCode.P;
        GameManager.Instance.generalActions[GeneralAction.Confirm] = KeyCode.Return;
        GameManager.Instance.generalActions[GeneralAction.Quit] = KeyCode.Escape;
    }
}
