using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    // Start is called before the first frame update
    public enum Level {Main,Shoes,Building,Bank };
    static Level LV;
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
        if(Entrance.prevLV == Entrance.PreviousLV.Building)
        {

        }
    }
}
