using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailMover : MonoBehaviour
{
    public RailScripts rail;
    public Transform lookAt;
    public bool smoothMove = true;
    public float moveSpeed = 10.0f;

    private Transform thisTransform;
    private Vector3 lastPosition;

    private void Start()
    {
        thisTransform = transform;
    }

    private void Update()
    {

        if (smoothMove)
        {
            lastPosition = Vector3.Lerp(lastPosition, rail.ProjectPositionOnRail(lookAt.position), Time.deltaTime * moveSpeed);
            thisTransform.position = lastPosition;
        }
        else
        {
            thisTransform.position = rail.ProjectPositionOnRail(lookAt.position);
        }

        thisTransform.LookAt(lookAt.position);
        //thisTransform.position = rail.ProjectOnSegment(Vector3.zero, Vector3.forward * 20, lookAt.position);
    }
}
