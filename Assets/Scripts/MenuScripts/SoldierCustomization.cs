using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoldierCustomization : MonoBehaviour
{
    private Color bodyColor;
    private Color weaponColor;
    private int model;
    public Material materialBody;
    public Material materialWeapon;
    
    // Start is called before the first frame update
    void Start()
    {
        bodyColor = Color.white;
        weaponColor = Color.white;
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

    public void ChangeWeaponColor(Color p_color)
    {
        weaponColor = p_color;
        materialWeapon.color = weaponColor;
    }
}
