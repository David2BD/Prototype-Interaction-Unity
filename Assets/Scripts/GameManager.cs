using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// possible actions player can do
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

// general controls
public enum GeneralAction
{
    Confirm,
    Pause,
    Quit
}

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    // Singleton
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    GameObject singleton = new GameObject("GameManagerSingleton");
                    _instance = singleton.AddComponent<GameManager>();
                }
            }

            return _instance;
        }
    }
    
    public Dictionary<PlayerAction, KeyCode> playerBlue;
    public Dictionary<PlayerAction, KeyCode> playerRed;
    public Dictionary<GeneralAction, KeyCode> generalActions;

    private string player1_name = "Player 1"; // nom par defaut
    private string player2_name = "Player 2"; // nom par defaut
    
    // Singleton
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            
            // initialisation des valeurs de bases pour les controles
            // valeurs controles generaux
            generalActions = new Dictionary<GeneralAction, KeyCode>()
            {
                { GeneralAction.Pause, KeyCode.P },
                { GeneralAction.Confirm, KeyCode.Return },
                { GeneralAction.Quit, KeyCode.Escape }
            };
            
            // valeurs controles joueur 1
            playerBlue = new Dictionary<PlayerAction, KeyCode>()
            {
                { PlayerAction.MoveLeft, KeyCode.LeftArrow },
                { PlayerAction.MoveRight, KeyCode.RightArrow },
                { PlayerAction.Jump, KeyCode.UpArrow },
                { PlayerAction.EnterAimingMode, KeyCode.Z },
                { PlayerAction.AimHigher, KeyCode.UpArrow },
                { PlayerAction.AimLower, KeyCode.DownArrow },
                { PlayerAction.Shoot, KeyCode.Space },
            };
            
            // valeurs controles joueur 2
            playerRed = new Dictionary<PlayerAction, KeyCode>()
            {
                { PlayerAction.MoveLeft, KeyCode.LeftArrow },
                { PlayerAction.MoveRight, KeyCode.RightArrow },
                { PlayerAction.Jump, KeyCode.UpArrow },
                { PlayerAction.EnterAimingMode, KeyCode.Z },
                { PlayerAction.AimHigher, KeyCode.UpArrow },
                { PlayerAction.AimLower, KeyCode.DownArrow },
                { PlayerAction.Shoot, KeyCode.Space },
            };
            
            // existe entre les scenes
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public Dictionary<PlayerAction, KeyCode> getPlayerKeys(int player)
    {
        if (player == 1)
        {
            return playerBlue;
        }
        else
        {
            return playerRed;
        }
    }

    public string getName (int player)
    {
        if (player == 1)
        {
            return player1_name;
        }
        else
        {
            return player2_name;
        }
    }

    public void setName(int player, string name)
    {
        if (player == 1)
        {
            player1_name = name;
        }
        else
        {
            player2_name = name;
        }
    }
    
    public void saveAllControls()
    {
        PlayerPrefs.DeleteAll();
        
        // names
        PlayerPrefs.SetString("Player1", GameManager.Instance.player1_name);
        PlayerPrefs.SetString("Player2", GameManager.Instance.player2_name);
        
        // general controls
        PlayerPrefs.SetString("Pause", generalActions[GeneralAction.Pause].ToString());
        PlayerPrefs.SetString("Confirm", generalActions[GeneralAction.Confirm].ToString());
        PlayerPrefs.SetString("Quit", generalActions[GeneralAction.Quit].ToString());
        
        // player 1 controls
        PlayerPrefs.SetString("Left1", playerBlue[PlayerAction.MoveLeft].ToString());
        PlayerPrefs.SetString("Right1", playerBlue[PlayerAction.MoveRight].ToString());
        PlayerPrefs.SetString("Aim1", playerBlue[PlayerAction.EnterAimingMode].ToString());
        PlayerPrefs.SetString("AimH1", playerBlue[PlayerAction.AimHigher].ToString());
        PlayerPrefs.SetString("AimL1", playerBlue[PlayerAction.AimLower].ToString());
        PlayerPrefs.SetString("Shoot1", playerBlue[PlayerAction.Shoot].ToString());
        PlayerPrefs.SetString("Jump1", playerBlue[PlayerAction.Jump].ToString());
        
        // player 2 controls
        PlayerPrefs.SetString("Left2", playerRed[PlayerAction.MoveLeft].ToString());
        PlayerPrefs.SetString("Right2", playerRed[PlayerAction.MoveRight].ToString());
        PlayerPrefs.SetString("Aim2", playerRed[PlayerAction.EnterAimingMode].ToString());
        PlayerPrefs.SetString("AimH2", playerRed[PlayerAction.AimHigher].ToString());
        PlayerPrefs.SetString("AimL2", playerRed[PlayerAction.AimLower].ToString());
        PlayerPrefs.SetString("Shoot2", playerRed[PlayerAction.Shoot].ToString());
        PlayerPrefs.SetString("Jump2", playerRed[PlayerAction.Jump].ToString());
        
        PlayerPrefs.Save();
    }

    public void loadAllControls()
    {
        // names
        player1_name = PlayerPrefs.GetString("Player1");
        player2_name = PlayerPrefs.GetString("Player2");

        // general controls
        generalActions[GeneralAction.Pause] = (KeyCode)System.Enum.Parse(typeof(KeyCode),
            PlayerPrefs.GetString("Pause"));
        generalActions[GeneralAction.Confirm] = (KeyCode)System.Enum.Parse(typeof(KeyCode),
            PlayerPrefs.GetString("Confirm"));
        generalActions[GeneralAction.Quit] = (KeyCode)System.Enum.Parse(typeof(KeyCode),
            PlayerPrefs.GetString("Quit"));
        
        // player 1 controls
        playerBlue[PlayerAction.MoveLeft] = (KeyCode)System.Enum.Parse(typeof(KeyCode),
            PlayerPrefs.GetString("Left1"));
        playerBlue[PlayerAction.MoveRight] = (KeyCode)System.Enum.Parse(typeof(KeyCode),
            PlayerPrefs.GetString("Right1"));
        playerBlue[PlayerAction.EnterAimingMode] = (KeyCode)System.Enum.Parse(typeof(KeyCode),
            PlayerPrefs.GetString("Aim1"));
        playerBlue[PlayerAction.AimHigher] = (KeyCode)System.Enum.Parse(typeof(KeyCode),
            PlayerPrefs.GetString("AimH1"));
        playerBlue[PlayerAction.AimLower] = (KeyCode)System.Enum.Parse(typeof(KeyCode),
            PlayerPrefs.GetString("AimL1"));
        playerBlue[PlayerAction.Shoot] = (KeyCode)System.Enum.Parse(typeof(KeyCode),
            PlayerPrefs.GetString("Shoot1"));
        playerBlue[PlayerAction.Jump] = (KeyCode)System.Enum.Parse(typeof(KeyCode),
            PlayerPrefs.GetString("Jump1"));

        // player 2 controls
        playerRed[PlayerAction.MoveLeft] = (KeyCode)System.Enum.Parse(typeof(KeyCode),
            PlayerPrefs.GetString("Left2"));
        playerRed[PlayerAction.MoveRight] = (KeyCode)System.Enum.Parse(typeof(KeyCode),
            PlayerPrefs.GetString("Right2"));
        playerRed[PlayerAction.EnterAimingMode] = (KeyCode)System.Enum.Parse(typeof(KeyCode),
            PlayerPrefs.GetString("Aim2"));
        playerRed[PlayerAction.AimHigher] = (KeyCode)System.Enum.Parse(typeof(KeyCode),
            PlayerPrefs.GetString("AimH2"));
        playerRed[PlayerAction.AimLower] = (KeyCode)System.Enum.Parse(typeof(KeyCode),
            PlayerPrefs.GetString("AimL2"));
        playerRed[PlayerAction.Shoot] = (KeyCode)System.Enum.Parse(typeof(KeyCode),
            PlayerPrefs.GetString("Shoot2"));
        playerRed[PlayerAction.Jump] = (KeyCode)System.Enum.Parse(typeof(KeyCode),
            PlayerPrefs.GetString("Jump2"));
    }
}
