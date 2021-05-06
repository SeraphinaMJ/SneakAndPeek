using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    [SerializeField]
    private float m_waitingTime = 10f;
    public float charSpeed { get { return m_charSpeed; } set { m_charSpeed = value; } }
    private float m_charSpeed;

    //To detect the way if it's going backwards
    public float LookingDirection { get { return m_lookingDirection; } set { m_lookingDirection = value; } }
    public float VerticalDirection { get { return m_verticalDirection; } set { m_verticalDirection = value; } }
    private float m_lookingDirection;
    private float m_verticalDirection;
    //deltatime for waiting
    private float m_dt;
    //true if animation is currently played
    private bool isHoodOnPlaying;
    private bool isHoodOffPlaying;
    public bool HoodieOn { get { return isHoodOn; } set { isHoodOn = value; } }

    [SerializeField]
    private bool isInsideBuilding = false;
    private bool isHoodOn;
    private bool isWaiting;
    private bool isWaitingPlaying;
    private bool isOnIdle;
    private bool isAttackPlaying;
    //x axis and y axis used on transforming location
    private float translation;
    private float straffe;
    // variable to keep character sprite renderer reference
    private SpriteRenderer m_characterSprite; 
    // variable to keep character sprite animator reference
    private Script_AnimationMgr m_anim;
    private Vector3 m_originalcolCenter;
    private float  m_originalcolHeight;
    private void Start()
    {
        instance = this;
        m_dt = 0;
        translation = 0f;
        straffe = 0f;
        isHoodOnPlaying = false;
        isHoodOn = false;
        isHoodOffPlaying = false;
        isOnIdle = false;
        isAttackPlaying = false;
        m_characterSprite = GetComponent<SpriteRenderer>();
        m_anim = GetComponent<Script_AnimationMgr>();
        m_originalcolCenter = GetComponent<CapsuleCollider>().center;
        m_originalcolHeight = GetComponent<CapsuleCollider>().height;
    }
    void Update()
    {
        /* update the character */
        InputUpdate();
        CharacterUpdate();
        DirectionMovementGuiding();
        IdleUpdate();
        HoodOnUpdate();
        WaitingUpdate();
    }

    void InputUpdate()
    {
        if (!isHoodOnPlaying && !isHoodOffPlaying &&
      !isAttackPlaying)
        {
            m_lookingDirection = Input.GetAxis("Horizontal") * m_charSpeed;
            m_verticalDirection = Input.GetAxis("Vertical") * m_charSpeed;
        }
        else
        {
            m_lookingDirection = 0f;
            m_verticalDirection = 0f;
        }
    }
    //Script_MovRotationMgr instance used
    void DirectionMovementGuiding()
    {
        Script_MovtRotationMgr rotationInstance = Script_MovtRotationMgr.instance;
        if (rotationInstance.IsRotating == false && (m_lookingDirection != 0 || m_verticalDirection != 0))
        {
            straffe = m_verticalDirection * Time.deltaTime;
            translation = m_lookingDirection * Time.deltaTime;
            transform.Translate(translation, 0f, straffe);

            switch (rotationInstance.currentDirection)
            {
                case Script_MovtRotationMgr.DIRECTION.RIGHT:
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    break;
                case Script_MovtRotationMgr.DIRECTION.FORWARD:
                    transform.eulerAngles = new Vector3(0, -90, 0);
                    break;
                case Script_MovtRotationMgr.DIRECTION.LEFT:
                    transform.eulerAngles = new Vector3(0, 180, 0);
                    break;
                case Script_MovtRotationMgr.DIRECTION.BACK:
                    transform.eulerAngles = new Vector3(0, 90, 0);
                    break;
            }
        }
        else if (rotationInstance.IsRotating == true)
        {
            Vector3 target = new Vector3 (rotationInstance.Pivot.transform.position.x,
                                          transform.position.y,
                                          rotationInstance.Pivot.transform.position.z);
            if (m_lookingDirection != 0)
            {
                const float DEGREE = 90f;
                float looking = (m_lookingDirection / Mathf.Abs(m_lookingDirection));
                float dist = Vector3.Distance(target,transform.position);
                transform.RotateAround(target, Vector3.down, (Time.deltaTime * DEGREE * looking * (m_charSpeed / (dist*2))));
                transform.LookAt(target);
            }
            if(m_verticalDirection != 0)
            {
                float looking = (m_verticalDirection / Mathf.Abs(m_verticalDirection));

                transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * looking * m_charSpeed);
            }     
        }
    }
    void IdleUpdate()
    {
        if (m_lookingDirection == 0 &&
            !isAttackPlaying &&
            !isHoodOnPlaying &&
            !isHoodOffPlaying)
        {
            isOnIdle = true;
        }
        else
        {
            isOnIdle = false;
        }
    }
    void HoodOnUpdate()
    {
        /* if the mouse left button is clicked */
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AudioManager.audio_instance.ChangeHoodie();
            /* if hood is not on, turn on hood */
            if (!isHoodOn && !isHoodOffPlaying)
            {
                isHoodOnPlaying = true;  
            }
            else if(isHoodOn && !isHoodOnPlaying)
            {
                isHoodOffPlaying = true;
            }
        }

        if (isHoodOnPlaying && !isHoodOffPlaying)
        {
            /* play first attack animation */
            m_anim.Play("HOODON", false);
            isHoodOn = true;
            if (isInsideBuilding)
            {
                this.GetComponent<CapsuleCollider>().height = m_originalcolHeight - m_originalcolHeight / 2f;
                this.GetComponent<CapsuleCollider>().center = new Vector3(m_originalcolCenter.x,
                   m_originalcolCenter.y - this.GetComponent<CapsuleCollider>().height/2,
                   m_originalcolCenter.z);
                
            }
        }
        else if (isHoodOffPlaying && !isHoodOnPlaying)
        {
            /* play first attack animation */
            m_anim.Play("HOODOFF", false);
            isHoodOn = false;
            if (isInsideBuilding)
            {
                this.GetComponent<CapsuleCollider>().center = new Vector3(m_originalcolCenter.x,
                   m_originalcolCenter.y,
                   m_originalcolCenter.z);
                this.GetComponent<CapsuleCollider>().height = m_originalcolHeight;
            }
        }
    }
    void WaitingUpdate()
    {
        if (isOnIdle)
        {
            //m_waitingTime += Time.fixedDeltaTime;
            m_dt += Time.fixedDeltaTime;
        }
        else
        {
            //m_waitingTime = -1f;
            m_dt = -1f;
        }

        if (m_waitingTime < m_dt)
        {
            isWaiting = true;
            isWaitingPlaying = true;
        }
        else if (m_dt < 0f)
        {
            isWaiting = false;
            m_dt = 0f;
        }
    }
    void CharacterUpdate()
    {
        /* if attack animation is not playing */
        if (!isAttackPlaying && !isHoodOnPlaying && !isHoodOffPlaying)
        {
            /* update the character position */
            MovementUpdate();
        }

        if (!isHoodOnPlaying && !isHoodOffPlaying)
        {
            /* update the attack logic */
            AttackUpdate();
        }
        if (!isAttackPlaying)
        {
            /* update hood on logic */
            HoodOnUpdate();
        }
    }
    void MovementUpdate()
    {

        /* if mouse position is right side of the character */
        if (0 < m_lookingDirection)
        {
            m_characterSprite.flipX = false;
        }
        /* if mouse position is left side of the character */
        else if (m_lookingDirection < 0)
        {
            m_characterSprite.flipX = true;
        }


        if (m_lookingDirection != 0 || m_verticalDirection!=0)
        {
            if (!isHoodOn)
            {
                /* play run animation */
                m_anim.Play("WALK");
            }
            else
            {
                /* play run animation */
                m_anim.Play("WALKHOODON");
            }
        }
        else if (!isWaiting)
        {
            if (!isHoodOn)
            {
                /* play idle animation */
                m_anim.Play("IDLE");
            }
            else
            {
                /* play run animation */
                m_anim.Play("IDLEHOODON");
            }
        }
        else
        {
            if (!isHoodOn)
            {
                if (isWaitingPlaying)
                {
                    /* play idle animation */
                    m_anim.Play("WAIT", false);
                    m_dt = 0f;
                }
                else
                {
                    m_anim.Play("WAITIDLE");
                    m_dt = 0f;
                }
            }
            else
            {
                if (isWaitingPlaying)
                {
                    /* play idle animation */
                    m_anim.Play("WAITHOODON", false);
                    m_dt = 0f;
                }
                else
                {
                    m_anim.Play("WAITHOODONIDLE");
                    m_dt = 0f;
                }
            }
        }
    }
    void AttackUpdate()
    {
        /* if the mouse left button is clicked */
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            /* if first attack animation is not playing */
            if (!isAttackPlaying)
            {
                isAttackPlaying = true;
            }
        }

        if (isAttackPlaying)
        {
            //character.GetComponent<Collider>().enabled = true;
            if (!isHoodOn)
            {
                /* play first attack animation */
                m_anim.Play("ATTACK", false);
            }
            else
            {
                m_anim.Play("ATTACKHOODON", false);
            }

        }
    }
    /* function to be called by sprite animator trigger */
    public void Trigger_DisableAttack()
    {
        isAttackPlaying = false;
        //character.GetComponent<Collider>().enabled = false;
    }

    /* function to be called by sprite animator trigger */
    public void Trigger_DisableHoodOn()
    {
        isHoodOnPlaying = false;
    }

    /* function to be called by sprite animator trigger */
    public void Trigger_DisableHoodOff()
    {
        isHoodOffPlaying = false;
    }

    /* function to be called by sprite animator trigger */
    public void Trigger_DisableWait()
    {
        isWaitingPlaying = false;
    }
}
