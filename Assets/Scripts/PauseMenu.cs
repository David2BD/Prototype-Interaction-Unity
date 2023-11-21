using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetKeyDown(GameManager.Instance.generalActions[InputManager.GeneralAction.Quit]))
        {
            SceneManager.LoadScene("Main Menu");
            Time.timeScale = 1f;
        }
    }
}
