using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    
    public GameObject mainMenu;
    public GameObject welcomeScreen;

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            mainMenu.SetActive(!mainMenu.activeSelf);
            welcomeScreen.SetActive(false);
            
        }
    }
}
