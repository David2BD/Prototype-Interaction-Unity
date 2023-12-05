using TMPro;
using UnityEngine;

namespace MenuScripts
{
    public class NewBehaviourScript : MonoBehaviour
    {
    
        public GameObject mainMenu;
        public GameObject welcomeScreen;

        public TextMeshProUGUI pressText;
    
        // Update is called once per frame
        public void Update()
        {
            pressText.SetText("Press " + GameManager.Instance.GeneralActions[GeneralAction.Confirm].ToString());
        
            if (Input.GetKeyDown(GameManager.Instance.GeneralActions[GeneralAction.Confirm]))
            {
                mainMenu.SetActive(!mainMenu.activeSelf);
                welcomeScreen.SetActive(false);
            }
        
        }
    }
}
