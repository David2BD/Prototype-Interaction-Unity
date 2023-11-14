using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;

    public KeyCode MoveLeft { get; set; }
    public KeyCode MoveRight { get; set; }
    public KeyCode EnterAimingMode { get; set; }
    public KeyCode AimHigher { get; set; }
    public KeyCode AimLower { get; set; }
    public KeyCode Shoot { get; set; }

    void Awake()
    {
        /*
        if (GM != null)
        {
            DontDestroyOnLoad(gameObject);
            GM = this;
        }
        else if (GM != this)
        {
            Destroy(gameObject);
        }
        
        MoveLeft = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("moveLeft", "RightArrow"));
        MoveRight = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("moveRight", "LeftArrow"));
        EnterAimingMode = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("setAim", "Z"));
        AimHigher = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("aimHigher", "UpArrow"));
        AimLower = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("aimLower", "DownArrow"));
        Shoot = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("shoot", "Space"));
        */
    }
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
