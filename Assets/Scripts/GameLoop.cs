using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    public int playerTurn;
    public int selectedSoldierBlue = 1;
    public int selectedSoldierRed = 1;
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
    private int aimCounter;

    

    public Vector3 distance;                        //For debugging purposes

    //private Dictionary<InputManager.PlayerAction, KeyCode> currentPlayer;
    
 
    void Start()
    {
        playerTurn = 1;
        //selectedSoldierBlue = 1;
        
        RegisterSoldier(new Soldier(), 1);
        RegisterSoldier(new Soldier(), 2);
        
        TextManager.GetComponent<textManager>().setTeam(1);

        isPlayer2CPU = GameSettings.isPlayer2CPU;
        CPUDiff = GameSettings.CPUDifficulty;
        aimCounter = -1;

    }

    void Update()
    {
        activeBall = FindObjectOfType<Ball>();
        
        if (Input.GetKeyDown(GameManager.Instance.GeneralActions[GeneralAction.Pause]))
        {
            TogglePause();
        }
        
        if (activeBall == null)
        {
            if (playerTurn == 1)
            {
                if (blueTeamUnits[selectedSoldierBlue] != null)
                {
                    playTurn(1, blueTeamUnits, selectedSoldierBlue);
                }
                TextManager.GetComponent<textManager>().setTeam(1);
            }
            else if (playerTurn == 2 && isPlayer2CPU == false)
            {
                if (redTeamUnits[selectedSoldierRed] != null)
                {
                    playTurn(2, redTeamUnits, selectedSoldierRed);
                }
                TextManager.GetComponent<textManager>().setTeam(2);
            }
            else if (playerTurn == 2 && isPlayer2CPU == true)
            {
                if (redTeamUnits[selectedSoldierRed] != null)
                {
                    playTurnCPU(2, redTeamUnits, selectedSoldierRed, CPUDiff);
                }

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
            redTeamUnits[1].SetActionUsed(false);
        }
        else
        {
            playerTurn = 1;
            blueTeamUnits[1].SetActionUsed(false);
        }
    }
    
    public void playTurn(int player, List<Soldier> soldiers, int selectedSoldier)
    {
        if (soldiers[selectedSoldier].GetAimingMode() == false)
        {
            // initialisation des parametres a chaque nouveau tour
            TextManager.GetComponent<textManager>().setMovingMode(true);
            TextManager.GetComponent<textManager>().setMovesLeft(soldiers[selectedSoldier].getMouvement());
                
            if (Input.GetKey(GameManager.Instance.GetPlayerKeys(player)[PlayerAction.MoveRight]))
            {
                soldiers[selectedSoldier].MoveRight();
                TextManager.GetComponent<textManager>().setMovesLeft(soldiers[selectedSoldier].getMouvement());
            }
            else if (Input.GetKey(GameManager.Instance.GetPlayerKeys(player)[PlayerAction.MoveLeft]))
            {
                soldiers[selectedSoldier].MoveLeft();
                TextManager.GetComponent<textManager>().setMovesLeft(soldiers[selectedSoldier].getMouvement());
            }
            else if (Input.GetKey(GameManager.Instance.GetPlayerKeys(player)[PlayerAction.Jump]))
            {
                soldiers[selectedSoldier].Jump();
            }
            else if (Input.GetKey(GameManager.Instance.GetPlayerKeys(player)[PlayerAction.EnterAimingMode]))
            {
                if (soldiers[selectedSoldier].GetAimingMode() == false)
                {
                    soldiers[selectedSoldier].Aim();
                }
            }
        }
        else if (soldiers[selectedSoldier].GetAimingMode())
        {
            TextManager.GetComponent<textManager>().setMovingMode(false);
            if (Input.GetKey(GameManager.Instance.GetPlayerKeys(player)[PlayerAction.AimHigher]))
            {
                soldiers[selectedSoldier].AimHigher();
                soldiers[selectedSoldier].AimHigher();
            }
            else if (Input.GetKey(GameManager.Instance.GetPlayerKeys(player)[PlayerAction.AimLower]))
            {
                soldiers[selectedSoldier].AimLower();
            }
                
            
            if (Input.GetKey(GameManager.Instance.GetPlayerKeys(player)[PlayerAction.Shoot]))
            {
                // set varying force later
                soldiers[selectedSoldier].Shoot();
                EndTurn();
            }
        }
    }


    public void playTurnCPU(int player, List<Soldier> soldiers, int selectedSoldier, int difficulty)
    {
        if (soldiers[selectedSoldier].GetAimingMode() == false)
        {
            // initialisation des parametre a chaque nouveau tour
            TextManager.GetComponent<textManager>().setMovingMode(true);
            TextManager.GetComponent<textManager>().setMovesLeft(soldiers[selectedSoldier].getMouvement());
        }
        
        distance = soldiers[selectedSoldier].transform.position - blueTeamUnits[1].transform.position;
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
                
                if (aimCounter < 0)
                {
                    int aimDuration = Random.Range(300, 700); 
                    
                    aimCounter = aimDuration;
                }
                
                aimCounter--;

                
                if (aimCounter > 0)
                {
                    int aimDirection = Random.Range(1, 3); 
                    if (aimDirection == 1)
                    {
                        soldiers[selectedSoldier].AimHigher();
                    }
                    else if (aimDirection == 2)
                    {
                        soldiers[selectedSoldier].AimLower();
                    }
                }
                

                // If the counter reaches 0, fire or perform other actions
                if (aimCounter == 0)
                {
                    soldiers[selectedSoldier].Shoot();
                    aimCounter = -1;
                    EndTurn();
                }

                
            }
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
