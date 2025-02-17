using UnityEngine;
using UnityEngine.SceneManagement;

namespace MenuScripts
{
    public class PauseMenu : MonoBehaviour
    {
        // Update is called once per frame
        public void Update()
        {
            if (Input.GetKeyDown(GameManager.Instance.GeneralActions[GeneralAction.Quit]))
            {
                SceneManager.LoadScene("Main Menu");
                Time.timeScale = 1f;
            }
        }
    }
}
