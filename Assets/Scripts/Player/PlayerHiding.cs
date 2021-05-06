using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerHiding : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject[] hideObjs;
    private GameObject player;
    private GameObject E_UI;
    private GameObject Hide_UI;
    private GameObject Arrow_UI;

    private PlayerAction pAction;
    private float dist;

    private Vector3 prevPos;

    private bool isOnHiding;
    private int statusNumber;
    private int prevStatus;

    private List<GameObject> ArrowUIs;
    private List<bool> isNearHideObj;

    private float xPos_Mid = Screen.width / 2;
    private float yPos_mid = Screen.height / 2;

    void Start()
    {
        hideObjs = GameObject.FindGameObjectsWithTag("Object_Hide");
        player = GameObject.FindGameObjectWithTag("Player");
        pAction = player.GetComponent<PlayerAction>();

        E_UI = GameObject.FindGameObjectWithTag("ActionKey");
        Hide_UI = GameObject.FindGameObjectWithTag("HideUI");

        Arrow_UI = GameObject.FindGameObjectWithTag("ArrowUI");

        dist = 3f;
        isOnHiding = false;

        ArrowUIs = new List<GameObject>();
        isNearHideObj = new List<bool>();

        for (int i = 0; i < hideObjs.Length; i++)
        {
            ArrowUIs.Add(Instantiate(Arrow_UI));
            isNearHideObj.Add(false);
        }

        for (int i = 0; i < hideObjs.Length; i++)
        {
            if (i == 1 || i == 2)
            {
                ArrowUIs[i].transform.Rotate(0, -90, 0);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerStatusChange();
        HidingPlayer();
    }

    private void HidingPlayer()
    {
        for (int i = 0; i < hideObjs.Length; i++)
        {
            if (Vector3.Distance(hideObjs[i].transform.position, player.transform.position) < dist)
            {
                isNearHideObj[i] = true;
                if (isOnHiding)
                {
                    isNearHideObj[i] = false;
                    ArrowUI(i);
                }
                else
                {
                    ArrowUIs[i].SetActive(false);
                }
                HideAction(i);
            }
            else
            {
                isNearHideObj[i] = false;
                ArrowUIs[i].SetActive(false);
            }
        }
    }

    private void ArrowUI(int num)
    {
        ArrowUIs[num].SetActive(true);
        ArrowUIs[num].transform.position = new Vector3(hideObjs[num].transform.position.x, hideObjs[num].transform.position.y + 2.5f, hideObjs[num].transform.position.z);
        ArrowUIs[num].transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
    }

    private void HideAction(int num)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isOnHiding)
            {
                isOnHiding = true;
                player.GetComponent<NavMeshAgent>().enabled = false;
                prevPos = player.transform.position;
                player.transform.position = hideObjs[num].transform.position;
                prevStatus = statusNumber;
                pAction.mStatus = PlayerAction.PlayerStatus.Hide;
                AudioManager.audio_instance.Hide();
            }
            else
            {
                isOnHiding = false;
                player.transform.position = prevPos;
                StatusChange(prevStatus);
                player.GetComponent<NavMeshAgent>().enabled = true;
                AudioManager.audio_instance.Hide();
            }
        }
    }

    private void OnGUI()
    {
        for (int i = 0; i < isNearHideObj.Count; i++)
        {
            if (isNearHideObj[i] == true)
            {
                GUI.skin.box.fontSize = 15;
                GUI.skin.box.fontStyle = FontStyle.Bold;
                GUI.Box(new Rect(xPos_Mid, yPos_mid, 150, 50), "Press 'E' to Hide ");
            }
        }
    }

    private void PlayerStatusChange()
    {
        if (pAction.mStatus == PlayerAction.PlayerStatus.Normal)
            statusNumber = 0;
        else if (pAction.mStatus == PlayerAction.PlayerStatus.Stealth)
            statusNumber = 1;
        else
            statusNumber = 2;
    }

    private void StatusChange(int num)
    {
        if (num == 0)
            pAction.mStatus = PlayerAction.PlayerStatus.Normal;
        else if (num == 1)
            pAction.mStatus = PlayerAction.PlayerStatus.Stealth;
        else if (num == 2)
            pAction.mStatus = PlayerAction.PlayerStatus.Hide;
    }

}
