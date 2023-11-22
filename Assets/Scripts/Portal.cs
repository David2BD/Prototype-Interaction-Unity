using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Soldier"))
        {
            GameObject player = other.gameObject;
            Soldier playerScript = player.GetComponent<Soldier>();
            GameObject activePortal = gameObject;

            if (playerScript.GetUsedTP() == false)
            {
                if (activePortal.name == "PortalA1")
                {
                    TeleportPlayer(player, activePortal, "PortalA2");
                }
                else if (activePortal.name == "PortalA2")
                {
                    TeleportPlayer(player, activePortal, "PortalA1");
                }
                else if (activePortal.name == "PortalB1")
                {
                    TeleportPlayer(player, activePortal, "PortalB2");
                }
                else if (activePortal.name == "PortalB2")
                {
                    TeleportPlayer(player, activePortal, "PortalB1");
                }

                playerScript.SetUsedTP(true);
            }
        }
        
    }
    
    private void TeleportPlayer(GameObject soldierToTP, GameObject sourcePortal, string destinationPortalName)
    {
     GameObject destinationPortal = GameObject.Find(destinationPortalName);

        if (destinationPortal != null)
        {
            soldierToTP.transform.position = destinationPortal.transform.position;
        }
        else
        {
            Debug.LogError("Destination portal not found: " + destinationPortalName);
        }
    }
}
