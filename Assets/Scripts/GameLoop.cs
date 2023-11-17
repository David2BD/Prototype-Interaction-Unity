
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

    public GameObject TextManager;

    //private Dictionary<InputManager.PlayerAction, KeyCode> currentPlayer;
    
    // Start is called before the first frame update
    void Start()
    {
        playerTurn = 1;
        selectedSoldierBlue = 0;
        //currentPlayer = InputManager.getPlayerBlue();
        
        RegisterSoldier(new Soldier(), 1);
        RegisterSoldier(new Soldier(), 2);
        
        TextManager.GetComponent<textManager>().setTeam(1);
    }

    // Update is called once per frame
    void Update()
    {
        // game ends, fonctionne pas
        if (blueTeamUnits.Count == 0 || redTeamUnits.Count == 0)
        {
            playerTurn = 1;
            selectedSoldierBlue = 0;
            //currentPlayer = InputManager.getPlayerBlue();
        
            RegisterSoldier(new Soldier(), 1);
            RegisterSoldier(new Soldier(), 2);
        }
        
        if (playerTurn == 1)
        {
            playTurn(1, blueTeamUnits, selectedSoldierBlue);
            TextManager.GetComponent<textManager>().setTeam(1);
        }
        else if (playerTurn == 2)
        {
            playTurn(2, redTeamUnits, selectedSoldierRed);
            TextManager.GetComponent<textManager>().setTeam(2);
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
    
    public int GetPlayerTurn()
    {
        return playerTurn;
    }
    
    public void SetPlayerTurn(int player)
    {
        playerTurn = player;
    }

    private void NextUnit(int player)
    {
        //playerTurn = (playerTurn + 1) % 2;

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

    public void EndTurn()
    {
        if (playerTurn == 1)
        {
            playerTurn = 2;
        }
        else
        {
            playerTurn = 1;
        }
    }
    
    public void playTurn(int player, List<Soldier> soldiers, int selectedSoldier)
    {
        
        if (soldiers[selectedSoldier].GetAimingMode() == false)
        {
            // initialisation des parametre a chaque nouveau tour
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
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                soldiers[selectedSoldier].AimLower();
            }
                
            
            if (Input.GetKey(KeyCode.Space))
            {
                // set varying force later
                soldiers[selectedSoldier].Shoot();
                soldiers[selectedSoldier].SetAimingMode(false);
                //NextUnit(player);
                soldiers[selectedSoldier].ResetMouvement();
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
    
}
