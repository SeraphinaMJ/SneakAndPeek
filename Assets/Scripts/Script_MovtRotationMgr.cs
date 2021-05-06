using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_MovtRotationMgr : MonoBehaviour
{
    public static Script_MovtRotationMgr instance;
    public enum DIRECTION { RIGHT, FORWARD, LEFT, BACK };
    public DIRECTION currentDirection;

    //Detect if character is going in correct way or opposite way
    private bool isThroughRight = false;
    private bool isThroughLeft = false;
    private bool isHeadingRight;

    private bool isRotating = false;
    public bool IsRotating { get { return isRotating; } set { isRotating = value; } }
    private Transform m_pivot;
    public Transform Pivot { get { return m_pivot; } set { m_pivot = value; } }

    private const string LeftBound = "LeftBound";
    private const string RightBound = "RightBound";
    // Start is called before the first frame update
    void Start()
    {
        //currentDirection = DIRECTION.RIGHT;
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
    }
    //Keeping track of current direction
    void DecidingRotationDirection()
    {
        if (PlayerMovement.instance.LookingDirection > 0)
        {
            switch (currentDirection)
            {
                case DIRECTION.RIGHT:
                    currentDirection = DIRECTION.FORWARD;
                    break;
                case DIRECTION.FORWARD:
                    currentDirection = DIRECTION.LEFT;
                    break;
                case DIRECTION.LEFT:
                    currentDirection = DIRECTION.BACK;
                    break;
                case DIRECTION.BACK:
                    currentDirection = DIRECTION.RIGHT;
                    break;
            }
        }
        else if (PlayerMovement.instance.LookingDirection < 0)
        {
            switch (currentDirection)
            {
                case DIRECTION.RIGHT:
                    currentDirection = DIRECTION.BACK;
                    break;
                case DIRECTION.FORWARD:
                    currentDirection = DIRECTION.RIGHT;
                    break;
                case DIRECTION.LEFT:
                    currentDirection = DIRECTION.FORWARD;
                    break;
                case DIRECTION.BACK:
                    currentDirection = DIRECTION.LEFT;
                    break;
            }
        }
    }
    //Prevent trigger from slightly touching the bound
    void OnTriggerEnter(Collider target)
    {
        if (PlayerMovement.instance.LookingDirection > 0)
        {
            isHeadingRight = true;
        }
        else if (PlayerMovement.instance.LookingDirection < 0)
        {
            isHeadingRight = false;
        }
    }
    //Doesn't work if entered direction and exited direction are different
    void OnTriggerExit(Collider target)
    {
        if ((PlayerMovement.instance.LookingDirection > 0 && isHeadingRight) || (PlayerMovement.instance.LookingDirection < 0 && !isHeadingRight))
        {

            if (target.gameObject.CompareTag(LeftBound))
            {
                //When getting out the rotate zone
                if (isThroughRight)
                {
                    DecidingRotationDirection();
                    isRotating = false;
                    isThroughLeft = false;
                    isThroughRight = false;
                    m_pivot = null;
                }
                //When getting in for the first time
                else
                {
                    isRotating = !isRotating;
                    isThroughLeft = !isThroughLeft;
                    m_pivot = target.gameObject.transform.parent.GetComponent<Transform>();
                }
            }
            else if (target.gameObject.CompareTag(RightBound))
            {
                if (isThroughLeft)
                {
                    DecidingRotationDirection();
                    isRotating = false;
                    isThroughLeft = false;
                    isThroughRight = false;
                    m_pivot = null;
                }
                else
                {
                    isRotating = !isRotating;
                    isThroughRight = !isThroughRight;
                    m_pivot = target.gameObject.transform.parent.GetComponent<Transform>();
                }
            }
        }
    }
}
