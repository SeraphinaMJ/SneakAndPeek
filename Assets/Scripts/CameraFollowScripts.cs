//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class CameraFollowScripts : MonoBehaviour
//{
//    //The target object
//    public Transform targetObject;

//    //Default distance between the target and the player
//    public Vector3 cameraoffset;

//    //Smooth factor will use in Camera rotation
//    public float smoothFactor = 0.5f;

//    //will check that the camera looked at on the target on or not.
//    public bool lookAtTarget = false;


//    // Start is called before the first frame update
//    void Start()
//    {
//        cameraoffset = transform.position - targetObject.transform.position;
//    }

//    // Update is called once per frame
//    void LateUpdate()
//    {
//        Vector3 newPosition = targetObject.transform.position + cameraoffset;
//        transform.position = Vector3.Slerp(transform.position, newPosition, smoothFactor);

//        //Camera Rotation change
//        if (lookAtTarget)
//        {
//            transform.LookAt(targetObject);
//        }

//    }
//}
