using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT_Catch : Script_BTNode
{
    BT_AIBehavior m_AI;
    float m_range;
    public BT_Catch(BT_AIBehavior AI, float range)
    {
        m_AI = AI;
        m_range = range;
    }
    public override BT_NodeStates Evaluate()
    {
        Transform target = m_AI.GetTarget();
        Vector3 agentPos = m_AI.GetAgentTransform().position;
        Vector3 targetPos = target.position;
        float dist = Vector3.Distance(agentPos, targetPos);
        if (PlayerAction.instance.mStatus == PlayerAction.PlayerStatus.Hide)
            m_range = 0;

        if ((target.tag=="Player")&&(dist < m_range))
        {
            m_AI.GetAgentTransform().transform.GetComponent<SpriteRenderer>().color = Color.red;
            m_AI.SetCondition(true);
            
            return BT_NodeStates.SUCESS; }
        else
        {
            m_AI.GetAgentTransform().transform.GetComponent<SpriteRenderer>().color = Color.white;
            return BT_NodeStates.FAILURE; }
    }
}
