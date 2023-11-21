using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewBehaviourScript : MonoBehaviour
{
    
    public GameObject mainMenu;
    public GameObject welcomeScreen;
    public GameObject controls;

    public TextMeshProUGUI pressText;
    
    // Update is called once per frame
    public void Update()
    {
        if (GameManager.Instance.generalActions == null)
        {
            // initialize control
            controls.GetComponent<InputManager>().setControls();
        }

        pressText.SetText("Press \n" + GameManager.Instance.generalActions[InputManager.GeneralAction.Confirm].ToString());
        
        if (Input.GetKeyDown(GameManager.Instance.generalActions[InputManager.GeneralAction.Confirm]))
        {
            mainMenu.SetActive(!mainMenu.activeSelf);
            welcomeScreen.SetActive(false);
            
        }
        
    }
}
