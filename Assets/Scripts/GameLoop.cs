using System.Collections;
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
    
    // Start is called before the first frame update
    void Start()
    {
        playerTurn = 1;
        selectedSoldierBlue = 0;
        
        RegisterSoldier(new Soldier(), 1);
        RegisterSoldier(new Soldier(), 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTurn == 1)
        {
            playTurn(1, blueTeamUnits, selectedSoldierBlue);
        }
        else if (playerTurn == 2)
        {
            playTurn(2, redTeamUnits, selectedSoldierRed);
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

    private void nextTurn(int player)
    {
        playerTurn = (playerTurn + 1) % 2;

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

    public void playTurn(int player, List<Soldier> soldiers, int selectedSoldier)
    {
        if (soldiers[selectedSoldier].GetAimingMode() == false)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                soldiers[selectedSoldier].MoveRight();
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                soldiers[selectedSoldier].MoveLeft();
            }
            else if (Input.GetKey(KeyCode.Space)) 
            {
                soldiers[selectedSoldier].Aim();
            }
        }
        else if (soldiers[selectedSoldier].GetAimingMode() == true)
        {
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
                nextTurn(player);
            }
        }
    }
}
