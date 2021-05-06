using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabinet : MonoBehaviour
{
    private GameObject player;
    private GameObject screwDriver;
    private GameObject safeLight;
    private GameObject safeDoor;

    private ScrewDriver pScrewDriver;
    private PlayerItem pItem;

    public bool readyToOpen;
    public bool isUsed;
    private bool safeDoorOpen;

    Renderer rend;
    new Light light;

    public static Cabinet instance;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pItem = player.GetComponent<PlayerItem>();

        screwDriver = GameObject.FindGameObjectWithTag("Key");
        if(screwDriver != null)
            pScrewDriver = screwDriver.GetComponent<ScrewDriver>();

        safeLight = GameObject.FindGameObjectWithTag("SafeLight");
        if(safeLight != null)
            light = safeLight.GetComponent<Light>();

        safeDoor = GameObject.FindGameObjectWithTag("SafeDoor");

        rend = this.GetComponent<Renderer>();

        readyToOpen = false;
        isUsed = false;
        instance = this;

        safeDoorOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        //safeLight.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        if (isUsed)
        {
            if (!safeDoorOpen)
            {
                safeDoor.transform.Rotate(0, -60, 0);
                safeDoor.transform.position = new Vector3(this.transform.position.x + 0.29f, this.transform.position.y, this.transform.position.z + 0.8f);
                safeDoorOpen = true;
            }       
            light.color = Color.red;
        }
    }
}
