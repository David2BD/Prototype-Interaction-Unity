using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioListenerController : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;

    public AudioListener cameraListener;

    private AudioListener listenerPlayer1;
    private AudioListener listenerPlayer2;
    
    // Start is called before the first frame update
    void Start()
    {
        listenerPlayer1 = player1.GetComponent<AudioListener>();
        listenerPlayer2 = player2.GetComponent<AudioListener>();

        listenerPlayer1.enabled = true;
        listenerPlayer2.enabled = false;
        cameraListener.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (listenerPlayer2 != null && listenerPlayer1 != null)
        {
            if (GameManager.Instance.getTurn() == 1)
            {
                listenerPlayer1.enabled = true;
                listenerPlayer2.enabled = false;
            }
            else
            {
                listenerPlayer1.enabled = false;
                listenerPlayer2.enabled = true;
            }
        }

    }
}
