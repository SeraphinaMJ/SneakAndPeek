using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT_Idle : Script_BTNode
{
    private float m_waitingTime = Random.Range(0,5);
    //deltatime for waiting
    private float m_dt = 0;
    BT_AIBehavior m_AI;
    Transform m_target = null;

    public BT_Idle(BT_AIBehavior AI, Transform target)
    {
        m_AI = AI;
        m_target = target;
    }

    public override BT_NodeStates Evaluate()
    {
        m_dt += Time.fixedDeltaTime;

        m_AI.GetAgentNavMesh().speed = 0f;
        if (m_AI.GetState() != State.IDLE)
        {
            if (m_AI.GetAnim() != null)
                m_AI.GetAnim().Play("IDLE");
            m_AI.SetState(State.IDLE);
        }

        if (m_AI.GetIsSuspicious())
        {
            return BT_NodeStates.SUCESS;
        }
        else
        {
            if (m_waitingTime < m_dt)
            {
                m_dt = 0;
                m_waitingTime = Random.Range(0, 3);
                return BT_NodeStates.SUCESS;
            }
            else
            {
                return BT_NodeStates.FAILURE;

            }
        }
    }
}
