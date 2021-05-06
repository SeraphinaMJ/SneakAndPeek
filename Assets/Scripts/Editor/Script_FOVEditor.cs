using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof (Script_CharacterDetection))]
public class Script_FOVEditor : Editor
{
    private void OnSceneGUI()
    {
        Script_CharacterDetection m_fov = (Script_CharacterDetection)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(m_fov.transform.position, Vector3.up, Vector3.forward, 360, m_fov.ViewRadius);
        Vector3 m_alertAngleA = m_fov.DirFromAngle(-m_fov.ViewAngle / 2f, false);
        Vector3 m_alertAngleB = m_fov.DirFromAngle(m_fov.ViewAngle / 2f, false);
        Handles.DrawLine(m_fov.transform.position, m_fov.transform.position + m_alertAngleA * m_fov.ViewRadius);
        Handles.DrawLine(m_fov.transform.position, m_fov.transform.position + m_alertAngleB * m_fov.ViewRadius);
        Handles.color = Color.red;
        foreach (Transform VisibleTargets in m_fov.m_visibleTargets)
        {
            Handles.DrawLine(m_fov.transform.position, VisibleTargets.position);
        }

    }
}
