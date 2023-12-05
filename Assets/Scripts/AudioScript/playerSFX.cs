using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSFX : MonoBehaviour
{
    public AudioSource[] audioSource; // Reference to the AudioSource component
    // 0 - shoot, 1 - walk

    private int current = 1;

    void Update()
    {
        
        current = GameManager.Instance.getTurn();
        if (Input.GetKey(GameManager.Instance.GetPlayerKeys(current)[PlayerAction.MoveLeft]) || 
                         Input.GetKey(GameManager.Instance.GetPlayerKeys(current)[PlayerAction.MoveRight]))
        {
            if (!audioSource[0].isPlaying)
            {
                audioSource[0].Play();
            }
        }
        else
        {
            audioSource[0].Stop();
        }

        if (Input.GetKey(GameManager.Instance.GetPlayerKeys(current)[PlayerAction.Shoot]))
        {
            audioSource[1].Play();
        }
        
    }
}
