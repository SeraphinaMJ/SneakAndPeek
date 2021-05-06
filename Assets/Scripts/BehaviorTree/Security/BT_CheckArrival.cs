using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class BT_CheckArrival : Script_BTNode
{
    BT_AIBehavior m_AI;
    //Minimun range that is considered arrival
    float m_range;
    public BT_CheckArrival(BT_AIBehavior AI,float range)
    {
        m_AI = AI;
        m_range = range;
    }
    public override BT_NodeStates Evaluate()
    {    
        Vector3 agentPos = m_AI.GetAgentTransform().position;
        Vector3 targetPos = m_AI.GetTargetPos();
        float dist = Vector3.Distance(agentPos, targetPos);

        if (dist < m_range)
        {
            if (m_AI.GetState() != State.IDLE)
            {
                if (m_AI.GetAnim() != null)
                    m_AI.GetAnim().Play("IDLE");
                m_AI.SetState(State.IDLE);
            }
            if (m_AI.GetIsSuspicious() && !m_AI.GetIsTargetInSight())
            {
                float radius = 4f;

                //Get vector around 6 ways of target poisition 
                //45,90,135,180... every 45deg
                //(transform.position.x + rcostheta,0,transform.position.z + rsintheta)  
                for (int i = 0; i < 360; i += 45)
                {
                    Vector3 target = new Vector3(m_AI.GetTargetPos().x + radius * Mathf.Cos(i),
                                                 m_AI.GetAgentTransform().position.y,
                                                 m_AI.GetTargetPos().z + radius * Mathf.Sin(i));

                    Vector3 dirToTarget = (target - agentPos).normalized; 
                    float distToTarget = Vector3.Distance(agentPos, target);
                    NavMeshHit navHit;
                    NavMesh.SamplePosition(target, out navHit, distToTarget, NavMesh.AllAreas);
                    if (Mathf.Abs(navHit.position.y - target.y) < 2f)
                    {
                        m_AI.GetWaypointList().Add(navHit.position);
                    }
                    
                }
            }

            return BT_NodeStates.SUCESS; }
        else
        {return BT_NodeStates.FAILURE; }
    }

}
