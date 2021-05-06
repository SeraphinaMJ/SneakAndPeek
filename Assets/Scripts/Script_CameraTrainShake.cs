using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_CameraTrainShake : MonoBehaviour
{
    public float power = 0.5f;
    public float duration = 1.0f;
    public Transform cameraT;
    public float slowDown = 1.0f;
    public bool shake = false;

    private Vector3 startPos;
    private float initialDur;

    void Start()
    {
        cameraT = Camera.main.transform;
        startPos = cameraT.localPosition;
        initialDur = duration;
    }

    void Update()
    {
        if (shake)
        {
            cameraT.localPosition = startPos + Random.insideUnitSphere * power;
            duration -= Time.deltaTime * slowDown;
        }
        else
        {
            shake = false;
            duration = initialDur;
            cameraT.localPosition = startPos;
        }
    }

}
