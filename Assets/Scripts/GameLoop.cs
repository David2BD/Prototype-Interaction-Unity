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
            if (Input.GetKey(KeyCode.RightArrow))  //Il faut ajouter la duree du frame dans lequation !!!!
            {
                Vector3 currentPosition = blueTeamUnits[selectedSoldier].transform.position;
                Vector3 newPosition = currentPosition + new Vector3(0.01f, 0.0f, 0.0f);
                
                blueTeamUnits[selectedSoldier].transform.position = newPosition;
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
