using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BT_Detect : Script_BTNode
{
    //deltatime for waiting
    BT_AIBehavior m_AI;
    List<Transform> m_enemyInSight = null;
    Transform m_target = null;
    bool mIsPlayerDetected;
    float MidDist = 7f;
    float LowDist = 4f;

    public BT_Detect(BT_AIBehavior AI, List<Transform> enemyInSight, MoveSoundUI soundCheck)
    {
        m_AI = AI;
        m_enemyInSight = enemyInSight;
        mIsPlayerDetected = false;
    }
    public override BT_NodeStates Evaluate()
    {
        
        m_enemyInSight = m_AI.EnemyInSight();
        if (m_enemyInSight.Count != 0)
        {
            foreach (Transform VisibleTargets in m_enemyInSight)
            {
                if (VisibleTargets.tag == "Player")
                {
                    m_target = VisibleTargets;
                    mIsPlayerDetected = true;
                }
                else if (!mIsPlayerDetected)
                    m_target = VisibleTargets.root;
            }
            if (!mIsPlayerDetected)
            {
                float distToPlayer = Vector3.Distance(m_AI.GetAgentTransform().position, MoveSoundUI.instance.PlayerInfo.position);
                if (MoveSoundUI.instance != null)
                {
                    switch (MoveSoundUI.instance.MovementStatus)
                    {
                        case MoveSoundUI.MovementSoundStatus.High:
                            m_target = MoveSoundUI.instance.PlayerInfo;
                            break;
                        case MoveSoundUI.MovementSoundStatus.Middle:
                                if(distToPlayer < MidDist){m_target = MoveSoundUI.instance.PlayerInfo;}
                            break;
                        case MoveSoundUI.MovementSoundStatus.Little:
                            if (distToPlayer < LowDist) { m_target = MoveSoundUI.instance.PlayerInfo; }
                            break;
                    }
                }
            }
            mIsPlayerDetected = false;


            NavMeshHit navHit;
            float distToTarget = Vector3.Distance(m_AI.GetAgentTransform().position, m_target.position);   
            NavMesh.SamplePosition(m_target.position, out navHit, distToTarget, NavMesh.AllAreas);
            if(Mathf.Abs(navHit.position.y - m_target.position.y)>2f)
            {
                m_AI.IsTargetInsight(false);
                m_AI.IsSuspicious(true);
                return BT_NodeStates.FAILURE;
            }

            m_AI.GetAgentNavMesh().speed = 4f;
            m_AI.SetTargetPos(navHit.position);

            m_AI.SetTarget(m_target);
            m_AI.GetWaypointList().Clear();
            m_AI.IsTargetInsight(true);
            m_AI.IsSuspicious(true);

            if (m_AI.GetState() != State.RUN)
            {
                if (m_AI.GetAnim() != null)
                    m_AI.GetAnim().Play("RUN");
                m_AI.SetState(State.RUN);
            }
            return BT_NodeStates.SUCESS;
        }
        else
        {
            m_target = null;
            float distToPlayer = Vector3.Distance(m_AI.GetAgentTransform().position, MoveSoundUI.instance.PlayerInfo.position);
            switch (MoveSoundUI.instance.MovementStatus)
            {
                case MoveSoundUI.MovementSoundStatus.High:
                    m_target = MoveSoundUI.instance.PlayerInfo;
                    break;
                case MoveSoundUI.MovementSoundStatus.Middle:
                    if (distToPlayer < MidDist) { m_target = MoveSoundUI.instance.PlayerInfo; }
                    break;
                case MoveSoundUI.MovementSoundStatus.Little:
                    if (distToPlayer < LowDist) { m_target = MoveSoundUI.instance.PlayerInfo; }
                    break;
            }
            if (m_target != null)
            {

                NavMeshHit navHit;
                float distToTarget = Vector3.Distance(m_AI.GetAgentTransform().position, m_target.position);
                NavMesh.SamplePosition(m_target.position, out navHit, distToTarget, NavMesh.AllAreas);

                if (Mathf.Abs(navHit.position.y - m_target.position.y) > 2f)
                {
                    m_AI.IsTargetInsight(false);
                    m_AI.IsSuspicious(true);
                    return BT_NodeStates.FAILURE;
                }

                m_AI.GetAgentNavMesh().speed = 4f;
                m_AI.SetTargetPos(navHit.position);

                m_AI.SetTarget(m_target);
                m_AI.GetWaypointList().Clear();
                m_AI.IsTargetInsight(true);
                m_AI.IsSuspicious(true);

                if (m_AI.GetState() != State.RUN)
                {
                    m_AI.GetAnim().Play("RUN");
                    m_AI.SetState(State.RUN);
                }
                return BT_NodeStates.SUCESS;
            }
            else
            {
                m_AI.IsTargetInsight(false);
                return BT_NodeStates.FAILURE;
            }
        }
       
    }

}