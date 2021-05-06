using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAction : MonoBehaviour
{
    public GameObject player;
    public GameObject spotLight;

    PlayerMovement mPlayer;
    public enum PlayerStatus { Stealth, Hide, Normal };
    public PlayerStatus mStatus;
    //public Light spotlight;

    public static PlayerAction instance;//
    // Start is called before the first frame update
    void Start()
    {
        mPlayer = player.GetComponent<PlayerMovement>();
        mStatus = PlayerStatus.Normal;
        instance = this;//    
    }

    // Update is called once per frame
    void Update()
    {
        ChangeStatus();
        StatusAction(mStatus);

        if (Input.GetKeyDown(KeyCode.T))
            Screen.fullScreen = !Screen.fullScreen; 

    }

    void ChangeStatus()
    {
        if (mStatus != PlayerStatus.Hide)
        {
            if (mPlayer.HoodieOn)
            {
                mStatus = PlayerStatus.Stealth;
            }
            else
            {
                mStatus = PlayerStatus.Normal;
            }
        }
    }

    void StatusAction(PlayerStatus status)
    {

        switch (status)
        {
            case PlayerStatus.Normal:
                {
                    Normal();
                    break;
                }
            case PlayerStatus.Hide:
                {
                    Hide();
                    break;
                }
            case PlayerStatus.Stealth:
                {
                    Stealth();
                    break;
                }
        }
        //Debug.Log(mStatus);
    }

    void Stealth()
    {
        mPlayer.charSpeed = 2.5f;
    }

    void Normal()
    {

        mPlayer.charSpeed = 5f;
    }

    void Hide()
    {
        mPlayer.charSpeed = 0f;
    }
}
