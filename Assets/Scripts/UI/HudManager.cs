using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
    public Texture HudBox;
    public Texture DiamondHud;
    public Texture KeyHud;
    public Texture DriverHud;
    public Texture KeyCodeHud;

    private float Width = Screen.width;
    private float Height = Screen.height;

    private float width_Ratio = 0.96f;
    private float height_Ratio = 0.03f;

    static public bool gotKey;
    static public bool gotDriver;
    static public bool gotDiamond;
    static public bool gotKeyCode;

    void Start()
    {

    }

    void Update()
    {
        if (PlayerItem.instance.hasKey)
        {
            gotKey = true;
        }
        if (PlayerItem.instance.hasDriver)
        {
            gotDriver = true;
        }
        if (PlayerItem.instance.hasDiamond)
        {
            gotDiamond = true;
        }
        if (PlayerItem.instance.hasKeyCode)
        {
            gotKeyCode = true;
        }

       // Debug.Log(Entrance.prevLV);

        if (Script_AIController.instance != null)
        {
            if (Script_AIController.instance.m_isPlayerCaught)
            {
                if (Entrance.prevLV == Entrance.PreviousLV.Building)
                {
                    gotDriver = false;
                    gotDiamond = false;
                    gotKeyCode = false;
                }
                if (Entrance.prevLV == Entrance.PreviousLV.Bank)
                {
                    gotKey = false;
                }
            }
        }
    }

    // Update is called once per frame
    private void OnGUI()
    {
        if (gotKey)
        {
            GUI.DrawTexture(new Rect(Width * width_Ratio - 10, Height * height_Ratio, 50, 50), HudBox, ScaleMode.ScaleToFit, true, 1);
            GUI.DrawTexture(new Rect(Width * width_Ratio - 10, Height * height_Ratio, 50, 50), KeyHud, ScaleMode.ScaleToFit, true, 1);
        }
        if (gotDriver)
        {
            GUI.DrawTexture(new Rect(Width * width_Ratio - 65, Height * height_Ratio, 50, 50), HudBox, ScaleMode.ScaleToFit, true, 1);
            GUI.DrawTexture(new Rect(Width * width_Ratio - 65, Height * height_Ratio, 50, 50), DriverHud, ScaleMode.ScaleToFit, true, 1);
        }
        if (gotDiamond)
        {
            GUI.DrawTexture(new Rect(Width * width_Ratio - 120, Height * height_Ratio, 50, 50), HudBox, ScaleMode.ScaleToFit, true, 1);
            GUI.DrawTexture(new Rect(Width * width_Ratio - 120, Height * height_Ratio, 50, 50), DiamondHud, ScaleMode.ScaleToFit, true, 1);
        }
        if (gotKeyCode)
        {
            GUI.DrawTexture(new Rect(Width * width_Ratio - 175, Height * height_Ratio, 50, 50), HudBox, ScaleMode.ScaleToFit, true, 1);
            GUI.DrawTexture(new Rect(Width * width_Ratio - 176f, Height * height_Ratio, 65, 50), KeyCodeHud, ScaleMode.ScaleToFit, true, 1.5f);
        }
    }

}
