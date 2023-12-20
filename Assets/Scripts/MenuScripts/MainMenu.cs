using GameScripts;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MenuScripts
{
    public class MainMenu : MonoBehaviour
    {
        public GameObject optionsMenu;
        public GameObject mainMenu;
        public GameObject audioMenu;
        public GameObject customizeMenu;

        private int _count;
        
        public TextMeshProUGUI player2Button;
        public TextMeshProUGUI current;

        public TextMeshProUGUI player1Name; 
        public TextMeshProUGUI player2Name;
        
        public GameObject playButton;
        public GameObject optionButton;
        public GameObject audioButton;

        public Canvas canvas;

        void Start()
        {
            GameSettings.isPlayer2CPU = false;
            GameSettings.CPUDifficulty = 0;

            if (GameManager.Instance != null)
            {
                current.SetText("Human");
                player2Button.SetText("Change : CPU (easy)");
            }
        }

        private void Update()
        {
            PlayButtonAnimation();
            ButtonsHorizontalSlide();
            if (player1Name != null && player2Name != null)
            {
                player1Name.SetText(GameManager.Instance.GetName(1));
                player2Name.SetText(GameManager.Instance.GetName(2));
            }
        }

        private void PlayButtonAnimation()
        {
            if (playButton.transform.position.y > Screen.height / 2)
            {
                playButton.transform.position = new Vector3(playButton.transform.position.x,
                    playButton.transform.position.y-2, playButton.transform.position.z);
            }
        }

        private void ButtonsHorizontalSlide()
        {
            if (optionButton.transform.position.x < Screen.width / 2)
            {
                optionButton.transform.position = new Vector3(optionButton.transform.position.x + 2,
                    optionButton.transform.position.y, optionButton.transform.position.z);
            }
            if (audioButton.transform.position.x < Screen.width / 2)
            {
                audioButton.transform.position = new Vector3(audioButton.transform.position.x + 2,
                    audioButton.transform.position.y, audioButton.transform.position.z);
            }
        }
        
        public void Play()
        {
            SceneManager.LoadScene("LoadingScreen");
        }

        public void Options()
        {
            optionsMenu.SetActive(!optionsMenu.activeSelf);
            mainMenu.SetActive(false);
            Debug.Log("Player enter game settings");
        }

        public void Audio()
        {
            audioMenu.SetActive(!audioMenu.activeSelf);
            mainMenu.SetActive(false);
        }
        
        public void CustomizeMenu()
        {
            customizeMenu.SetActive(!customizeMenu.activeSelf);
            mainMenu.SetActive(false);
        }
        
        public void ChangePlayer2()
        {
            _count = (_count + 1) % 2;
        
            // le texte du bouton est le niveau/adversaire
            if (_count == 1)
            {
                current.SetText("CPU (easy)");
                player2Button.SetText("Change : Human");
                GameSettings.isPlayer2CPU = true;
                GameSettings.CPUDifficulty = 1;
            }
            else if (_count == 0)
            {
                current.SetText("Human");
                player2Button.SetText("Change : CPU (easy)");
                GameSettings.isPlayer2CPU = false;
                GameSettings.CPUDifficulty = 0;
            }
        }
    
        public void Quit()
        {
            Application.Quit();
            Debug.Log("Player quit game");
        }
    }
}
