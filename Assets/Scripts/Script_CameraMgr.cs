using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_CameraMgr : MonoBehaviour
{
    private float smoothTime;
    private Vector3 saveStartPos = Vector3.zero;

    private void Start()
    {
        smoothTime = Time.time;
        saveStartPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }
    
    private void LateUpdate()
    {
        PlayerMovement playerInstance = PlayerMovement.instance;
        Script_MovtRotationMgr rotationInstance = Script_MovtRotationMgr.instance;

        Vector3 target = Vector3.zero;
        Vector3 lookTarget = Vector3.zero;
        float CamHeight = 1f;
        if (rotationInstance.IsRotating == false)
        {
            switch (Script_MovtRotationMgr.instance.currentDirection)
            {
                case Script_MovtRotationMgr.DIRECTION.RIGHT:
                    target = new Vector3(playerInstance.transform.position.x,
                                         playerInstance.transform.position.y + CamHeight,
                                         transform.position.z);

                    lookTarget = new Vector3(playerInstance.transform.position.x,
                                             playerInstance.transform.position.y + CamHeight,
                                             transform.position.z + 1);
                    break;

                case Script_MovtRotationMgr.DIRECTION.FORWARD:
                    target = new Vector3(transform.position.x,
                                         playerInstance.transform.position.y+ CamHeight,
                                         playerInstance.transform.position.z);

                    lookTarget = new Vector3(transform.position.x-1,
                                             playerInstance.transform.position.y + CamHeight,
                                             playerInstance.transform.position.z);
                    break;

                case Script_MovtRotationMgr.DIRECTION.LEFT:
                    target = new Vector3(playerInstance.transform.position.x,
                                         playerInstance.transform.position.y+ CamHeight,
                                         transform.position.z);

                    lookTarget = new Vector3(playerInstance.transform.position.x,
                                             playerInstance.transform.position.y + CamHeight,
                                             transform.position.z-1);
                    break;

                case Script_MovtRotationMgr.DIRECTION.BACK:
                    target = new Vector3( transform.position.x,
                                          playerInstance.transform.position.y+ CamHeight,
                                          playerInstance.transform.position.z);

                    lookTarget = new Vector3(transform.position.x + 1,
                                             playerInstance.transform.position.y + CamHeight,
                                             playerInstance.transform.position.z);
                    break;
            }
            saveStartPos = transform.position;
        }
        else
        {
            Vector3 pivot = new Vector3(rotationInstance.Pivot.transform.position.x,
                                          transform.position.y,
                                          rotationInstance.Pivot.transform.position.z);
            float distToPivot = Vector3.Distance(pivot, saveStartPos);
            target = new Vector3(pivot.x - playerInstance.transform.forward.x * (distToPivot),
                                 playerInstance.transform.position.y + CamHeight,
                                 pivot.z - playerInstance.transform.forward.z * (distToPivot));

            lookTarget = new Vector3(playerInstance.transform.position.x,
                                     transform.position.y,
                                     playerInstance.transform.position.z);

        }

        float coveredTime = (Time.time - smoothTime);
        float dist = Vector3.Distance(transform.position, target);
        float fraction = coveredTime / dist;
        transform.position = Vector3.Lerp(transform.position, target, fraction);

        transform.LookAt(lookTarget);
    }
}
