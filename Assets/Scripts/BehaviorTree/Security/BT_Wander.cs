using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class BT_Wander : Script_BTNode
{
    //maximun movement range in a single state
    float m_range;
    BT_AIBehavior m_AI;

    public BT_Wander(BT_AIBehavior AI,float range)
    {
        m_range = range;
        m_AI = AI;
    }

    public override BT_NodeStates Evaluate()
    {
        Vector3 targetPos;
        NavMeshHit navHit;
        if (m_AI.GetWaypointList().Count() != 0)
        {
            do
            {
                int randomIndex = Random.Range(0, m_AI.GetWaypointList().Count() - 1);
                //Random for now
                targetPos = m_AI.GetWaypointList().ElementAt(randomIndex);
                m_AI.GetAgentNavMesh().speed = 2.5f;

                m_AI.SetSearchCount(m_AI.GetSearchCount() + 1);

                NavMesh.SamplePosition(targetPos, out navHit, m_range, NavMesh.AllAreas);
            } while (navHit.position.x == Mathf.Infinity);
        }
        else
        {
            do
            {
                Vector3 randomDirection = new Vector3(Random.Range(-m_range, m_range), Random.Range(-m_range, m_range), Random.Range(-m_range, m_range));
                Vector3 agentPos = m_AI.GetAgentTransform().position;
                targetPos = randomDirection + agentPos;
                m_AI.GetAgentNavMesh().speed = 1.5f;

                NavMesh.SamplePosition(targetPos, out navHit, m_range, NavMesh.AllAreas);
            } while (navHit.position.x == Mathf.Infinity);
        }

        if (m_AI.GetSearchCount() == 5)
        {
            m_AI.IsSuspicious(false);
            m_AI.GetWaypointList().Clear();
            m_AI.SetSearchCount(0);
        }
        
        
       // Debug.Log(navHit.position);

       
        m_AI.SetTargetPos(navHit.position);
        if (m_AI.GetState() != State.WALK)
        {
            if(m_AI.GetAnim()!=null)
                m_AI.GetAnim().Play("WALK");
            m_AI.SetState(State.WALK);
        }
        return BT_NodeStates.SUCESS;

    }
}
