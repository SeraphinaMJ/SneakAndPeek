using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

public class MoveSoundUI : MonoBehaviour
{
    private GameObject player;
    public Transform PlayerInfo { get { return player.transform; } }
    private PlayerAction pAction;
    private ParticleSystem particle;

    public enum MovementSoundStatus { Little, Middle, High, None };

    public MovementSoundStatus MovementStatus { get { return mMovementStatus; } set { mMovementStatus = value; } }
    MovementSoundStatus mMovementStatus;

    private bool isMoving;
    public static MoveSoundUI instance;

    void Start()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        pAction = player.GetComponent<PlayerAction>();

        particle = this.GetComponent<ParticleSystem>();
        isMoving = false;
    }

    [System.Obsolete]
    void Update()
    {
        this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 1.5f, player.transform.position.z);

        CheckPlayerStatus();
        ChangeSoundStatus();
        MoveSound();

    }

    void CheckPlayerStatus()
    {
        float lookDir = PlayerMovement.instance.LookingDirection;
        float vertDir = PlayerMovement.instance.VerticalDirection;

        if (lookDir == 0f && vertDir == 0f)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        if (!isMoving)
        {
            if (pAction.mStatus == PlayerAction.PlayerStatus.Normal)
            {
                mMovementStatus = MovementSoundStatus.Middle;
            }
            else if (pAction.mStatus == PlayerAction.PlayerStatus.Stealth)
            {
                mMovementStatus = MovementSoundStatus.Little;
            }
            else if (pAction.mStatus == PlayerAction.PlayerStatus.Hide)
            {
                mMovementStatus = MovementSoundStatus.None;
            }

        }
        else
        {
            mMovementStatus = MovementSoundStatus.None;
        }
    }

    [System.Obsolete]
    void ChangeSoundStatus()
    {
        if (mMovementStatus == MovementSoundStatus.Little)
        {
            this.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            particle.startColor = Color.blue;
            particle.startSize = 15;
            particle.startLifetime = 1.0f;
            // AudioManager.audio_instance.FootStepStealth();
        }
        else if (mMovementStatus == MovementSoundStatus.Middle)
        {
            this.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            particle.startColor = Color.white;
            particle.startSize = 20;
            particle.startLifetime = 1.2f;
        }
        else if (mMovementStatus == MovementSoundStatus.High)
        {
            this.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            particle.startColor = Color.white;
            particle.startSize = 20;
            particle.startLifetime = 1.2f;
            //  AudioManager.audio_instance.FootStepNormal();
        }
        else if (mMovementStatus == MovementSoundStatus.None)
        {
            this.transform.localScale = new Vector3(0, 0, 0);
        }
    }

    void MoveSound()
    {
        if (!isMoving)
        {
            if (pAction.mStatus == PlayerAction.PlayerStatus.Stealth)
            {
                if (!AudioManager.audio_instance.Footstep_stealth.isPlaying)
                {
                    AudioManager.audio_instance.PlayFootStepStealth();
                }
            }
            else if (pAction.mStatus == PlayerAction.PlayerStatus.Normal)
            {
                if (!AudioManager.audio_instance.Footstep_Normal.isPlaying)
                {
                    AudioManager.audio_instance.PlayFootStepNormal();
                }
            }
        }
        else
        {
            AudioManager.audio_instance.StopFootStepNormal();
            AudioManager.audio_instance.StopFootStepStealth();
        }

        if (pAction.mStatus == PlayerAction.PlayerStatus.Hide)
        {
            AudioManager.audio_instance.StopFootStepNormal();
            AudioManager.audio_instance.StopFootStepStealth();
        }
    }
}
