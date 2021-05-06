using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

public class SoundUI : MonoBehaviour
{
    public bool isSoundOn;
    public int pStatus;

    GameObject player;
    GameObject gauge;

    private ProgressBar pBar;
    private ParticleSystem particle;
    private PlayerAction pAction;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gauge = GameObject.FindGameObjectWithTag("ProgressUI");
        pBar = gauge.GetComponent<ProgressBar>();

        particle = this.GetComponent<ParticleSystem>();
        pAction = player.GetComponent<PlayerAction>();

    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1.5f, player.transform.position.z);

        if (pBar.isStealingOn)
        {
            isSoundOn = true;
            if (pAction.mStatus == PlayerAction.PlayerStatus.Normal)
            {
                particle.startColor = Color.green;
                particle.startLifetime = 2;
            }
            if (pAction.mStatus == PlayerAction.PlayerStatus.Stealth)
            {
                particle.startColor = Color.red;
                particle.startLifetime = 1;

            }
        }
        else
            isSoundOn = false;


    }
}
