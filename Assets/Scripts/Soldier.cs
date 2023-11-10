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
    private float health;

    public GameObject ballPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        soldierRenderer = GetComponent<Renderer>();
        SetTeamMaterial();
        gameLoop = FindObjectOfType<GameLoop>();
        gameLoop.RegisterSoldier(this, team);
        health = 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
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

    public void Shoot()
    {
        GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        
        // set initial velocity
        float angle = Vector3.Angle(transform.eulerAngles, transform.forward);
        Vector3 initialVelocity = new Vector3(20 * Mathf.Cos(angle), 30 * Mathf.Sin(angle), 0);
        rb.velocity = initialVelocity;
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

    public float getHealth()
    {
        return health;
    }

    public void removeHealth(float damage)
    {
        health -= damage;
    }
}
