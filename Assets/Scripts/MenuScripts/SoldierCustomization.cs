using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class SoldierCustomization : MonoBehaviour
{
    private Color bodyColor;
    private Color weaponColor;
    private int model;
    public GameObject vikingHat;
    public GameObject magicianHat;
    public GameObject minerHat;
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

    public void ChangeAcc(int p_acc)
    {
        if (p_acc == 0)
        {
            magicianHat.SetActive(false);
            minerHat.SetActive(false);
            vikingHat.SetActive(false);
        }
        else if (p_acc == 1) 
        {
            magicianHat.SetActive(false);
            minerHat.SetActive(false);
            vikingHat.SetActive(!vikingHat.activeSelf);
        }
        else if (p_acc == 2)
        {
            magicianHat.SetActive(false);
            vikingHat.SetActive(false);
            minerHat.SetActive(!minerHat.activeSelf);
        }
        else if (p_acc == 3)
        {
            minerHat.SetActive(false);
            vikingHat.SetActive(false);
            magicianHat.SetActive(!magicianHat.activeSelf);
        }
    }
}
