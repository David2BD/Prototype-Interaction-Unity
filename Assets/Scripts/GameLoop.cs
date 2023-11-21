
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    public int playerTurn;
    public int selectedSoldierBlue;
    public int selectedSoldierRed;
    public List<Soldier> blueTeamUnits = new List<Soldier>();
    public List<Soldier> redTeamUnits = new List<Soldier>();
    
    public GameObject GameOverScreen;
    public GameObject TextManagerGameOver;

    public GameObject pauseMenu;
    
    public GameObject GameUI;

    public GameObject TextManager;
    
    private Ball activeBall;

    private bool isPlayer2CPU;
    private int CPUDiff;

    public Vector3 distance;                        //Fordebugging purposes

    //private Dictionary<InputManager.PlayerAction, KeyCode> currentPlayer;
    
 
    void Start()
    {
        playerTurn = 1;
        selectedSoldierBlue = 0;
        //currentPlayer = InputManager.getPlayerBlue();
        
        RegisterSoldier(new Soldier(), 1);
        RegisterSoldier(new Soldier(), 2);
        
        TextManager.GetComponent<textManager>().setTeam(1);

        isPlayer2CPU = GameSettings.isPlayer2CPU;
        CPUDiff = GameSettings.CPUDifficulty;

    }

    void Update()
    {
        activeBall = FindObjectOfType<Ball>();
        
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
        
        if (activeBall == null)
        {
            if (playerTurn == 1)
            {
                playTurn(1, blueTeamUnits, selectedSoldierBlue);
                TextManager.GetComponent<textManager>().setTeam(1);
            }
            else if (playerTurn == 2 && isPlayer2CPU == false)
            {
                playTurn(2, redTeamUnits, selectedSoldierRed);
                TextManager.GetComponent<textManager>().setTeam(2);
            }
            else if (playerTurn == 2 && isPlayer2CPU == true)
            {
                playTurnCPU(2, redTeamUnits, selectedSoldierRed, CPUDiff);
                TextManager.GetComponent<textManager>().setTeam(2);
            }
        }
    }

    void TogglePause()
    {
        // Toggle the pause state
        if (Time.timeScale == 0f)
        {
            // Resume the game
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
        }
        else
        {
            // Pause the game
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
        }
    }
    
    public void RegisterSoldier(Soldier soldier, int team)
    {
        if (team == 1)
        { 
            blueTeamUnits.Add(soldier);
        }
        else if (team == 2)
        {
            redTeamUnits.Add(soldier);
        }
    }
    
   

    private void NextUnit(int player)
    {
        if (playerTurn == 1)
        {
            // add variable later based on team size
            selectedSoldierBlue = (selectedSoldierBlue + 1) % 1;
        }
        else
        {
            selectedSoldierRed = (selectedSoldierRed + 1) % 1;
        }
        
    }

    public void GameOver(int winningTeam)
    {
        if (winningTeam == 1)
        {
            GameOverScreen.SetActive(!GameOverScreen.activeSelf);
            TextManagerGameOver.GetComponent<GameOverUI>().SetWinner(1);
            
        }
        else if (winningTeam == 2)
        {
            GameOverScreen.SetActive(!GameOverScreen.activeSelf);
            TextManagerGameOver.GetComponent<GameOverUI>().SetWinner(2);
            
        }
    }
    
    public void EndTurn()
    {
        if (playerTurn == 1)
        {
            playerTurn = 2;
            redTeamUnits[0].SetActionUsed(false);
        }
        else
        {
            playerTurn = 1;
            blueTeamUnits[0].SetActionUsed(false);
        }
    }
    
    public void playTurn(int player, List<Soldier> soldiers, int selectedSoldier)
    {
        
        if (soldiers[selectedSoldier].GetAimingMode() == false)
        {
            // initialisation des parametres a chaque nouveau tour
            TextManager.GetComponent<textManager>().setMovingMode(true);
            TextManager.GetComponent<textManager>().setMovesLeft(soldiers[selectedSoldier].getMouvement());
                
            if (Input.GetKey(KeyCode.RightArrow))
            {
                soldiers[selectedSoldier].MoveRight();
                TextManager.GetComponent<textManager>().setMovesLeft(soldiers[selectedSoldier].getMouvement());
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                soldiers[selectedSoldier].MoveLeft();
                TextManager.GetComponent<textManager>().setMovesLeft(soldiers[selectedSoldier].getMouvement());
            }
            else if (Input.GetKey(KeyCode.Z)) 
            {
                if (soldiers[selectedSoldier].GetAimingMode() == false)
                {
                    soldiers[selectedSoldier].Aim();
                }
            }
        }
        else if (soldiers[selectedSoldier].GetAimingMode() == true)
        {
            TextManager.GetComponent<textManager>().setMovingMode(false);
            if (Input.GetKey(KeyCode.UpArrow))
            {
                soldiers[selectedSoldier].AimHigher();
                soldiers[selectedSoldier].AimHigher();
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                soldiers[selectedSoldier].AimLower();
            }
                
            
            if (Input.GetKey(KeyCode.Space))
            {
                // set varying force later
                soldiers[selectedSoldier].Shoot();
                EndTurn();
            }
        }
        
        
        /*
        if (soldiers[selectedSoldier].GetAimingMode() == false)
        {
            if (Input.GetKey(GameManager.GM.MoveRight))
            {
                soldiers[selectedSoldier].MoveRight();
            }
            else if (Input.GetKey(GameManager.GM.MoveLeft))
            {
                soldiers[selectedSoldier].MoveLeft();
            }
            else if (Input.GetKey(GameManager.GM.EnterAimingMode)) 
            {
                if (soldiers[selectedSoldier].GetAimingMode() == false)
                {
                    soldiers[selectedSoldier].Aim();
                }
            }
        }
        else if (soldiers[selectedSoldier].GetAimingMode())
        {
            if (Input.GetKey(GameManager.GM.AimHigher))
            {
                soldiers[selectedSoldier].AimHigher();
            }
            else if (Input.GetKey(GameManager.GM.AimLower))
            {
                soldiers[selectedSoldier].AimLower();
            }
                
            
            if (Input.GetKey(GameManager.GM.Shoot))
            {
                // set varying force later
                soldiers[selectedSoldier].Shoot();
                soldiers[selectedSoldier].SetAimingMode(false);
                //NextUnit(player);
                soldiers[selectedSoldier].ResetMouvement();
                EndTurn();
            }
        }
        */
    }


    public void playTurnCPU(int player, List<Soldier> soldiers, int selectedSoldier, int difficulty)
    {
        if (soldiers[selectedSoldier].GetAimingMode() == false)
        {
            // initialisation des parametre a chaque nouveau tour
            TextManager.GetComponent<textManager>().setMovingMode(true);
            TextManager.GetComponent<textManager>().setMovesLeft(soldiers[selectedSoldier].getMouvement());
        }

        /*Vector3*/ distance = soldiers[selectedSoldier].transform.position - blueTeamUnits[0].transform.position;
        Vector3 position = soldiers[selectedSoldier].transform.position;
        if (difficulty == 1)                        //easy
        {
            if (distance.x > 20.0f)
            {
                soldiers[selectedSoldier].MoveLeft();
            }
            
            if (distance.x < 15.0f && position.x < -33)
            {
                soldiers[selectedSoldier].MoveRight();
            }

            if (soldiers[selectedSoldier].getMouvement() <= 0 || (distance.x <= 20.0f && distance.x >= 15.0f) || position.x >= -33)
            {
                soldiers[selectedSoldier].Aim();
            }

            if (soldiers[selectedSoldier].GetAimingMode() == true)
            {
                soldiers[selectedSoldier].Shoot();
                EndTurn();
            }
        }
        else if (difficulty == 2)                   //hard
        {
            
        }
    }

    public int GetPlayerTurn()
    {
        return playerTurn;
    }
    
    public void SetPlayerTurn(int player)
    {
        playerTurn = player;
    }
}
