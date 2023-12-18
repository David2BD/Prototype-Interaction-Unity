using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoldierCustomization : MonoBehaviour
{
    private Color color;
    private int model;
    public Material materialBody;
    public Material materialWeapon;
    
    // Start is called before the first frame update
    void Start()
    {
        color = Color.clear;
        model = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeBodyColor(Color p_color)
    {
        color = p_color;
        materialBody.color = color;
    }
}
