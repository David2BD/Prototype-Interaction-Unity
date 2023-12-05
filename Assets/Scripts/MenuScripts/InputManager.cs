using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MenuScripts
{
    public class InputManager : MonoBehaviour
    {
        public GameObject settingsMenu;
        public GameObject inputMenu;

        // parent for all controls
        public Transform player1;
        public Transform player2;
    
        // for updating player controls values
        private int _playerKey = 1;
        private int _childKey;
        private bool _gettingKey = true;
        private bool _waitingKey = false;
        private KeyCode _newKey = KeyCode.None;

        public TextMeshProUGUI keyUpdate;

        // for updating general controls values
        public TMP_InputField pause;
        public TMP_InputField confirm;
        public TMP_InputField quit;

        public void Update()
        {
            // player names
            player1.GetComponentInChildren<TextMeshProUGUI>().SetText(GameManager.Instance.GetName(1));
            player2.GetComponentInChildren<TextMeshProUGUI>().SetText(GameManager.Instance.GetName(2));
        
            // updating text based on current values
            GetKeyValues(player1, 1);
            GetKeyValues(player2, 2);

            pause.text = GameManager.Instance.GeneralActions[GeneralAction.Pause].ToString();
            confirm.text = GameManager.Instance.GeneralActions[GeneralAction.Confirm].ToString();
            quit.text = GameManager.Instance.GeneralActions[GeneralAction.Quit].ToString();

            // who is currently updating
            switch (_playerKey)
            {
                case 1:
                    keyUpdate.SetText(GameManager.Instance.GetName(1) + " updating key.");
                    break;
                case 2:
                    keyUpdate.SetText(GameManager.Instance.GetName(2) + " updating key.");
                    break;
                case 3:
                    keyUpdate.SetText( "Updating general controls.");
                    break;
            }
        
            // when setting a new keyCode
            if (_waitingKey)
            {
                if (_playerKey == 3) // general controls
                { 
                    if (Input.anyKey) 
                    { 
                        foreach (KeyCode k in System.Enum.GetValues(typeof(KeyCode))) 
                        { 
                            if (Input.GetKeyDown(k) && k != KeyCode.Mouse0) 
                            { 
                                _newKey = k; 
                                UpdateGeneral(_childKey);
                                _waitingKey = false;
                                _newKey = KeyCode.None;
                                break;
                            }
                        }
                    }
                }
                else if (_playerKey != 3 && _childKey != 0) // player controls
                {
                    if (Input.anyKey)
                    { 
                        foreach (KeyCode k in System.Enum.GetValues(typeof(KeyCode))) 
                        { 
                            if (Input.GetKeyDown(k) && k != KeyCode.Mouse0) 
                            { 
                                _newKey = k; 
                                UpdateKeyAction(_playerKey); 
                                _waitingKey = false; 
                                _newKey = KeyCode.None; 
                                break;
                            } 
                        }
                    }
                }
            }
        }
    
        public void SetArrows(int player)
        {
            SetArrowsKeys(player == 1 ? GameManager.Instance.PlayerBlue : GameManager.Instance.PlayerRed);
        }
    
        private static void SetArrowsKeys(Dictionary<PlayerAction, KeyCode> dict)
        {
            dict[PlayerAction.MoveLeft] = KeyCode.LeftArrow;
            dict[PlayerAction.MoveRight] = KeyCode.RightArrow;
            dict[PlayerAction.Jump] = KeyCode.UpArrow;
            dict[PlayerAction.EnterAimingMode] = KeyCode.Z;
            dict[PlayerAction.AimHigher] = KeyCode.UpArrow;
            dict[PlayerAction.AimLower] = KeyCode.DownArrow;
            dict[PlayerAction.Shoot] = KeyCode.Space;
        }

        public void SetQwerty(int player)
        {
            setQWERTY_Keys(player == 1 ? GameManager.Instance.PlayerBlue : GameManager.Instance.PlayerRed);
        }

        private void setQWERTY_Keys(Dictionary<PlayerAction, KeyCode> dict)
        {
            dict[PlayerAction.MoveLeft] = KeyCode.A;
            dict[PlayerAction.MoveRight] = KeyCode.D;
            dict[PlayerAction.Jump] = KeyCode.W;
            dict[PlayerAction.EnterAimingMode] = KeyCode.Q;
            dict[PlayerAction.AimHigher] = KeyCode.W;
            dict[PlayerAction.AimLower] = KeyCode.S;
            dict[PlayerAction.Shoot] = KeyCode.E;
        }
    
    
        // update control key
        private void UpdateKeyAction(int p)
        {
            if (_newKey != KeyCode.None)
            {
                switch (_childKey)
                {
                    case 1:
                        GameManager.Instance.GetPlayerKeys(p)[PlayerAction.MoveLeft] = _newKey;
                        break;
                    case 2:
                        GameManager.Instance.GetPlayerKeys(p)[PlayerAction.MoveRight] = _newKey;
                        break;
                    case 3:
                        GameManager.Instance.GetPlayerKeys(p)[PlayerAction.EnterAimingMode] = _newKey;
                        break;
                    case 4:
                        GameManager.Instance.GetPlayerKeys(p)[PlayerAction.AimHigher] = _newKey;
                        break;
                    case 5:
                        GameManager.Instance.GetPlayerKeys(p)[PlayerAction.AimLower] = _newKey;
                        break;
                    case 6:
                        GameManager.Instance.GetPlayerKeys(p)[PlayerAction.Shoot] = _newKey;
                        break;
                    case 7:
                        GameManager.Instance.GetPlayerKeys(p)[PlayerAction.Jump] = _newKey;
                        break;
                }
            }
        }

        public void ChangePlayer()
        {
            switch (_playerKey)
            {
                case 1 :
                    _playerKey = 2;
                    break;
                case 2 :
                    _playerKey = 3;
                    break;
                case 3 :
                    _playerKey = 1;
                    break;
            }
        }

    
        // get current control value
        private void GetKeyValues(Transform player, int p)
        {
            _gettingKey = true;
            // key values player 1
            for (int i = 1; i < player.childCount; i++)
            {
                if (player.GetChild(i).name == "GoLeft")
                {
                    player.GetChild(i).GetComponent<TMP_InputField>().text = 
                        GameManager.Instance.GetPlayerKeys(p)[PlayerAction.MoveLeft].ToString();
                }
                else if (player.GetChild(i).name == "GoRight")
                {
                    player.GetChild(i).GetComponent<TMP_InputField>().text = 
                        GameManager.Instance.GetPlayerKeys(p)[PlayerAction.MoveRight].ToString();
                }
                else if (player.GetChild(i).name == "EnterAim")
                {
                    player.GetChild(i).GetComponent<TMP_InputField>().text = 
                        GameManager.Instance.GetPlayerKeys(p)[PlayerAction.EnterAimingMode].ToString();
                }
                else if (player.GetChild(i).name == "AimHigh")
                {
                    player.GetChild(i).GetComponent<TMP_InputField>().text = 
                        GameManager.Instance.GetPlayerKeys(p)[PlayerAction.AimHigher].ToString();
                }
                else if (player.GetChild(i).name == "AimLow")
                {
                    player.GetChild(i).GetComponent<TMP_InputField>().text = 
                        GameManager.Instance.GetPlayerKeys(p)[PlayerAction.AimLower].ToString();
                }
                else if (player.GetChild(i).name == "Shoot")
                {
                    player.GetChild(i).GetComponent<TMP_InputField>().text = 
                        GameManager.Instance.GetPlayerKeys(p)[PlayerAction.Shoot].ToString();
                }
                else if (player.GetChild(i).name == "Jump")
                {
                    player.GetChild(i).GetComponent<TMP_InputField>().text = 
                        GameManager.Instance.GetPlayerKeys(p)[PlayerAction.Jump].ToString();
                
                }
            }

            _gettingKey = false;
        }
    
        // if we're expecting new key value
        public void StartWaitingForKey( int child)
        {
            if (!_gettingKey)
            {
                _waitingKey = true;
                _childKey = child;
            }
        }

        private void UpdateGeneral(int i)
        {
            switch (i)
            {
                case 1:
                    GameManager.Instance.GeneralActions[GeneralAction.Pause] = _newKey;
                    break;
                case 2:
                    GameManager.Instance.GeneralActions[GeneralAction.Confirm] = _newKey;
                    break;
                case 3:
                    GameManager.Instance.GeneralActions[GeneralAction.Quit] = _newKey;
                    break;
            }
        }
    
        // go back
        public void Return()
        {
            settingsMenu.SetActive(!settingsMenu.activeSelf);
            inputMenu.SetActive(false);
            Debug.Log("Player go back to settings menu");
        }

        // reset values for general controls
        public void ResetGeneral()
        {
            GameManager.Instance.GeneralActions[GeneralAction.Pause] = KeyCode.P;
            GameManager.Instance.GeneralActions[GeneralAction.Confirm] = KeyCode.Return;
            GameManager.Instance.GeneralActions[GeneralAction.Quit] = KeyCode.Escape;
        }
    }
}
