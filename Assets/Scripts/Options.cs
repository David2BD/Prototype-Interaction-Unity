using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
    public void Return()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
