using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public float DiamondHUD = 1;
    public Sprite DIA, DIAGET;

    void Start()
    {
        DIA = Resources.Load<Sprite>("dia");
        DIAGET = Resources.Load<Sprite>("diaGet");
    }

    void Update()
    {
        Image image;
        if (!(image = gameObject.GetComponent<Image>()))
            Debug.Log("no image component");


        if (DiamondHUD == 1)
        {
            image.sprite = DIA;
            //if(gameObject.)
                DiamondHUD--;
        }
       

        if (DiamondHUD == 0)
            image.sprite = DIAGET;
    }




}
