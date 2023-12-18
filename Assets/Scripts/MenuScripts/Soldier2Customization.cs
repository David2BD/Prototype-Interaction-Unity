using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Soldier2Customization : MonoBehaviour
{
    private Color bodyColor;
    private int model;
    public Material materialBody;
    
    // Start is called before the first frame update
    void Start()
    {
        bodyColor = Color.white;
        model = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeBodyColor(Color p_color)
    {
        bodyColor = p_color;
        materialBody.color = bodyColor;
    }


}
