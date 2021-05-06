using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupDiamond : MonoBehaviour
{
    

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            //print("Item stolen");
            
            Destroy(gameObject);
           
        }
    }
}
