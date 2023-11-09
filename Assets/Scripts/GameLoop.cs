using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    public int playerTurn;
    public int selectedSoldier;
    public List<Soldier> blueTeamUnits = new List<Soldier>();
    public List<Soldier> redTeamUnits = new List<Soldier>();
    
    // Start is called before the first frame update
    void Start()
    {
        playerTurn = 1;
        selectedSoldier = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTurn == 1)
        {
            if (blueTeamUnits[selectedSoldier].GetAimingMode() == false)
            {
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    blueTeamUnits[selectedSoldier].MoveRight();
                }
                else if (Input.GetKey(KeyCode.LeftArrow))
                {
                    blueTeamUnits[selectedSoldier].MoveLeft();
                }
                else if (Input.GetKey(KeyCode.Space)) 
                {
                    blueTeamUnits[selectedSoldier].Aim();
                }
            }
            else if (blueTeamUnits[selectedSoldier].GetAimingMode() == true)
            {
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    blueTeamUnits[selectedSoldier].AimHigher();
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    blueTeamUnits[selectedSoldier].AimLower();
                }
            }
        }
        else if (playerTurn == 2)
        {
            
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
    
    public int GetplayerTurn()
    {
        return playerTurn;
    }
    
    public void SetplayerTurn(int player)
    {
        playerTurn = player;
    }
    
}
