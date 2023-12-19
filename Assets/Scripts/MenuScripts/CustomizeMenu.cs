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
    
    private TMP_Dropdown colorBodyMenu;
    private TMP_Dropdown colorWeaponMenu;
    private TMP_Dropdown modelMenu;
    
    private bool modelSwitched = false;
    
    
    private TMP_Dropdown AccMenu;
    
    public GameObject soldier1Visual;
    public GameObject soldier2Visual;
    private GameObject soldierAcc;
    public  SoldierCustomization soldier;
    public Soldier2Customization soldier2;
    private MeshFilter mesh;
    private Material material;

    public static int model = 0 ;
    public static Color colorBody = Color.white;
    public static Color colorWeapon = Color.white;
    public static int currentHat = 0;
    
    
    // Start is called before the first frame update
    void Start()
    {
        colorBodyMenu = GameObject.Find("BodyColorDropdown").GetComponent<TMP_Dropdown>();
        colorWeaponMenu = GameObject.Find("WeaponColorDropdown").GetComponent<TMP_Dropdown>();
        modelMenu = GameObject.Find("ModelDropdown").GetComponent<TMP_Dropdown>();
        AccMenu = GameObject.Find("AccessoryDropdown").GetComponent<TMP_Dropdown>();

    }

    // Update is called once per frame
    void Update()
    {
        //Activate selected Model
        if (modelMenu.value == 0 && !modelSwitched)
        {
            soldier1Visual.SetActive(!soldier1Visual.activeSelf);
            soldier2Visual.SetActive(false);
            model = 0;
            modelSwitched = true;
        }
        else if (modelMenu.value == 1 && modelSwitched)
        {
            soldier2Visual.SetActive(!soldier2Visual.activeSelf);
            soldier1Visual.SetActive(false);
            model = 1;
            modelSwitched = false;
        }
        
        
        //body Color swap
        if (colorBodyMenu.value == 0)
        {
           soldier.ChangeBodyColor(Color.white);
           soldier2.ChangeBodyColor(Color.white);
           colorBody = Color.white;
        }
        else if (colorBodyMenu.value == 1)
        {
            soldier.ChangeBodyColor(Color.blue);
            soldier2.ChangeBodyColor(Color.blue);
            colorBody = Color.blue;
        }
        else if (colorBodyMenu.value == 2)
        {
            soldier.ChangeBodyColor(Color.red);
            soldier2.ChangeBodyColor(Color.red);
            colorBody = Color.red;
        }
        else if (colorBodyMenu.value == 3)
        {
            soldier.ChangeBodyColor(Color.green);
            soldier2.ChangeBodyColor(Color.green);
            colorBody = Color.green;
        }
        
        //weapon color swap
        if (colorWeaponMenu.value == 0)
        {
            soldier.ChangeWeaponColor(Color.white);
            colorWeapon = Color.white;
        }
        else if (colorWeaponMenu.value == 1)
        {
            soldier.ChangeWeaponColor(Color.blue);
            colorWeapon = Color.blue;
        }
        else if (colorWeaponMenu.value == 2)
        {
            soldier.ChangeWeaponColor(Color.red);
            colorWeapon = Color.red;
        }
        else if (colorWeaponMenu.value == 3)
        {
            soldier.ChangeWeaponColor(Color.green);
            colorWeapon = Color.green;
        }
        
        //Hat swap
        
        if (AccMenu.value == 0 && currentHat != 0) //Default
        {
            soldier.ChangeAcc(0);
            currentHat = 0;
        }
        else if (AccMenu.value == 1 && currentHat != 1) //Viking Helm
        {
            soldier.ChangeAcc(1);
            currentHat = 1;
        }
        else if (AccMenu.value == 2 && currentHat != 2) //Miner hat
        {
            soldier.ChangeAcc(2);
            currentHat = 2;
        }
        else if (AccMenu.value == 3 && currentHat != 3) //Magician hat
        {
            soldier.ChangeAcc(3);
            currentHat = 3;
        }

            
        
    }
    
    public void Return()
    {
        mainMenu.SetActive(!mainMenu.activeSelf);
        customizeMenu.SetActive(false);
        Debug.Log("Player go back to options menu");
    }
}
