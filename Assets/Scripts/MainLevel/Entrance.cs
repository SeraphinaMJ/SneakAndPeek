using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Entrance : MonoBehaviour
{
    public enum PreviousLV { Main, Shoes, Building, Bank, Subway };
    public static PreviousLV prevLV;

    static bool lvStarted;

    private GameObject player;

    private GameObject entrance;
    private GameObject ET_Square;
    private GameObject ET_Light;

    private Renderer rend;
    private bool readyToEnter;

    private float xPos_Mid = Screen.width / 2;
    private float yPos_mid = Screen.height / 2;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        entrance = GameObject.FindGameObjectWithTag("Entrance");
        ET_Square = GameObject.FindGameObjectWithTag("Entrance_Square");
        ET_Light = GameObject.FindGameObjectWithTag("EntranceLight");

        rend = ET_Square.GetComponent<Renderer>();
        if (!lvStarted)
        {
            prevLV = PreviousLV.Main;
        }
    }

    // Update is called once per frame
    void Update()
    {
        ChangeEntrancePos();
        ReadyToEnter();
    }

    public void ReadyToEnter()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 entrance = ET_Square.transform.position;

        rend.material.color = Color.blue;

        if(Vector3.Distance(playerPos, entrance) < 1.0f)
        {
            readyToEnter = true;
            rend.material.color = Color.yellow;
            ET_Light.SetActive(true);
            if(Input.GetKeyDown(KeyCode.E))
            {
                lvStarted = true;
                if (prevLV == PreviousLV.Main)
                {
                    prevLV = PreviousLV.Shoes;
                    SceneManager.LoadScene("Level_Turorial");
                }
                else if(prevLV == PreviousLV.Shoes)
                {
                    prevLV = PreviousLV.Building;
                    SceneManager.LoadScene("Level_Building");
                }
                else if(prevLV == PreviousLV.Building)
                {
                    prevLV = PreviousLV.Bank;
                    SceneManager.LoadScene("Level_Bank");
                }
                else if(prevLV == PreviousLV.Bank)
                {
                    prevLV = PreviousLV.Subway;
                    SceneManager.LoadScene("Level_Train");
                }
            }
        }
        else
        {
            readyToEnter = false;
            ET_Light.SetActive(false);
        }
    }

    public void ChangeEntrancePos()
    {
        if (prevLV == PreviousLV.Main)
        {
            entrance.transform.position = new Vector3(44.05f, 0.502f, 11.38f);
        }
        else if (prevLV == PreviousLV.Shoes)
        {
            entrance.transform.position = new Vector3(57.291f,0.502f, -16.65f);
        }
        else if (prevLV == PreviousLV.Building)
        {
            entrance.transform.position = new Vector3(74.296f, 0.502f, 12.55f);
        }
        else if (prevLV == PreviousLV.Bank)
        {
            entrance.transform.position = new Vector3(77.56f, 0.502f, 4.64f);
        }  
    }

    public void OnGUI()
    {
        if (readyToEnter)
        {
            GUI.skin.box.fontSize = 15;
            GUI.skin.box.fontStyle = FontStyle.Bold;
            GUI.Box(new Rect(xPos_Mid, yPos_mid, 250, 40), "Press 'E' to Enter");
        }
    }

}
