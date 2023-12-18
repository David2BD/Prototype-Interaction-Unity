using System.Collections.Generic;
using UnityEngine;

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
    Audio,
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
    
    public Dictionary<PlayerAction, KeyCode> PlayerBlue;
    public Dictionary<PlayerAction, KeyCode> PlayerRed;
    public Dictionary<GeneralAction, KeyCode> GeneralActions;

    private string _player1Name = "Player 1"; // nom par defaut
    private string _player2Name = "Player 2"; // nom par defaut

    private int current_turn = 1;

    // to change musicLevel
    private int counterMusic = 0;
    
    // to know when to play hit/miss jingles
    private int[] ballHits = new int[2] {0, 0};
    private int[] ballMiss = new int[2] {0, 0};

    private int maxMiss = 2;
    private int maxHit = 2;
    
    // Singleton
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            
            // initialisation des valeurs de bases pour les controles
            // valeurs controles generaux
            GeneralActions = new Dictionary<GeneralAction, KeyCode>()
            {
                { GeneralAction.Pause, KeyCode.P },
                { GeneralAction.Confirm, KeyCode.Return },
                { GeneralAction.Audio, KeyCode.O },
                { GeneralAction.Quit, KeyCode.Escape }
            };
            
            // valeurs controles joueur 1
            PlayerBlue = new Dictionary<PlayerAction, KeyCode>()
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
            PlayerRed = new Dictionary<PlayerAction, KeyCode>()
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
    
    public Dictionary<PlayerAction, KeyCode> GetPlayerKeys(int player)
    {
        if (player == 1)
        {
            return PlayerBlue;
        }
        else
        {
            return PlayerRed;
        }
    }

    public string GetName (int player)
    {
        if (player == 1)
        {
            return _player1Name;
        }
        else
        {
            return _player2Name;
        }
    }

    public void SetName(int player, string name)
    {
        
        if (player == 1)
        {
            _player1Name = name;
        }
        else
        {
            _player2Name = name;
        }
    }

    public void setTurn(int t)
    {
        current_turn = t;
    }

    public int getTurn()
    {
        return current_turn;
    }

    public void updateMusicLevel()
    {
        counterMusic += 1;
    }

    public void setCounterMusicLevel(int i)
    {
        counterMusic = i;
    }

    public int getMusicLevel()
    {
        return counterMusic switch
        {
            0 => 1,
            1 => 2,
            2 => 3,
            3 => 3,
            4 => 4,
            _ => 5
        };
    }

    public bool getBallHit()
    {
        return (ballHits[0] == maxHit || ballHits[1] == maxHit) ;
    }

    public void ballHitIncrease()
    {
        if (current_turn == 1)
        {
            ballHits[0] += 1;
        }
        else
        {
            ballHits[1] += 1;
        }
        resetBallMiss();
    }
    
    public void resetBallHit()
    {
        if (ballHits[0] == maxHit)
        {
            ballHits[0] = 0;
        }
        
        if (ballHits[1] == maxHit)
        {
            ballHits[1] = 0;
        }
    }
    
    public bool getBallMiss()
    {
        return (ballMiss[0] == maxMiss || ballMiss[1] == maxMiss);
    }
    
    public void ballMissIncrease()
    {
        if (current_turn == 1)
        {
            ballMiss[0] += 1;
        }
        else
        {
            ballMiss[1] += 1;
        }

        resetBallHit();
    }
    
    public void resetBallMiss()
    {
        if (ballMiss[0] == maxMiss)
        {
            ballMiss[0] = 0;
        }

        if (ballMiss[1] == maxMiss)
        {
            ballMiss[1] = 0;
        }
    }
    
    
    public void SaveAllControls()
    {
        PlayerPrefs.DeleteAll();
        
        // names
        PlayerPrefs.SetString("Player1", GameManager.Instance._player1Name);
        PlayerPrefs.SetString("Player2", GameManager.Instance._player2Name);
        
        // general controls
        PlayerPrefs.SetString("Pause", GeneralActions[GeneralAction.Pause].ToString());
        PlayerPrefs.SetString("Confirm", GeneralActions[GeneralAction.Confirm].ToString());
        PlayerPrefs.SetString("Quit", GeneralActions[GeneralAction.Quit].ToString());
        
        // player 1 controls
        PlayerPrefs.SetString("Left1", PlayerBlue[PlayerAction.MoveLeft].ToString());
        PlayerPrefs.SetString("Right1", PlayerBlue[PlayerAction.MoveRight].ToString());
        PlayerPrefs.SetString("Aim1", PlayerBlue[PlayerAction.EnterAimingMode].ToString());
        PlayerPrefs.SetString("AimH1", PlayerBlue[PlayerAction.AimHigher].ToString());
        PlayerPrefs.SetString("AimL1", PlayerBlue[PlayerAction.AimLower].ToString());
        PlayerPrefs.SetString("Shoot1", PlayerBlue[PlayerAction.Shoot].ToString());
        PlayerPrefs.SetString("Jump1", PlayerBlue[PlayerAction.Jump].ToString());
        
        // player 2 controls
        PlayerPrefs.SetString("Left2", PlayerRed[PlayerAction.MoveLeft].ToString());
        PlayerPrefs.SetString("Right2", PlayerRed[PlayerAction.MoveRight].ToString());
        PlayerPrefs.SetString("Aim2", PlayerRed[PlayerAction.EnterAimingMode].ToString());
        PlayerPrefs.SetString("AimH2", PlayerRed[PlayerAction.AimHigher].ToString());
        PlayerPrefs.SetString("AimL2", PlayerRed[PlayerAction.AimLower].ToString());
        PlayerPrefs.SetString("Shoot2", PlayerRed[PlayerAction.Shoot].ToString());
        PlayerPrefs.SetString("Jump2", PlayerRed[PlayerAction.Jump].ToString());
        
        PlayerPrefs.Save();
    }

    public void LoadAllControls()
    {
        // names
        _player1Name = PlayerPrefs.GetString("Player1");
        _player2Name = PlayerPrefs.GetString("Player2");

        // general controls
        GeneralActions[GeneralAction.Pause] = (KeyCode)System.Enum.Parse(typeof(KeyCode),
            PlayerPrefs.GetString("Pause"));
        GeneralActions[GeneralAction.Confirm] = (KeyCode)System.Enum.Parse(typeof(KeyCode),
            PlayerPrefs.GetString("Confirm"));
        GeneralActions[GeneralAction.Quit] = (KeyCode)System.Enum.Parse(typeof(KeyCode),
            PlayerPrefs.GetString("Quit"));
        
        // player 1 controls
        PlayerBlue[PlayerAction.MoveLeft] = (KeyCode)System.Enum.Parse(typeof(KeyCode),
            PlayerPrefs.GetString("Left1"));
        PlayerBlue[PlayerAction.MoveRight] = (KeyCode)System.Enum.Parse(typeof(KeyCode),
            PlayerPrefs.GetString("Right1"));
        PlayerBlue[PlayerAction.EnterAimingMode] = (KeyCode)System.Enum.Parse(typeof(KeyCode),
            PlayerPrefs.GetString("Aim1"));
        PlayerBlue[PlayerAction.AimHigher] = (KeyCode)System.Enum.Parse(typeof(KeyCode),
            PlayerPrefs.GetString("AimH1"));
        PlayerBlue[PlayerAction.AimLower] = (KeyCode)System.Enum.Parse(typeof(KeyCode),
            PlayerPrefs.GetString("AimL1"));
        PlayerBlue[PlayerAction.Shoot] = (KeyCode)System.Enum.Parse(typeof(KeyCode),
            PlayerPrefs.GetString("Shoot1"));
        PlayerBlue[PlayerAction.Jump] = (KeyCode)System.Enum.Parse(typeof(KeyCode),
            PlayerPrefs.GetString("Jump1"));

        // player 2 controls
        PlayerRed[PlayerAction.MoveLeft] = (KeyCode)System.Enum.Parse(typeof(KeyCode),
            PlayerPrefs.GetString("Left2"));
        PlayerRed[PlayerAction.MoveRight] = (KeyCode)System.Enum.Parse(typeof(KeyCode),
            PlayerPrefs.GetString("Right2"));
        PlayerRed[PlayerAction.EnterAimingMode] = (KeyCode)System.Enum.Parse(typeof(KeyCode),
            PlayerPrefs.GetString("Aim2"));
        PlayerRed[PlayerAction.AimHigher] = (KeyCode)System.Enum.Parse(typeof(KeyCode),
            PlayerPrefs.GetString("AimH2"));
        PlayerRed[PlayerAction.AimLower] = (KeyCode)System.Enum.Parse(typeof(KeyCode),
            PlayerPrefs.GetString("AimL2"));
        PlayerRed[PlayerAction.Shoot] = (KeyCode)System.Enum.Parse(typeof(KeyCode),
            PlayerPrefs.GetString("Shoot2"));
        PlayerRed[PlayerAction.Jump] = (KeyCode)System.Enum.Parse(typeof(KeyCode),
            PlayerPrefs.GetString("Jump2"));
    }
}
