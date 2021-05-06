using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource Footstep_stealth;
    public AudioSource Footstep_Normal;

    public AudioSource Hiding;

    public AudioSource ItemGet;

    public AudioSource Keypad_beep;
    public AudioSource NearKeypad;

    public AudioSource Unlock;

    public AudioSource Cloth;
   
    public AudioSource BGM;

    public AudioSource CodeFail;
    public AudioSource CodeCorrect;

    public AudioSource SafeLocked;

    public AudioSource GuardWalk;
    public AudioSource GuardKey;
    public AudioSource Detect;

    public static AudioManager audio_instance;

    private GameObject NPC_Obj;
    private GameObject Player_Obj;
    private Script_AIController AI_controller;
    private float vol_modifier = 1;

    void Start()
    {
        audio_instance = this;

        Footstep_stealth.Stop();
        Footstep_Normal.Stop();
        Hiding.Stop();
        ItemGet.Stop();
        Keypad_beep.Stop();
        Unlock.Stop();
        Cloth.Stop();
        NearKeypad.Stop();
        //BGM.Stop();
        CodeFail.Stop();
        CodeCorrect.Stop();
        SafeLocked.Stop();
        Detect.Stop();

        NPC_Obj = GameObject.FindGameObjectWithTag("NPC");
        Player_Obj = GameObject.FindGameObjectWithTag("Player");

        if (NPC_Obj != null)
           AI_controller = NPC_Obj.GetComponent<Script_AIController>();
    }

    // Update is called once per frame
    void Update()
    {
        GuardSoundManagement();
    }

    public void GuardSoundManagement()
    {
        if (NPC_Obj != null)
        {
            float distance = Vector3.Distance(NPC_Obj.transform.position, Player_Obj.transform.position);
            float max_dist = 40f;
            float min_dist = 4f;
            float volume_max = 1;
            float total = max_dist - min_dist;
            float distance_y = Player_Obj.transform.position.y - NPC_Obj.transform.position.y;
            if (distance_y > Mathf.Abs(2.8f))
            {
                vol_modifier = 0f;
            }
            else
            {
                vol_modifier = 1f;
            }
            GuardWalk.volume = vol_modifier * (volume_max - (distance * (1.5f / total)));
            GuardKey.volume = (vol_modifier * (volume_max - (distance * (1.5f / total))))*0.3f;
            BGM.volume = 0.02f;
        }
    }

    public void PlayFootStepStealth()
    {
        Footstep_stealth.Play();
    }
    public void PlayFootStepNormal()
    {
        Footstep_Normal.Play();
    }
    public void StopFootStepStealth()
    {
        Footstep_stealth.Stop();
    }
    public void StopFootStepNormal()
    {
        Footstep_Normal.Stop();
    }

    public void Hide()
    {
        Hiding.Play();
    }
    public void GetItem()
    {
        ItemGet.Play();
    }
    public void KeypadBeep()
    {
        Keypad_beep.Play();
    }

    public void PlayUnlocking()
    {
        Unlock.Play();
        if (PlayerAction.instance.mStatus == PlayerAction.PlayerStatus.Stealth)
            Unlock.volume = 0.2f;
        else
            Unlock.volume = 1.0f;
    }
    public void StopUnlocking()
    {
        Unlock.Stop();
    }

    public void ChangeHoodie()
    {
        Cloth.Play();
        Cloth.volume = 0.3f;
    }

    public void NearKeyPad()
    {
        NearKeypad.Play();
        NearKeypad.volume = 0.5f;
    }

    public void PlayBGM()
    {
        BGM.Play();
    }

    public void PlayCodeFail()
    {
        CodeFail.Play();
    }
    public void PlayCodeCorrect()
    {
        CodeCorrect.Play();
    }
    public void StopCodeFail()
    {
        CodeFail.Stop();
    }
    public void StopCodeCorrect()
    {
        CodeCorrect.Stop();
    }
    public void PlaySafeLock()
    {
        SafeLocked.Play();
    }
    public void StopSafeLock()
    {
        SafeLocked.Stop();
    }

    public void PlayGuardWalk()
    {
        float distance = Vector3.Distance(NPC_Obj.transform.position, Player_Obj.transform.position);
        float volumeModifier = 0.033f;
        GuardWalk.volume = distance * volumeModifier;
        GuardWalk.Play();
    }
    public void PlayGuardKey()
    {
        float distance = Vector3.Distance(NPC_Obj.transform.position, Player_Obj.transform.position);
        float volumeModifier = 0.033f;
        GuardKey.volume = distance * volumeModifier;
        GuardKey.Play();
    }
    public void Detection()
    {
        Detect.Play();
    }
}
