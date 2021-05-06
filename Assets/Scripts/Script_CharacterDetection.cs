using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Script_CharacterDetection : MonoBehaviour
{
    public static Script_CharacterDetection instance;
    [SerializeField]
    private float m_viewRad = 0.0f;
    public float  ViewRadius{ get { return m_viewRad; } set { m_viewRad = value; } }
    [SerializeField]
    [Range(0,360)]
    private float m_viewAngle = 0.0f;
    public float ViewAngle { get { return m_viewAngle; } set { m_viewAngle = value; } }

    [SerializeField]
    private int edgeResolveItr = 4;
    [SerializeField]
    private float edgeDistThreshold = 0.5f;

    [SerializeField]
    [Range(0, 100)]
    private float meshResolution = 0;

    [SerializeField]
    //private MeshFilter m_viewMeshFilter = null;
    private GameObject m_viewMeshFilter = null;
    private Mesh m_viewMesh;

    [SerializeField]
    private LayerMask m_targetMask = 0;
    [SerializeField]
    private LayerMask m_obstacleMask = 0;
    public LayerMask ObstacleMask { get { return m_obstacleMask; } set { m_obstacleMask = value; } }

    [HideInInspector]
    public List<Transform> m_visibleTargets;

    void Start()
    {
        instance = this;
        m_viewMesh = new Mesh();
        m_viewMesh.name = "View Mesh";
        m_viewMeshFilter.GetComponent<MeshFilter>().mesh = m_viewMesh;

        StartCoroutine("FindTargetsWithDelay",.2f);
        StartCoroutine("DeleteWaypointsWithDelay", .2f);
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while(true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    IEnumerator DeleteWaypointsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            DeleteVisibleWaypoints();
        }
    }

    void LateUpdate()
    {
        DrawFOV();
    }

    void FindVisibleTargets()
    {
        m_visibleTargets.Clear();
        Collider[] targetsInRadius = Physics.OverlapSphere(transform.position, m_viewRad, m_targetMask);
        for (int i = 0; i < targetsInRadius.Length; i++)
        {
            Transform target = targetsInRadius[i].transform;
            Vector3 dirToTarget = (target.GetComponent<Collider>().bounds.center - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < m_viewAngle / 2f)
            {
                float distToTarget = Vector3.Distance(transform.position, target.GetComponent<Collider>().bounds.center);
                if (!Physics.Raycast(transform.position, dirToTarget, distToTarget ,m_obstacleMask))
                {
                    m_visibleTargets.Add(target);
                }
            }
        }
    }

    void DeleteVisibleWaypoints()
    {

        for (int i = 0; i < Script_AIController.instance.m_wayPoints.Count; i++)
        {
            Vector3 dirToTarget = (Script_AIController.instance.m_wayPoints.ElementAt(i) - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < m_viewAngle / 2f)
            {
                float distToTarget = Vector3.Distance(transform.position, Script_AIController.instance.m_wayPoints.ElementAt(i));
                if (!Physics.Raycast(transform.position, dirToTarget, distToTarget, m_obstacleMask))
                {
                    Script_AIController.instance.m_wayPoints.RemoveAt(i);
                }
            }
        }
    }

    public Vector3 DirFromAngle(float angleInDeg,bool isGlobalAngle)
    {
        if (!isGlobalAngle)
            angleInDeg += transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(angleInDeg*Mathf.Deg2Rad),0, Mathf.Cos(angleInDeg * Mathf.Deg2Rad));
    }

    void DrawFOV()
    {
        int stepCount = Mathf.RoundToInt(m_viewAngle * meshResolution);
        float stepAngleSize = m_viewAngle / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();
        ViewCastInfo oldViewCast = new ViewCastInfo();
        for (int i = 0; i <= stepCount; i++)
        {
            float angle = transform.eulerAngles.y - m_viewAngle / 2f + stepAngleSize * i;
            ViewCastInfo newViewCast = Viewcast(angle);
            //Debug.DrawLine(transform.position, transform.position + DirFromAngle(angle, true)*m_viewRad,Color.yellow);
            if(i>0)
            {
                bool isExceededThreshold = Mathf.Abs(oldViewCast.dist - newViewCast.dist) > edgeDistThreshold;
                if (oldViewCast.hit != newViewCast.hit || (isExceededThreshold && oldViewCast.hit && newViewCast.hit))
                {
                    EdgeInfo edge = FindEdge(oldViewCast, newViewCast);
                    if (edge.pointA != Vector3.zero)
                        viewPoints.Add(edge.pointA);
                    if (edge.pointB != Vector3.zero)
                        viewPoints.Add(edge.pointB);
                }
            }
            viewPoints.Add(newViewCast.point);
            oldViewCast = newViewCast;
        }
        int vertCount = viewPoints.Count + 1;
        Vector3[] vert = new Vector3[vertCount];
        int[] triangles = new int[(vertCount - 2) * 3];

        vert[0] = Vector3.zero;
        for (int i = 0; i < vertCount - 1; i++)
        {
            vert[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

            if (i < vertCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }
        m_viewMesh.Clear();
        m_viewMesh.vertices = vert;
        m_viewMesh.triangles = triangles;
        m_viewMesh.RecalculateNormals();
        if(m_visibleTargets.Count!=0)
        {
            bool isPlayerDetected = false;
            foreach (Transform VisibleTargets in m_visibleTargets)
            {
                if (VisibleTargets.tag == "Player")
                {
                    isPlayerDetected = true;
                    transform.GetChild(1).gameObject.SetActive(true);
                    transform.GetChild(0).gameObject.SetActive(false);
                    m_viewMeshFilter.GetComponent<MeshRenderer>().material.color = new Color(0.962f, 0.186f, 0.186f, 0.381f);
                }
                else if(!isPlayerDetected)
                {
                    transform.GetChild(0).gameObject.SetActive(true);
                    transform.GetChild(1).gameObject.SetActive(false);
                    m_viewMeshFilter.GetComponent<MeshRenderer>().material.color = new Color(0.826f, 0.801f, 0.126f, 0.381f);
                }
            }
            isPlayerDetected = false;
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            m_viewMeshFilter.GetComponent<MeshRenderer>().material.color = new Color(0.211f, 0.9835f, 1f, 0.388f);
        }
    }

    EdgeInfo FindEdge(ViewCastInfo min,ViewCastInfo max)
    {
        float minAngle= min.angle;
        float maxAngle = max.angle;
        Vector3 minPoint = Vector3.zero;
        Vector3 maxPoint = Vector3.zero;

        for(int i =0; i<edgeResolveItr;i++)
        {
            float angle = (minAngle + maxAngle) / 2;
            ViewCastInfo newViewCast = Viewcast(angle);

            bool isExceededThreshold = Mathf.Abs(min.dist - newViewCast.dist) > edgeDistThreshold;
            if (newViewCast.hit == min.hit && !isExceededThreshold)
            {
                minAngle = angle;
                minPoint = newViewCast.point;
            }
            else
            {
                maxAngle = angle;
                maxPoint = newViewCast.point;
            }
        }
        return new EdgeInfo(minPoint, maxPoint);
    }
    ViewCastInfo Viewcast(float globalAngle)
    {
        Vector3 dir  = DirFromAngle(globalAngle, true);
        RaycastHit hit;
        if(Physics.Raycast(transform.position,dir,out hit, m_viewRad, m_obstacleMask))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            return new ViewCastInfo(false, transform.position+dir* m_viewRad, m_viewRad, globalAngle);
        }
    }

    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float dist;
        public float angle;

        public ViewCastInfo(bool _hit,Vector3 _point, float _dist,float _angle)
        {
            hit = _hit;
            point = _point;
            dist = _dist;
            angle = _angle;
        }
    }
     public struct EdgeInfo
    {
        public Vector3 pointA;
        public Vector3 pointB;

        public EdgeInfo(Vector3 _pointA, Vector3 _pointB)

        {
            pointA = _pointA;
            pointB = _pointB;
        }
    }

}
