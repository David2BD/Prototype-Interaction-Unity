using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisManager : MonoBehaviour
{
    public float sensitivity;
    public float gravity;
    public float deadzone;
    public KeyCode plus;
    public KeyCode minus;

    public float V { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        V = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(plus))
        {
            V = Math.Min(V + sensitivity, 1f);
        }
        else if (Input.GetKeyDown(minus))
        {
            V = Math.Min(V - sensitivity, -1f);
        }
        else if (V >= deadzone)
        {
            V -= gravity;
        }
        else if (V < deadzone)
        {
            V += gravity;
        }
        else
        {
            V = 0;
        }
    }
}
