using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Not working! working on it./
public class Script_NPCPreventRotation : MonoBehaviour
{
    Vector3 m_updatePos;
    void Start()
    {
        //Direction Based on Player Direction
        m_updatePos = gameObject.transform.GetChild(0).position;
    }

    void Update()
    {
        m_updatePos = gameObject.transform.GetChild(0).position;

        transform.position = m_updatePos; // restore original rotation with new Y
    }
}
