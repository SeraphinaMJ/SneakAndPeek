using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_ReflectionMgr : MonoBehaviour
{
    Camera m_reflectionCam;
    Camera m_mainCam;

    [SerializeField]
    private Material m_reflectionMaterial = null;

    [SerializeField]
    [Range(0.0f,12.0f)]
    private float m_reflectionFactor = 0.5f;

    RenderTexture m_renderTarget;
    // Start is called before the first frame update
    void Start()
    {
        GameObject reflectionCamGo = new GameObject("ReflectionCamera");
        m_reflectionCam = reflectionCamGo.AddComponent<Camera>();
        m_reflectionCam.enabled = false;

        m_mainCam = Camera.main;

        m_renderTarget = new RenderTexture(Screen.width, Screen.height, 24);

    }

    // Update is called once per frame
    void Update()
    {
        Shader.SetGlobalFloat("_reflectionFactor", m_reflectionFactor);
        RenderReflection();
    }

    void RenderReflection()
    {
        m_reflectionCam.CopyFrom(m_mainCam);
        Vector3 camDirectionWorldSpace = m_mainCam.transform.forward;
        Vector3 camUpWorldSpace = m_mainCam.transform.up;
        Vector3 camPosWorldSpace = m_mainCam.transform.position;

        Vector3 camDirectionPlaneSpace = transform.InverseTransformDirection(camDirectionWorldSpace);
        Vector3 camUpPlaneSpace = transform.InverseTransformDirection(camUpWorldSpace);
        Vector3 camPosPlaneSpace = transform.InverseTransformPoint(camPosWorldSpace);

        camDirectionPlaneSpace.y *= -1f;
        camUpPlaneSpace.y *= -1f;
        camPosPlaneSpace.y = camPosPlaneSpace.y *- 1f+ transform.position.y;//transform.position.y;//

        camDirectionWorldSpace = transform.TransformDirection(camDirectionPlaneSpace);
        camUpWorldSpace = transform.TransformDirection(camUpPlaneSpace);
        camPosWorldSpace = transform.TransformPoint(camPosPlaneSpace);

        m_reflectionCam.transform.position = camPosWorldSpace;
        m_reflectionCam.transform.LookAt(camPosWorldSpace + camDirectionWorldSpace, camUpWorldSpace);

        //Set render target to reflection cam

        m_reflectionCam.targetTexture = m_renderTarget;

        m_reflectionCam.Render();

        m_reflectionMaterial.SetTexture("_ReflectionTex", m_renderTarget);
    }

}
