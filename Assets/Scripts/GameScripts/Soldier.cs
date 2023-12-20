using System;
using AudioScript;
using UnityEngine;

namespace GameScripts
{
    public class Soldier : MonoBehaviour
    {
        private GameLoop gameLoop;
        private Renderer soldierRenderer;
        public int team;
        private float mouvement  = 5.0f;                //Pour avoir la longueur du mouvement qui reste pour le tour
        private bool AimingMode = false;
        private bool ActionUsed = false;
        private float moveSpeed = 2.0f;
        public int health;
        public HPBar barScript;
        
        private Animator animator;
        public GameObject soldier_demo;
        public GameObject soldier_toon;
        private bool moving;
        
        private bool usedTP;
    
        //private Slider powerUp;
        //private bool powerUpRunning = false;
    
        //For aiming
        public Vector3 AimingAngle;
        private LineRenderer lineRenderer;
        private float rotationSpeed = 3f;

        public GameObject ballPrefab;

        public GameObject SFX_sounds;
        public GameObject instruments_sounds;

        private bool lowHealth = false;
        
        private GameObject model1;
        private GameObject model2;

        public Material demoWeaponMat;
        public Material demoBodyMat;
        public Material toonSoldierBodyMat;
        
        public GameObject vikingHat;
        public GameObject magicianHat;
        public GameObject minerHat;

        public GameObject jetPackParticles;
        private bool jetPackOn = false;

        public ParticlePoolingManager ParticlePool;
    
        // Start is called before the first frame update
        void Start()
        {
            SetupCustomization();
            
            AimingAngle = (team == 1) ? new Vector3(0.5f, 0.5f, 0) : new Vector3(-0.5f, 0.5f, 0);
            AimingAngle.Normalize();
            soldierRenderer = GetComponent<Renderer>();
            //SetTeamMaterial();
            gameLoop = FindObjectOfType<GameLoop>();
            gameLoop.RegisterSoldier(this, team);
            health = 100;
            lineRenderer = GetComponent<LineRenderer>();
            
            if (CustomizeMenu.model == 0)
            {
                animator = soldier_demo.GetComponent<Animator>();
            }
            else if (CustomizeMenu.model == 1)
            {
                animator = soldier_toon.GetComponent<Animator>();
            }
            

            //soldier.GetComponent<>
            usedTP = false;
            
        }

    
    
        // Update is called once per frame
        void Update()
        {
            if (health <= 0)
            {
                Destroy(gameObject);
            }
            
            
            Vector3[] positions = new Vector3[3];
            positions[0] = transform.position + new Vector3(0.0f, 0.6f, 0.0f);
            positions[1] =  positions[0] + (AimingAngle);
            lineRenderer.SetPositions(positions);
            
        }

        public bool getJetPackStatus()
        {
            return jetPackOn;
        }
        
        public void setJetPackStatus(bool status)
        {
            jetPackOn = status;
        }
        
        public void TurnOnJetPack()
        {
           jetPackParticles.SetActive(!jetPackParticles.activeSelf);
            jetPackOn = true;
        }
        public void TurnOffJetPack()
        {
            jetPackParticles.SetActive(false);
            jetPackOn = false;
        }
        
