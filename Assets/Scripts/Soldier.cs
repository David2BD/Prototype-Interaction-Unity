using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    private GameLoop gameLoop;
    private Renderer soldierRenderer;
    public int team;
    private float mouvement  = 3.0f;                //Pour avoir la longueur du mouvement qui reste pour le tour
    private bool AimingMode = false;
    private bool ActionUsed = false;
    private float moveSpeed = 2.0f;
    
    // Start is called before the first frame update
    void Start()
    {
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

    public void MoveRight()
    {
        if (mouvement > 0)                                               //On sassure quil reste du mouvement au soldat
        {
            Vector3 currentPosition = transform.position;
            Vector3 newPosition = currentPosition + new Vector3(moveSpeed * Time.deltaTime, 0.0f, 0.0f);
            RemoveMouvement(newPosition.x - currentPosition.x);                //Retire le mouvement fait
            transform.position = newPosition;
        }
    }
    
    public void MoveLeft()
    {
        if (mouvement > 0)                                               //On sassure quil reste du mouvement au soldat
        {
            Vector3 currentPosition = transform.position;
            Vector3 newPosition = currentPosition + new Vector3(-moveSpeed * Time.deltaTime, 0.0f, 0.0f);
            RemoveMouvement(newPosition.x - currentPosition.x);                //Retire le mouvement fait
            transform.position = newPosition;
        }
    }

    public void AimHigher()
    {
        Debug.Log("Player aim goes up");
    }
    
    public void AimLower()
    {
        Debug.Log("Player aim goes down");
    }

    public void Aim()
    {
        AimingMode = true;
        Debug.Log("Player start aiming");
    }
    
    private void RemoveMouvement(float mouvementToRemove)              //on call ca avec le deplacement du frame pour ajuster le mouvement qu<il reste au soldat
    {
        mouvement -= Math.Abs(mouvementToRemove);
    }

    public bool GetActionUsed()
    {
        return ActionUsed;
    }

    public void SetActionUsed()                                       //Pas besoin de parametre on fait juste caller ca quand le player a utilise son action
    {
        ActionUsed = true;
    }

    public bool GetAimingMode()
    {
        return AimingMode;
    }

    public void SetAimingMode(bool mode)
    {
        AimingMode = mode;
    }
}
