using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum State
{
    IDLE,
    WALK,
    RUN
}
public interface BT_AIBehavior
{
    Vector3 SetTargetPos(Vector3 targetPos);
    Vector3 GetTargetPos();
    Vector3 SetSearchPos(Vector3 searchPos);
    Vector3 GetSearchPos();
    void SetState(State current);
    State GetState();

    Script_AnimationMgr GetAnim();
    List<Vector3> GetWaypointList();

    void IsTargetInsight(System.Boolean condition);
    void  IsSuspicious(System.Boolean condition);
    bool GetIsSuspicious();
    void SetSearchCount(int count);
    int GetSearchCount();

    bool GetIsTargetInSight();
    void SetCondition(System.Boolean condition);

    Transform SetTarget(Transform target);
    Transform GetTarget();

    NavMeshAgent GetAgentNavMesh();
    Transform GetAgentTransform();
    
    List<Transform> EnemyInSight();
}
