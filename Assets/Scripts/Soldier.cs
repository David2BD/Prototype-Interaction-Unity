
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
    public int health;
    public HPBar barScript;
    
    //For aiming
    public Vector3 AimingAngle;
    private LineRenderer lineRenderer;
    private float rotationSpeed = 5f;

    public GameObject ballPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        AimingAngle = new Vector3(0.5f, 0.5f, 0);
        AimingAngle.Normalize();
        soldierRenderer = GetComponent<Renderer>();
        SetTeamMaterial();
        gameLoop = FindObjectOfType<GameLoop>();
        gameLoop.RegisterSoldier(this, team);
        health = 100;
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        
        //lineRenderer.SetVertexCount(1);
        Vector3[] positions = new Vector3[3];
        positions[0] = transform.position + new Vector3(0.0f, 0.0f, 0.0f);
        positions[1] =  positions[0] + (AimingAngle);
        lineRenderer.SetPositions(positions);
        //lineRenderer.(transform.position,  AimingAngle * 100, Color.blue);
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
        if( AimingAngle.x > 0.25f)
        {
            float rotationAmount = rotationSpeed * Time.deltaTime;
            AimingAngle.Set(AimingAngle.x, AimingAngle.y + rotationAmount, AimingAngle.z);
            AimingAngle.Normalize();
        }
        else
        {
            AimingAngle.Set(0.251f, AimingAngle.y, AimingAngle.z);
        }
    }
    
    public void AimLower()
    {
        if( AimingAngle.x > 0.25f)
        {
            float rotationAmount = rotationSpeed * Time.deltaTime;
            AimingAngle.Set(AimingAngle.x, AimingAngle.y - rotationAmount, AimingAngle.z);
            AimingAngle.Normalize();
        }
        else
        {
            AimingAngle.Set(0.251f, AimingAngle.y, AimingAngle.z);
        }
    }

    public void Shoot()
    {
        GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        
        // set initial velocity
        float angle = Vector3.Angle(transform.eulerAngles, transform.forward);
        Vector3 initialVelocity = AimingAngle * 20;
        //Vector3 initialVelocity = new Vector3(20 * Mathf.Cos(angle), 30 * Mathf.Sin(angle), 0);
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

    public void removeHealth(int damage)
    {
        
        health -= damage;
        barScript.Change(-damage);
    }
}
