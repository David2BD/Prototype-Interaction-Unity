using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewBehaviourScript : MonoBehaviour
{
    
    public GameObject mainMenu;
    public GameObject welcomeScreen;

    public TextMeshProUGUI pressText;
    
    // Update is called once per frame
    public void Update()
    {
        pressText.SetText("Press " + GameManager.Instance.generalActions[GeneralAction.Confirm].ToString());
        
        if (Input.GetKeyDown(GameManager.Instance.generalActions[GeneralAction.Confirm]))
        {
            mainMenu.SetActive(!mainMenu.activeSelf);
            welcomeScreen.SetActive(false);
        }
        
    }
}
