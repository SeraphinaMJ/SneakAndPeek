using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    // Start is called before the first frame update
    public bool hasKey;
    public bool hasDriver;
    public bool hasDiamond;
    public bool hasKeyCode;

    public bool nearKey;
    public bool nearDriver;
    public bool nearSafe;

    [Header("Tools")]
    private GameObject Key;
    private GameObject ScrewDriver;

    private ScrewDriver tool_screwDriver;

    [Header("Objects")]
    private GameObject Cabinet;
    private GameObject ScrewDriver_UI;
    private Cabinet pCabinet;

    private float distanceVal;

    public static PlayerItem instance;//

    private float xPos_Mid = Screen.width / 2;
    private float yPos_mid = Screen.height / 2;

    void Start()
    {
        Key = GameObject.FindGameObjectWithTag("Key");

        ScrewDriver = GameObject.FindGameObjectWithTag("ScrewDriver");
        if(ScrewDriver != null)
          tool_screwDriver = ScrewDriver.GetComponent<ScrewDriver>();

        Cabinet = GameObject.FindGameObjectWithTag("Cabinet");
        if(Cabinet != null)
           pCabinet = Cabinet.GetComponent<Cabinet>();

        ScrewDriver_UI = GameObject.FindGameObjectWithTag("ScrewDriverUI");

        distanceVal = 3.0f;

        hasKey = false;
        hasDriver = false;
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        GetItems();
        OpenCabinet();
    }

    void GetItems()
    {    
        if (Key != null)
        {
            float distance_Key = Vector3.Distance(Key.transform.position, this.transform.position);
            //Check distance between key and player to get it
            if (distance_Key <= distanceVal)
            {
                Debug.Log("key");
                if (hasKey)
                    nearKey = false;
                else
                    nearKey = true;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (!hasKey)
                        AudioManager.audio_instance.GetItem();
                    hasKey = true;
                }
            }
            else
            {
                nearKey = false;
            }
        }

        if (ScrewDriver != null)
        {
            float distance_ScrewDriver = Vector3.Distance(ScrewDriver.transform.position, this.transform.position);
            //Check distance between Screwdriver and player to get it
            if (distance_ScrewDriver <= distanceVal)
            {
                if (hasDriver)
                    nearDriver = false;
                else
                    nearDriver = true;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (!hasDriver)
                        AudioManager.audio_instance.GetItem();
                    hasDriver = true;
                }
            }
            else
            {
                nearDriver = false;
            }
        }       
    }

    void OpenCabinet()
    {
        if (Cabinet != null)
        {
            float distance_Cabinet = Vector3.Distance(Cabinet.transform.position, this.transform.position);

            if (distance_Cabinet <= distanceVal)
            {
                if (tool_screwDriver.readyToUse)
                {
                    nearSafe = true;
                }
                else
                {
                    ScrewDriver_UI.SetActive(true);
                    ScrewDriver_UI.transform.position = new Vector3(Cabinet.transform.position.x, Cabinet.transform.position.y + 1.5f, Cabinet.transform.position.z);
                    ScrewDriver_UI.transform.localScale = new Vector3(0.2f, 0.2f, 1);
                    nearSafe = false;
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (tool_screwDriver.readyToUse && !pCabinet.isUsed)
                    {
                        pCabinet.readyToOpen = true;
                    }
                    else
                    {
                        if (!AudioManager.audio_instance.SafeLocked.isPlaying)
                        {
                            AudioManager.audio_instance.PlaySafeLock();
                        }
                    }
                }
            }
            else
            {
                nearSafe = false;
                ScrewDriver_UI.SetActive(false);
            }

            if (pCabinet.isUsed)
            {
                nearSafe = false;
                ScrewDriver_UI.SetActive(false);
            }
        }
    }
    private void OnGUI()
    {
        if (nearKey)
        {
            GUI.skin.box.fontSize = 15;
            GUI.skin.box.fontStyle = FontStyle.Bold;
            GUI.Box(new Rect(xPos_Mid, yPos_mid, 150, 50), "Press 'E' to Get Key ");
        }

        if (nearDriver)
        {
            GUI.skin.box.fontSize = 15;
            GUI.skin.box.fontStyle = FontStyle.Bold;
            GUI.Box(new Rect(xPos_Mid, yPos_mid, 200, 50), "Press 'E' to Get Driver ");
        }

        if (nearSafe)
        {
            GUI.skin.box.fontSize = 15;
            GUI.skin.box.fontStyle = FontStyle.Bold;
            GUI.color = Color.red;
            GUI.Box(new Rect(xPos_Mid, yPos_mid, 200, 50), "Press 'E' to Open Safe ");
        }
    }
}
