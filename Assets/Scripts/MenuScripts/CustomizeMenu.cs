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
    
    public GameObject dropdownMenuColor;
    private TMP_Dropdown colorMenu;
    public GameObject dropdownMenuModel;
    private Dropdown modelMenu;
    
    public  SoldierCustomization soldier;
    private MeshFilter mesh;
    private Material material;
    
    
    // Start is called before the first frame update
    void Start()
    {
        colorMenu = dropdownMenuColor.GetComponent<TMP_Dropdown>();
       // mesh = Soldier.GetComponent<MeshFilter>();
        //material = mesh.GetComponent<Material>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (colorMenu.value == 0)
        {
           soldier.ChangeBodyColor(Color.green);
        }
    }
    
    public void Return()
    {
        mainMenu.SetActive(!mainMenu.activeSelf);
        customizeMenu.SetActive(false);
        Debug.Log("Player go back to options menu");
    }
}
