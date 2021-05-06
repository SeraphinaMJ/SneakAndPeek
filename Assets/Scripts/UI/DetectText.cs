using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DetectText : MonoBehaviour
{
    // Start is called before the first frame update
    private Text text;

    public GameObject npc;
    Script_CharacterDetection npc_detection;

    GameObject player;

    void Start()
    {
        text = gameObject.GetComponent<Text>();
        npc_detection = npc.GetComponent<Script_CharacterDetection>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
