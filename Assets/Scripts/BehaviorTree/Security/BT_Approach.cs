using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BT_Approach : Script_BTNode
{
    BT_AIBehavior m_AI;
    Transform m_agentTransform;
    Vector3 m_targetPos;

    public BT_Approach(BT_AIBehavior AI)
    {
        m_AI = AI;
    }
    public override BT_NodeStates Evaluate()
    {
        m_targetPos = m_AI.GetTargetPos();

        //Move to target point
        m_AI.GetAgentNavMesh().SetDestination(m_targetPos);

        return BT_NodeStates.SUCESS;

    }
}
