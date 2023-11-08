using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    private GameLoop gameLoop;
    private Renderer soldierRenderer;
    private int team;
    
    // Start is called before the first frame update
    void Start()
    {
        team = 1;                                       //va falloir setter ca en creant les soldiers mais sont tous dans blue team pour linstant
        soldierRenderer = GetComponent<Renderer>();
        SetTeamMaterial();
        gameLoop = FindObjectOfType<GameLoop>();
        gameLoop.RegisterSoldier(this, team);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void SetTeamMaterial()
    {
        if (team == 1)
        {
            soldierRenderer.material.color = Color.blue;
        }
        else if (team == 2)
        {
            soldierRenderer.material.color = Color.red;
        }
    }
}
