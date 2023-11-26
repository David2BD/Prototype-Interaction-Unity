using UnityEngine;
using TMPro;

public class Options : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject controllerMenu;
    public GameObject optionMenu;

    private int _currentPlayer = 1;
    public TextMeshProUGUI textOptions;
    
    public TMP_InputField player1;
    public TMP_InputField player2;

    public void Update()
    {
        player1.text = GameManager.Instance.getName(1);
        player2.text = GameManager.Instance.getName(2);
        
        switch (_currentPlayer)
        {
            case 1:
                textOptions.SetText(GameManager.Instance.getName(1) + " updating controls.");
                break;
            case 2:
                textOptions.SetText(GameManager.Instance.getName(2) + " updating controls.");
                break;
        }
    }

    public void SetControls()
    {
        controllerMenu.SetActive(!controllerMenu.activeSelf);
        optionMenu.SetActive(false);
        Debug.Log("Player enter settings menu");
    }

    public void ChangeName(int player)
    {
        if (player == 1 && player1.text != null)
        {
            GameManager.Instance.setName(1, player1.text);
        }
        else if (player == 2 && player2.text != null)
        {
            GameManager.Instance.setName(2, player2.text);
        }
    }

    public void UpdateControlQwerty()
    {
        controllerMenu.GetComponent<InputManager>().SetQwerty(_currentPlayer);
    }
    
    public void UpdateControlArrow()
    {
        controllerMenu.GetComponent<InputManager>().SetArrows(_currentPlayer);
    }

    public void ChangePlayer()
    {
        _currentPlayer = (_currentPlayer == 1) ? 2 : 1; 
    }

    public void SaveControls()
    {
        GameManager.Instance.saveAllControls();
    }

    public void LoadControls()
    {
        GameManager.Instance.loadAllControls();
    }
    
    public void Return()
    {
        mainMenu.SetActive(!mainMenu.activeSelf);
        optionMenu.SetActive(false);
        Debug.Log("Player go back to game menu");
    }
}
