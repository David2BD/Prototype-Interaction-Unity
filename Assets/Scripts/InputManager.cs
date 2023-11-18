using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public GameObject settingsMenu;
    public GameObject inputMenu;
    private Dictionary<PlayerAction, KeyCode> playerBlue;
    private Dictionary<PlayerAction, KeyCode> playerRed;
    
    // all possible actions that need an input
    // add jump later?
    public enum PlayerAction
    {
        MoveLeft,
        MoveRight,
        EnterAimingMode,
        AimHigher,
        AimLower,
        Shoot
    }

    public void Start()
    {
        setInitialConfiguration(playerBlue);
        setInitialConfiguration(playerRed);
    }

    public void setInitialConfiguration(Dictionary<PlayerAction, KeyCode> player)
    {
        player.Add(PlayerAction.MoveLeft, KeyCode.RightArrow);
        player.Add(PlayerAction.MoveRight, KeyCode.LeftArrow);
        player.Add(PlayerAction.EnterAimingMode, KeyCode.Z);
        player.Add(PlayerAction.AimHigher, KeyCode.UpArrow);
        player.Add(PlayerAction.AimLower, KeyCode.DownArrow);
        player.Add(PlayerAction.Shoot, KeyCode.Space);
    }

    public void setArrows()
    {
        setArrowsKeys(playerBlue);
        setArrowsKeys(playerRed);
    }
    
    private void setArrowsKeys(Dictionary<PlayerAction, KeyCode> dict)
    {
        dict[PlayerAction.MoveLeft] = KeyCode.RightArrow;
        dict[PlayerAction.MoveRight] = KeyCode.LeftArrow;
        dict[PlayerAction.EnterAimingMode] = KeyCode.Z;
        dict[PlayerAction.AimHigher] = KeyCode.UpArrow;
        dict[PlayerAction.AimLower] = KeyCode.DownArrow;
        dict[PlayerAction.Shoot] = KeyCode.Space;
    }

    public void setQWERTY()
    {
        setQWERTY_Keys(playerBlue);
        setQWERTY_Keys(playerRed);
    }

    private void setQWERTY_Keys(Dictionary<PlayerAction, KeyCode> dict)
    {
        dict[PlayerAction.MoveLeft] = KeyCode.A;
        dict[PlayerAction.MoveRight] = KeyCode.D;
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

    public void updateKeyAction(PlayerAction pA, Dictionary<PlayerAction, KeyCode> dict)
    {
        //dict[pA] = InputField();
    }
    
    public void Return()
    {
        settingsMenu.SetActive(!settingsMenu.activeSelf);
        inputMenu.SetActive(false);
        Debug.Log("Player go back to settings menu");
    }
}
