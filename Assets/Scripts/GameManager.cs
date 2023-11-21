using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

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

    public KeyCode test { get; set; }
    
    public Dictionary<InputManager.PlayerAction, KeyCode> playerBlue;
    public Dictionary<InputManager.PlayerAction, KeyCode> playerRed;
    public Dictionary<InputManager.GeneralAction, KeyCode> generalActions;

    private string player1_name = "Player 1";
    private string player2_name = "Player 2";
        
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    private void Update()
    {
    }
    
    void Awake()
    {
        /*
        MoveLeft = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("moveLeft", "RightArrow"));
        MoveRight = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("moveRight", "LeftArrow"));
        EnterAimingMode = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("setAim", "Z"));
        AimHigher = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("aimHigher", "UpArrow"));
        AimLower = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("aimLower", "DownArrow"));
        Shoot = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("shoot", "Space"));
        */

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
}