        private void SetupCustomization()
        {
            if (CustomizeMenu.model == 0)
            {
                soldier_demo.SetActive(!soldier_demo.activeSelf);
                soldier_toon.SetActive(false);
                demoWeaponMat.color = CustomizeMenu.colorWeapon;
                demoBodyMat.color = CustomizeMenu.colorBody;
                
                if (CustomizeMenu.currentHat == 0)
                {
                    magicianHat.SetActive(false);
                    minerHat.SetActive(false);
                    vikingHat.SetActive(false);
                }
                else if (CustomizeMenu.currentHat == 1) 
                {
                    magicianHat.SetActive(false);
                    minerHat.SetActive(false);
                    vikingHat.SetActive(!vikingHat.activeSelf);
                }
                else if (CustomizeMenu.currentHat == 2)
                {
                    magicianHat.SetActive(false);
                    vikingHat.SetActive(false);
                    minerHat.SetActive(!minerHat.activeSelf);
                }
                else if (CustomizeMenu.currentHat == 3)
                {
                    minerHat.SetActive(false);
                    vikingHat.SetActive(false);
                    magicianHat.SetActive(!magicianHat.activeSelf);
                }
                
            }
            if (CustomizeMenu.model == 1)
            {
                soldier_demo.SetActive(false);
                soldier_toon.SetActive(!soldier_toon.activeSelf);
                toonSoldierBodyMat.color = CustomizeMenu.colorBody;
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

        public void setTeam(int t)
        {
            team = t;
        }

        public int getTeam()
        {
            return team;
        }
        
        public void MoveRight()
        {
            
            if (mouvement > 0)                                               //On sassure quil reste du mouvement au soldat
            {
                SFX_sounds.GetComponent<playerSFX>().playMovement();
                animator.SetBool("Moving", true);
                Vector3 currentPosition = transform.position;
                Vector3 newPosition = currentPosition + new Vector3(moveSpeed * Time.deltaTime, 0.0f, 0.0f);
                RemoveMouvement(newPosition.x - currentPosition.x);                //Retire le mouvement fait
                transform.position = newPosition;
            }
            else
            {
                SFX_sounds.GetComponent<playerSFX>().stopSound();
            }
            
        }
    
        public void MoveLeft()
        {
            if (mouvement > 0)                                               //On sassure quil reste du mouvement au soldat
            {
                SFX_sounds.GetComponent<playerSFX>().playMovement();
                animator.SetBool("Moving", true);
                Vector3 currentPosition = transform.position;
                Vector3 newPosition = currentPosition + new Vector3(-moveSpeed * Time.deltaTime, 0.0f, 0.0f);
                RemoveMouvement(newPosition.x - currentPosition.x);                //Retire le mouvement fait
                transform.position = newPosition;
            }
            else
            {
                SFX_sounds.GetComponent<playerSFX>().stopSound();
            }
        }

        public void StopMoving()
        {
            animator.SetBool("Moving", false);
        }
        
        public void AimHigher()
        {
            if( AimingAngle.x > 0.25f || AimingAngle.x < -0.25f)
            {
                float rotationAmount = rotationSpeed * Time.deltaTime;
                AimingAngle.Set(AimingAngle.x, AimingAngle.y + rotationAmount, AimingAngle.z);
                AimingAngle.Normalize();
            }
            else
            {
                Vector3 angle_team = (team == 1) ? new Vector3(0.251f, AimingAngle.y, AimingAngle.z) :
                    new Vector3(-0.251f, AimingAngle.y, AimingAngle.z);
                AimingAngle.Set(angle_team.x, angle_team.y, angle_team.z);
            }
        }
    
    
    
        public void AimLower()
        {
            if( AimingAngle.x > 0.25f || AimingAngle.x < -0.25f)
            {
                float rotationAmount = rotationSpeed * Time.deltaTime;
                AimingAngle.Set(AimingAngle.x, AimingAngle.y - rotationAmount, AimingAngle.z);
                AimingAngle.Normalize();
            }
            else
            {
                Vector3 angle_team = (team == 1) ? new Vector3(0.251f, AimingAngle.y, AimingAngle.z) :
                    new Vector3(-0.251f, AimingAngle.y, AimingAngle.z);
                AimingAngle.Set(angle_team.x, angle_team.y, angle_team.z);
            }
        }
    
    
  

        public void Shoot()
        {
            if (ActionUsed == false) 
            {
                SFX_sounds.GetComponent<playerSFX>().playShoot();
                Vector3 bulletPos = (team == 1)
                    ? new Vector3(transform.position.x + 1, transform.position.y + 0.8f, transform.position.z)
                    : new Vector3(transform.position.x - 1, transform.position.y + 0.8f, transform.position.z);
                GameObject ball = Instantiate(ballPrefab, bulletPos, Quaternion.identity);
                Rigidbody rb = ball.GetComponent<Rigidbody>();

                // set initial velocity
                float angle = Vector3.Angle(transform.eulerAngles, transform.forward);
                Vector3 initialVelocity = AimingAngle * 20;
                //Vector3 initialVelocity = new Vector3(20 * Mathf.Cos(angle), 30 * Mathf.Sin(angle), 0);
                rb.velocity = initialVelocity;
                ActionUsed = true;
                SetAimingMode(false);
                ResetMouvement();
                usedTP = false;
            }
        }
        
        public void Aim()
        {
            instruments_sounds.GetComponent<InstrumentController>().PlayAll(true);
            
            SFX_sounds.GetComponent<playerSFX>().playArming();
            SFX_sounds.GetComponent<playerSFX>().playVoiceAttack();
            AimingMode = true;
        
            Debug.Log("Player start aiming");
        }
    
        private void RemoveMouvement(float mouvementToRemove)              //on call ca avec le deplacement du frame pour ajuster le mouvement qu<il reste au soldat
        {
            mouvement -= Math.Abs(mouvementToRemove);
        }

        public void ResetMouvement()
        {
            mouvement = 5.0f;
        }
    
        public bool GetActionUsed()
        {
            return ActionUsed;
        }

        public void SetActionUsed(bool setting)                                       //Pas besoin de parametre on fait juste caller ca quand le player a utilise son action
        {
            ActionUsed = setting;
        }

        public bool GetAimingMode()
        {
            return AimingMode;
        }

        public void SetAimingMode(bool mode)
        {
            AimingMode = mode;
        }

        public bool GetUsedTP()
        {
            return usedTP;
        }

        public void SetUsedTP(bool mode)
        {
            usedTP = mode;
        }
    
        public float getMouvement()
        {
            return mouvement;
        }

        public void Jump()
        {
            SFX_sounds.GetComponent<playerSFX>().playJump();
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.AddForce(0, 10, 0, ForceMode.Impulse);
        }

        public float getHealth()
        {
            return health;
        }

        public void stopSound()
        {
            SFX_sounds.GetComponent<playerSFX>().stopSound();
        }

        public bool getLowHealth()
        {
            return lowHealth;
        }

        public void removeHealth(int damage)
        {
            SFX_sounds.GetComponent<playerSFX>().playVoiceHit();
            health -= damage;
            barScript.Change(-damage);
            
            GameManager.Instance.updateMusicLevel();
            instruments_sounds.GetComponent<InstrumentController>().PlayAll(false);

            if (health <= 40)
            {
                lowHealth = true;
                SFX_sounds.GetComponent<playerSFX>().playVoiceLowHealth();
            }
            
            if (health <= 0)
            {
                if (team == 1)
                {
                    gameLoop.GameOver(2);
                }
                else if (team == 2)
                {
                    gameLoop.GameOver(1);
                }
            }
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                TurnOffJetPack();
            }
        }
    
    
    }
}
