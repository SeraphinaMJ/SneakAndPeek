using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_NPCLookAt : MonoBehaviour
{
    Vector3 m_target;
    [SerializeField]
    Transform agent = null;
    void Update()
    {
        DirectionGuide();     
    }

    void DirectionGuide()
    {
        m_target = new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z);
        transform.LookAt(m_target);
        Vector3 dirToTarget = (transform.position - agent.transform.position).normalized;
        transform.position = agent.transform.position;
        Script_MovtRotationMgr rotationInstance = Script_MovtRotationMgr.instance;
        switch (rotationInstance.currentDirection)
        {
            case Script_MovtRotationMgr.DIRECTION.RIGHT:
                if (dirToTarget.x < 0) GetComponent<SpriteRenderer>().flipX = true;
                else GetComponent<SpriteRenderer>().flipX = false;
                break;
            case Script_MovtRotationMgr.DIRECTION.FORWARD:
                if (dirToTarget.z < 0) GetComponent<SpriteRenderer>().flipX = true;
                else GetComponent<SpriteRenderer>().flipX = false;
                break;
            case Script_MovtRotationMgr.DIRECTION.LEFT:
                if (dirToTarget.x > 0) GetComponent<SpriteRenderer>().flipX = true;
                else GetComponent<SpriteRenderer>().flipX = false;
                break;
            case Script_MovtRotationMgr.DIRECTION.BACK:
                if (dirToTarget.z > 0) GetComponent<SpriteRenderer>().flipX = true;
                else GetComponent<SpriteRenderer>().flipX = false;
                break;
        }

    }
}
