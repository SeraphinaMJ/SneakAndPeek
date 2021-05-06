using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Script_AIController : MonoBehaviour, BT_AIBehavior
{
    State m_current;
    public static Script_AIController instance;
    public Vector3 m_targetPos;
    public Vector3 m_SearchPos;
    public Transform m_target;
    public MoveSoundUI m_soundCheck;
    public bool m_isPlayerCaught = false;
    public bool isTargetInsight { get { return m_isTargetInSight; } set { m_isTargetInSight = value; } }
    private bool m_isTargetInSight = false;
    public bool m_isSuspicious = false;
    public int m_SearchCount = 0;
    //behaviors
    public BT_Selector m_rootAI;

    public BT_Sequence m_catchSequence;
    public BT_Sequence m_detectionSequence;
    public BT_Sequence m_moveSequence;
    public BT_Sequence m_idleSequence;

    public BT_Inverter invertDetection;
    public BT_Inverter invertCatch;
    [SerializeField]
    private Script_CharacterDetection m_fov = null;
    const float ARRIVAL_RANGE = 2f;
    const float CATCH_RANGE = 2.0f;
    const float SEARCH_RANGE = 10f;

    public List<Vector3> m_wayPoints;
    // variable to keep character sprite animator reference
    private Script_AnimationMgr m_anim;
    // Start is called before the first frame update
    void Start()
    {
        m_anim = GetComponentInParent<Script_AnimationMgr>();
        instance = this;
        invertDetection = new BT_Inverter(new BT_Detect(this, m_fov.m_visibleTargets, m_soundCheck));
        invertCatch = new BT_Inverter(new BT_Catch(this,ARRIVAL_RANGE));
        m_targetPos = transform.position;
        m_target = transform;

        m_idleSequence = new BT_Sequence(new List<Script_BTNode>
        {
            new BT_Idle(this,transform),
            new BT_Wander(this,SEARCH_RANGE),
        });

        m_catchSequence = new BT_Sequence(new List<Script_BTNode>
        {
            new BT_Catch(this,CATCH_RANGE),
        });

        m_detectionSequence = new BT_Sequence(new List<Script_BTNode>
        {
           invertDetection,
           new BT_CheckArrival(this,ARRIVAL_RANGE),
           m_idleSequence
        });

        m_moveSequence = new BT_Sequence(new List<Script_BTNode>
        {
            new BT_Approach(this),    
        });

        m_rootAI = new BT_Selector(new List<Script_BTNode>
        {
            m_detectionSequence,
            m_catchSequence,
            m_moveSequence, 
        });

        new BT_Wander(this, SEARCH_RANGE);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(transform.position,m_targetPos);
        m_rootAI.Evaluate();
    }
       
    void OnDrawGizmos()
    {
        foreach (Vector3 waypoints in m_wayPoints)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(waypoints, 0.05f);
        }
    }
        public Vector3 SetTargetPos(Vector3 targetPos)
    {
        m_targetPos = targetPos;
        return m_targetPos;
    }

    public Vector3 GetTargetPos()
    {
        return m_targetPos;
    }

    public Vector3 SetSearchPos(Vector3 searchPos)
    {
        m_SearchPos = searchPos;
        return m_SearchPos;
    }
    public Vector3 GetSearchPos()
    {
        return m_SearchPos;
    }
    public void SetState(State current)
    {
        m_current = current;
    }
    public State GetState()
    {
        return m_current;
    }
    public Script_AnimationMgr GetAnim()
    {
        return m_anim;
    }
    public List<Vector3> GetWaypointList()
    {
        return m_wayPoints;
    }
    public void IsTargetInsight(System.Boolean condition)
    {
        m_isTargetInSight = condition;
    }
    public void IsSuspicious(System.Boolean condition)
    {
        m_isSuspicious = condition;
    }
    public bool GetIsSuspicious()
    {
        return m_isSuspicious;
    }
    public void SetSearchCount(int count)
    {
        m_SearchCount = count;
    }
    public int GetSearchCount()
    {
        return m_SearchCount;
    }

    public bool GetIsTargetInSight()
    {
        return m_isTargetInSight;
    }
    public void SetCondition(System.Boolean condition)
    {
        m_isPlayerCaught = condition;
    }

    public Transform SetTarget(Transform target)
    {
        m_target = target;
        return m_target;
    }
    public Transform GetTarget()
    {
        return m_target;
    }

    public NavMeshAgent GetAgentNavMesh()
    {
        return this.GetComponent<NavMeshAgent>();
    }

    public Transform GetAgentTransform()
    {
        return transform;
    }

    public List<Transform> EnemyInSight()
    {
        return m_fov.m_visibleTargets;
    }
}
