using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Update is called once per frame
    public void Update()
    {
        if (Input.GetKeyDown(GameManager.Instance.generalActions[GeneralAction.Quit]))
        {
            SceneManager.LoadScene("Main Menu");
            Time.timeScale = 1f;
        }
    }
}
