using System.Collections;
using System.Collections.Generic;
using AudioScript;
using UnityEngine;

namespace GameScripts
{
    public class GameLoop : MonoBehaviour
    {
        public int playerTurn;
        public int selectedSoldierBlue = 0;
        public int selectedSoldierRed = 0;
        public List<Soldier> blueTeamUnits = new List<Soldier>();
        public List<Soldier> redTeamUnits = new List<Soldier>();
    
        public GameObject GameOverScreen;
        public GameObject TextManagerGameOver;

        public GameObject pauseMenu;
        public GameObject audioMenu;
    
        public GameObject GameUI;

        public GameObject TextManager;
    
        private Ball activeBall;

        private bool isPlayer2CPU;
        private int CPUDiff;
        private int aimCounter;
        
        public GameObject jingles_sounds;
        public GameObject instruments_sounds;
        
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
            
            GameManager.Instance.setCounterMusicLevel(0);
            
            // start music
            instruments_sounds.GetComponent<InstrumentController>().PlayAll(false);
        }

        void Update()
        {
            activeBall = FindObjectOfType<Ball>();
        
            if (Input.GetKeyDown(GameManager.Instance.GeneralActions[GeneralAction.Pause]))
            {
                TogglePause();
            }

            if (Input.GetKeyDown(GameManager.Instance.GeneralActions[GeneralAction.Audio]))
            {
                ToggleAudio();
            }
            
            if (GameManager.Instance.getBallMiss())
            {
                // play jingle ball miss
                jingles_sounds.GetComponent<JinglesController>().playBallMiss();
                GameManager.Instance.resetBallMiss();
            }
            
            if (GameManager.Instance.getBallHit())
            {
                // play jingle ball hit
                jingles_sounds.GetComponent<JinglesController>().playBallHit();
                GameManager.Instance.resetBallHit();
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
            bool aiming = (playerTurn == 1)
                ? blueTeamUnits[selectedSoldierBlue].GetAimingMode()
                : redTeamUnits[selectedSoldierRed].GetAimingMode();
            
            // Toggle the pause state
            if (Time.timeScale == 0f)
            {
                // Resume the game
                Time.timeScale = 1f;
                pauseMenu.SetActive(false);
                instruments_sounds.GetComponent<InstrumentController>().PlayPause(false);
                instruments_sounds.GetComponent<InstrumentController>().PlayAll(aiming);
            }
            else
            {
                instruments_sounds.GetComponent<InstrumentController>().PlayPause(true);
                
                // Pause the game
                Time.timeScale = 0f;
                pauseMenu.SetActive(true);
            }
        }
        
        void ToggleAudio()
        {
            // Toggle the pause state
            if (Time.timeScale == 0f)
            {
                // Resume the game
                Time.timeScale = 1f;
                audioMenu.SetActive(false);
            }
            else
            {
                // Pause the game
                Time.timeScale = 0f;
                audioMenu.SetActive(true);
            }
        }
    
        public void RegisterSoldier(Soldier soldier, int team)
        {
            if (team == 1)
            { 
                soldier.setTeam(1);
                blueTeamUnits.Add(soldier);
            }
            else if (team == 2)
            {
                soldier.setTeam(2);
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
            instruments_sounds.GetComponent<InstrumentController>().StopAll();
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
                // set to 1 for build
                redTeamUnits[0].SetActionUsed(false);
            }
            else
            {
                playerTurn = 1;
                blueTeamUnits[0].SetActionUsed(false);
            }
            instruments_sounds.GetComponent<InstrumentController>().PlayAll(false);
        }
    
        public void playTurn(int player, List<Soldier> soldiers, int selectedSoldier)
        {
            GameManager.Instance.setTurn(player);
            
            if (soldiers[selectedSoldier].getLowHealth())
            {
                jingles_sounds.GetComponent<JinglesController>().lowHealthState();
            }
            else
            {
                jingles_sounds.GetComponent<JinglesController>().stopSound();
            }
            
            if (soldiers[selectedSoldier].GetAimingMode() == false)
            {
                // initialisation des parametres a chaque nouveau tour
                TextManager.GetComponent<textManager>().setMovingMode(true);
                TextManager.GetComponent<textManager>().setMovesLeft(soldiers[selectedSoldier].getMouvement());
                if (Input.GetKeyUp(GameManager.Instance.GetPlayerKeys(player)[PlayerAction.MoveRight]))
                {
                    soldiers[selectedSoldier].StopMoving();
                }
                if (Input.GetKeyUp(GameManager.Instance.GetPlayerKeys(player)[PlayerAction.MoveLeft]))
                {
                    soldiers[selectedSoldier].StopMoving();
                }
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
                else
                {
                    soldiers[selectedSoldier].stopSound();
                }
            }
            else if (soldiers[selectedSoldier].GetAimingMode())
            {
                TextManager.GetComponent<textManager>().setMovingMode(false);
                if (Input.GetKey(GameManager.Instance.GetPlayerKeys(player)[PlayerAction.AimHigher]))
                {
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
        
            distance = soldiers[selectedSoldier].transform.position - blueTeamUnits[0].transform.position;
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
        
        IEnumerator playLowHealth()
        {

                while (true)
                {
                    jingles_sounds.GetComponent<JinglesController>().lowHealthState();
                    yield return new WaitForSeconds(1f);
                    
                    yield return new WaitForSeconds(6f);
                }
        }
    }
}
