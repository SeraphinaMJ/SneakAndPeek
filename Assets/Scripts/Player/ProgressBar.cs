using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class ProgressBar : MonoBehaviour
{
    private GameObject player;

    private GameObject cabinet;
    private GameObject soundUI;

    private Slider slider;
    private PlayerAction pAction;
    private Cabinet pCabinet;
    private PlayerItem pItem;

    public float fill;
    public float fillSpeed;
    private float distance;

    public bool isStealingOn = false;
    public bool isStealed = false;

    private float dist_val;
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
        player = GameObject.FindGameObjectWithTag("Player");
        pAction = player.GetComponent<PlayerAction>();

        cabinet = GameObject.FindGameObjectWithTag("Cabinet");
        pCabinet = cabinet.GetComponent<Cabinet>();

        soundUI = GameObject.FindGameObjectWithTag("SoundEffect");
        soundUI.GetComponent<SoundUI>();

        pItem = player.GetComponent<PlayerItem>();

        fill = 0;
        slider.value = 0;
        fillSpeed = 0.1f;

        dist_val = 2.5f;
    }

    // Update is called once per frame
    void Update()
    {
        GaugeBar();
    }

    void GaugeBar()
    {
        slider.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1.5f, player.transform.position.z);

        distance = Vector3.Distance(cabinet.transform.position, pAction.transform.position);

        if (distance <= dist_val && (pCabinet.readyToOpen) == true && !(pCabinet.isUsed))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                isStealingOn = true;
            }
        }

        if (isStealingOn)
        {
            slider.transform.localScale = new Vector3(1.2f, 1.2f, 1);
            soundUI.SetActive(true);
            if (pAction.mStatus == PlayerAction.PlayerStatus.Normal)
            {
                soundUI.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                fillSpeed = 0.3f;
            }
            if (pAction.mStatus == PlayerAction.PlayerStatus.Stealth)
            {
                soundUI.transform.localScale = new Vector3(0.6f, 0.6f, 1.2f);
                fillSpeed = 0.15f;
            }
            ProgressBarTimer();
            PlayUnlockingSound();
        }
        else
        {
            slider.transform.localScale = new Vector3(0, 0, 0);
            fill = 0;
            slider.value = 0;
            soundUI.SetActive(false);
        }
    }

    void ProgressBarTimer()
    {
        distance = Vector3.Distance(pCabinet.transform.position, pAction.transform.position);

        if (distance > dist_val || Input.GetKeyDown(KeyCode.W))
        {
            isStealingOn = false;
            isStealed = false;
        }

        fill += Time.deltaTime * fillSpeed;
        slider.value = fill;

        if (slider.value >= 1)
        {
            isStealingOn = false;
            isStealed = true;
            pCabinet.isUsed = true;
            pItem.hasDiamond = true;
            pItem.hasKeyCode = true;
        }
    }

    void PlayUnlockingSound()
    {
        if (isStealingOn)
        {
            if (!AudioManager.audio_instance.Unlock.isPlaying)
            {
                AudioManager.audio_instance.PlayUnlocking();
            }
        }
        else
        {
            AudioManager.audio_instance.StopUnlocking();
        }

        if (isStealed)
        {
            AudioManager.audio_instance.GetItem();
        }
    }


}
