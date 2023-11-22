using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    
    public Dictionary<InputManager.PlayerAction, KeyCode> playerBlue;
    public Dictionary<InputManager.PlayerAction, KeyCode> playerRed;
    public Dictionary<InputManager.GeneralAction, KeyCode> generalActions;

    private string player1_name = "Player 1"; // nom par defaut
    private string player2_name = "Player 2"; // nom par defaut
        
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    private void Update()
    {
    }
    
    // Singleton
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public Dictionary<InputManager.PlayerAction, KeyCode> getPlayerKeys(int player)
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

    public void InputManagerUpdate(Dictionary<InputManager.PlayerAction, KeyCode> blue,
        Dictionary<InputManager.PlayerAction, KeyCode> red,
        Dictionary<InputManager.GeneralAction, KeyCode> general)
    {
        playerBlue = blue;
        playerRed = red;
        generalActions = general;
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
        PlayerPrefs.SetString("Pause", generalActions[InputManager.GeneralAction.Pause].ToString());
        PlayerPrefs.SetString("Confirm", generalActions[InputManager.GeneralAction.Confirm].ToString());
        PlayerPrefs.SetString("Quit", generalActions[InputManager.GeneralAction.Quit].ToString());
        
        // player 1 controls
        PlayerPrefs.SetString("Left1", playerBlue[InputManager.PlayerAction.MoveLeft].ToString());
        PlayerPrefs.SetString("Right1", playerBlue[InputManager.PlayerAction.MoveRight].ToString());
        PlayerPrefs.SetString("Aim1", playerBlue[InputManager.PlayerAction.EnterAimingMode].ToString());
        PlayerPrefs.SetString("AimH1", playerBlue[InputManager.PlayerAction.AimHigher].ToString());
        PlayerPrefs.SetString("AimL1", playerBlue[InputManager.PlayerAction.AimLower].ToString());
        PlayerPrefs.SetString("Shoot1", playerBlue[InputManager.PlayerAction.Shoot].ToString());
        PlayerPrefs.SetString("Jump1", playerBlue[InputManager.PlayerAction.Jump].ToString());
        
        // player 2 controls
        PlayerPrefs.SetString("Left2", playerRed[InputManager.PlayerAction.MoveLeft].ToString());
        PlayerPrefs.SetString("Right2", playerRed[InputManager.PlayerAction.MoveRight].ToString());
        PlayerPrefs.SetString("Aim2", playerRed[InputManager.PlayerAction.EnterAimingMode].ToString());
        PlayerPrefs.SetString("AimH2", playerRed[InputManager.PlayerAction.AimHigher].ToString());
        PlayerPrefs.SetString("AimL2", playerRed[InputManager.PlayerAction.AimLower].ToString());
        PlayerPrefs.SetString("Shoot2", playerRed[InputManager.PlayerAction.Shoot].ToString());
        PlayerPrefs.SetString("Jump2", playerRed[InputManager.PlayerAction.Jump].ToString());
        
        PlayerPrefs.Save();
    }

    public void loadAllControls()
    {
        // names
        player1_name = PlayerPrefs.GetString("Player1");
        player2_name = PlayerPrefs.GetString("Player2");

        // general controls
        generalActions[InputManager.GeneralAction.Pause] = (KeyCode)System.Enum.Parse(typeof(KeyCode),
            PlayerPrefs.GetString("Pause"));
        generalActions[InputManager.GeneralAction.Confirm] = (KeyCode)System.Enum.Parse(typeof(KeyCode),
            PlayerPrefs.GetString("Confirm"));
        generalActions[InputManager.GeneralAction.Quit] = (KeyCode)System.Enum.Parse(typeof(KeyCode),
            PlayerPrefs.GetString("Quit"));
        
        // player 1 controls
        playerBlue[InputManager.PlayerAction.MoveLeft] = (KeyCode)System.Enum.Parse(typeof(KeyCode),
            PlayerPrefs.GetString("Left1"));
        playerBlue[InputManager.PlayerAction.MoveRight] = (KeyCode)System.Enum.Parse(typeof(KeyCode),
            PlayerPrefs.GetString("Right1"));
        playerBlue[InputManager.PlayerAction.EnterAimingMode] = (KeyCode)System.Enum.Parse(typeof(KeyCode),
            PlayerPrefs.GetString("Aim1"));
        playerBlue[InputManager.PlayerAction.AimHigher] = (KeyCode)System.Enum.Parse(typeof(KeyCode),
            PlayerPrefs.GetString("AimH1"));
        playerBlue[InputManager.PlayerAction.AimLower] = (KeyCode)System.Enum.Parse(typeof(KeyCode),
            PlayerPrefs.GetString("AimL1"));
        playerBlue[InputManager.PlayerAction.Shoot] = (KeyCode)System.Enum.Parse(typeof(KeyCode),
            PlayerPrefs.GetString("Shoot1"));
        playerBlue[InputManager.PlayerAction.Jump] = (KeyCode)System.Enum.Parse(typeof(KeyCode),
            PlayerPrefs.GetString("Jump1"));

        // player 2 controls
        playerRed[InputManager.PlayerAction.MoveLeft] = (KeyCode)System.Enum.Parse(typeof(KeyCode),
            PlayerPrefs.GetString("Left2"));
        playerRed[InputManager.PlayerAction.MoveRight] = (KeyCode)System.Enum.Parse(typeof(KeyCode),
            PlayerPrefs.GetString("Right2"));
        playerRed[InputManager.PlayerAction.EnterAimingMode] = (KeyCode)System.Enum.Parse(typeof(KeyCode),
            PlayerPrefs.GetString("Aim2"));
        playerRed[InputManager.PlayerAction.AimHigher] = (KeyCode)System.Enum.Parse(typeof(KeyCode),
            PlayerPrefs.GetString("AimH2"));
        playerRed[InputManager.PlayerAction.AimLower] = (KeyCode)System.Enum.Parse(typeof(KeyCode),
            PlayerPrefs.GetString("AimL2"));
        playerRed[InputManager.PlayerAction.Shoot] = (KeyCode)System.Enum.Parse(typeof(KeyCode),
            PlayerPrefs.GetString("Shoot2"));
        playerRed[InputManager.PlayerAction.Jump] = (KeyCode)System.Enum.Parse(typeof(KeyCode),
            PlayerPrefs.GetString("Jump2"));
    }
}
