using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    private GameObject player;
    private GameObject mainCam;
    private bool once;
    private float curTime;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mainCam = GameObject.FindGameObjectWithTag("MainCamera");
        once = false;
        curTime = 0;      
    }

    // Update is called once per frame
    void Update()
    {   
        if (Entrance.prevLV == Entrance.PreviousLV.Shoes)
        {
            if (!once)
            {
                player.transform.position = new Vector3(44.05f, 0.96f, 11.38f);            
                once = true;          
            }
        }
        if (Entrance.prevLV == Entrance.PreviousLV.Building)
        {
            curTime += Time.deltaTime;
            if (curTime <= 0.3f)
            {
                if (!once)
                {
                    Script_MovtRotationMgr.instance.currentDirection = Script_MovtRotationMgr.DIRECTION.RIGHT;
                    player.transform.position = new Vector3(57.291f, 0.96f, -17f);                                  
                    mainCam.transform.position = new Vector3(mainCam.transform.position.x, mainCam.transform.position.y, -24.88f);
                }
            }
            else
            {
                once = true;
                curTime = 0.4f;
            }
        }
        if (Entrance.prevLV == Entrance.PreviousLV.Bank)
        {
            curTime += Time.deltaTime;
            if (curTime <= 0.3f)
            {
                if (!once)
                {
                    Script_MovtRotationMgr.instance.currentDirection = Script_MovtRotationMgr.DIRECTION.FORWARD;
                    player.transform.position = new Vector3(76f, 0.96f, 12.55f);                       
                    mainCam.transform.position = new Vector3(82.3f, mainCam.transform.position.y, mainCam.transform.position.z);
                }
            }
            else
            {
                once = true;
                curTime = 0.4f;
            }
        }
    }

}
