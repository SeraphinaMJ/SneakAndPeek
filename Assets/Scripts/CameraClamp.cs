using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClamp : MonoBehaviour
{
    [SerializeField] private Transform targetToFollow = null;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            transform.position.x,
            transform.position.y,//Mathf.Clamp(targetToFollow.position.y, -3f, 3f),
            Mathf.Clamp(targetToFollow.position.z, -19.0f, targetToFollow.position.z));


    }
}
