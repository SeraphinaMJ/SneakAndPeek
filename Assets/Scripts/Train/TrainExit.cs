using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrainExit : MonoBehaviour
{
    private GameObject player;
    private GameObject exit;

    private Renderer rend;
    private bool readyToExit;

    private float xPos_Mid = Screen.width / 2;
    private float yPos_mid = Screen.height / 2;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        exit = GameObject.FindGameObjectWithTag("Train_Exit");

        rend = exit.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, exit.transform.position) <= 2.5f)
        {
            readyToExit = true;
            rend.material.color = Color.yellow;
            if (Input.GetKeyDown(KeyCode.E))
            {
                HudManager.gotKey = false;
                HudManager.gotDriver = false;
                HudManager.gotDiamond = false;
                HudManager.gotKeyCode = false;

                Entrance.prevLV = Entrance.PreviousLV.Main;

                SceneManager.LoadScene("Level_Credit");
            }
        }
        else
        {
            readyToExit = false;
            rend.material.color = Color.blue;
        }

    }
    public void OnGUI()
    {
        if (readyToExit)
        {
            GUI.skin.box.fontSize = 15;
            GUI.skin.box.fontStyle = FontStyle.Bold;
            GUI.Box(new Rect(xPos_Mid, yPos_mid, 250, 40), "Press 'E' to take Train");
        }
    }

}
