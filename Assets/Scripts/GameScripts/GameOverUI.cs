using AudioScript;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameScripts
{
    public class GameOverUI : MonoBehaviour
    {
        public TextMeshProUGUI winnerText;
        public GameObject jingles_sounds;

        public void Update()
        {
            if (Input.GetKey(GameManager.Instance.GeneralActions[GeneralAction.Quit]))
            {
                jingles_sounds.GetComponent<JinglesController>().stopSound();
                SceneManager.LoadScene("Main Menu");
            
            }
        }

        public void SetWinner(int winningTeam)
        {
            jingles_sounds.GetComponent<JinglesController>().stopSound();
            jingles_sounds.GetComponent<JinglesController>().playWin();
            if (winningTeam == 1)
            {
                winnerText.SetText(GameManager.Instance.GetName(1).ToString() + " win" +
                                   "\nPress " + GameManager.Instance.GeneralActions[GeneralAction.Quit].ToString() + " to go back.");
            }
            else if (winningTeam == 2)
            {
                winnerText.SetText(GameManager.Instance.GetName(2).ToString() + " win " + 
                                   "\nPress " + GameManager.Instance.GeneralActions[GeneralAction.Quit].ToString() + " to go back.");
            }
        }
    }
}
