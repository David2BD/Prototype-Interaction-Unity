using System.Collections;
using System.Collections.Generic;
using MenuScripts;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CustomizeMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject customizeMenu;
    
    public GameObject dropdownBodyColor;
    private TMP_Dropdown colorBodyMenu;
    
    public GameObject dropdownWeapColor;
    private TMP_Dropdown colorWeaponMenu;
    
    public GameObject dropdownMenuModel;
    private Dropdown modelMenu;
    
    public  SoldierCustomization soldier;
    private MeshFilter mesh;
    private Material material;
    
    
    // Start is called before the first frame update
    void Start()
    {
        colorBodyMenu = GameObject.Find("BodyColorDropdown").GetComponent<TMP_Dropdown>();
        //colorBodyMenu = dropdownBodyColor.GetComponent<TMP_Dropdown>();
        colorWeaponMenu = GameObject.Find("WeaponColorDropdown").GetComponent<TMP_Dropdown>();
       // mesh = Soldier.GetComponent<MeshFilter>();
        //material = mesh.GetComponent<Material>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (colorBodyMenu.value == 0)
        {
           soldier.ChangeBodyColor(Color.white);
        }
        else if (colorBodyMenu.value == 1)
        {
            soldier.ChangeBodyColor(Color.blue);
        }
        else if (colorBodyMenu.value == 2)
        {
            soldier.ChangeBodyColor(Color.red);
        }
        else if (colorBodyMenu.value == 3)
        {
            soldier.ChangeBodyColor(Color.green);
        }
        
        
        if (colorWeaponMenu.value == 0)
        {
            soldier.ChangeWeaponColor(Color.white);
        }
        else if (colorWeaponMenu.value == 1)
        {
            soldier.ChangeWeaponColor(Color.blue);
        }
        else if (colorWeaponMenu.value == 2)
        {
            soldier.ChangeWeaponColor(Color.red);
        }
        else if (colorWeaponMenu.value == 3)
        {
            soldier.ChangeWeaponColor(Color.green);
        }
        
    }
    
    public void Return()
    {
        mainMenu.SetActive(!mainMenu.activeSelf);
        customizeMenu.SetActive(false);
        Debug.Log("Player go back to options menu");
    }
}
