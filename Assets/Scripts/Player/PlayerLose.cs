using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLose : MonoBehaviour
{

    public List<bool> isCaughtByNPC;

    public bool isRestart;

    private GameObject[] NPC;
    private GameObject player;
    private GameObject warning1;
    private GameObject warning2;

    public Material mat1;
    public Material mat2;

    private Renderer rend1;
    private Renderer rend2;

    private List<GameObject> NPC_List;
    private List<Script_AIController> AI_List;

    private float xPos_Mid = Screen.width / 2;
    private float yPos_mid = Screen.height / 2;

    private float curTime = 0;
    private bool isCheatOn;

    void Start()
    {
        NPC = GameObject.FindGameObjectsWithTag("NPC");
        player = GameObject.FindGameObjectWithTag("Player");
        warning1 = GameObject.FindGameObjectWithTag("sign1");
        warning2 = GameObject.FindGameObjectWithTag("sign2");

        rend1 = warning1.GetComponent<Renderer>();
        rend2 = warning2.GetComponent<Renderer>();

        NPC_List = new List<GameObject>();
        AI_List = new List<Script_AIController>();

        isDetectByNPC = new List<bool>();
        isCaughtByNPC = new List<bool>();
       
        for (int i = 0; i < NPC.Length; i++)
        {
            NPC_List.Add(NPC[i]);   
        }

        for(int i = 0; i < NPC.Length; i++)
        {
            AI_List.Add(NPC_List[i].GetComponent<Script_AIController>());
        }

        for(int i = 0; i < NPC_List.Count; i++)
        {
            isDetectByNPC.Add(false);
            isCaughtByNPC.Add(false);
        }

        isRestart = false;
        isCheatOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheatOnOff();
        if (!isCheatOn)
        {
            CheckStatus();
        }
        for(int i = 0; i < AI_List.Count; i++)
        {
            Debug.Log("NPC detection" + i + isDetectByNPC[i]);
            Debug.Log("NPC caught" + i + isCaughtByNPC[i]);
        }
    }

    void CheatOnOff()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isCheatOn)
            {                          
                isCheatOn = false;
                warning1.transform.localScale = new Vector3(0.072f, 0.323f, 0.072f);
                warning2.transform.localScale = new Vector3(0.072f, 0.072f, 0.072f);
                for (int i = 0; i < AI_List.Count; i++)
                {
                    AI_List[i].isTargetInsight = false;
                    AI_List[i].m_isPlayerCaught = false;              
                }
            }
            else
            {
                isCheatOn = true;
                warning1.transform.localScale = new Vector3(0, 0, 0);
                warning2.transform.localScale = new Vector3(0, 0, 0);
            }
        }
    }

    void CheckStatus()
    {
        for (int i = 0; i < AI_List.Count; i++)
        {
            isDetectByNPC[i] = AI_List[i].isTargetInsight;
            isCaughtByNPC[i] = AI_List[i].m_isPlayerCaught;
       
            Vector3 playerPos = player.transform.position;
            Vector3 signPos1 = new Vector3(playerPos.x, playerPos.y + 1.8f, playerPos.z);
            Vector3 signPos2 = new Vector3(playerPos.x, playerPos.y + 1.5f, playerPos.z);

            if (isDetectByNPC[i] && !(isCaughtByNPC[i]))
            {
                warning1.SetActive(true);
                warning2.SetActive(true);

                warning1.transform.position = signPos1;
                warning2.transform.position = signPos2;

                rend1.material = mat1;
                rend2.material = mat1;
            }
            else if (isCaughtByNPC[i])
            {
                warning1.SetActive(true);
                warning2.SetActive(true);

                warning1.transform.position = signPos1;
                warning2.transform.position = signPos2;

                rend1.material = mat2;
                rend2.material = mat2;

                isRestart = true;

            }
            else if (!(isCaughtByNPC[i]) && !(isDetectByNPC[i]))
            {
                warning1.SetActive(false);
                warning2.SetActive(false);
            }

            if (isRestart)
            {
                PlayerMovement.instance.charSpeed = 0;
                Timer();
            }
        }
    }

    void Timer()
    {
        curTime += Time.deltaTime;
        if(curTime >= 2.5f)
        {                   
            if (Entrance.prevLV == Entrance.PreviousLV.Building)
            {
                isRestart = false;
                SceneManager.LoadScene("Level_Building");
               
            }
            if (Entrance.prevLV == Entrance.PreviousLV.Bank)
            {
                isRestart = false;
                SceneManager.LoadScene("Level_Bank");             
            }        
        }
    }

    void OnGUI()
    {
        if (!isCheatOn)
        {
            for (int i = 0; i < AI_List.Count; i++)
            {
                if (isDetectByNPC[i] && !(isCaughtByNPC[i]))
                {
                    GUI.skin.box.fontSize = 20;
                    GUI.color = Color.yellow;
                    GUI.Box(new Rect(xPos_Mid * 0.8f, (yPos_mid * 2) * 0.93f, 350, 40), " Be careful for Guards! ");
                }
                else if (isCaughtByNPC[i] && isRestart)
                {
                    GUI.skin.box.fontSize = 40;
                    GUI.color = Color.red;
                    GUI.Box(new Rect(xPos_Mid * 0.8f, (yPos_mid * 2) * 0.88f, 600, 60), " YOU ARE CAUGHT !! ");
                }
            }
        }
        else
        {
            GUI.skin.box.fontSize = 20;
            GUI.color = Color.yellow;
            GUI.Box(new Rect(xPos_Mid * 0.8f, (yPos_mid * 2) * 0.88f, 600, 60), " Cheat on : Guard can't catch you ");
        }
    }

}
