using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyPad : MonoBehaviour
{
    public string curPassword = "4592";
    public string input;
    public bool onTrigger;
    public bool doorOpen;
    public bool keypadScreen;
    public Transform doorHinge;
    public bool isCorrectInput;
    public float time;

    private float xPos_Mid = Screen.width / 2;
    private float yPos_mid = Screen.height / 2;

    void OnTriggerEnter(Collider other)
    {
        onTrigger = true;
    }

    void OnTriggerExit(Collider other)
    {
        onTrigger = false;
        keypadScreen = false;
        input = "";
    }

    private void Start()
    {
        isCorrectInput = true;
    }

    void Update()
    {
        if (keypadScreen)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0))
            {
                input = input + "0";
                AudioManager.audio_instance.KeypadBeep();
            }

            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
            {
                input = input + "1";
                AudioManager.audio_instance.KeypadBeep();
            }

            if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
            {
                input = input + "2";
                AudioManager.audio_instance.KeypadBeep();
            }

            if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
            {
                input = input + "3";
                AudioManager.audio_instance.KeypadBeep();
            }

            if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
            {
                input = input + "4";
                AudioManager.audio_instance.KeypadBeep();
            }

            if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
            {
                input = input + "5";
                AudioManager.audio_instance.KeypadBeep();
            }

            if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6))
            {
                input = input + "6";
                AudioManager.audio_instance.KeypadBeep();
            }

            if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7))
            {
                input = input + "7";
                AudioManager.audio_instance.KeypadBeep();
            }

            if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8))
            {
                input = input + "8";
                AudioManager.audio_instance.KeypadBeep();
            }

            if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9))
            {
                input = input + "9";
                AudioManager.audio_instance.KeypadBeep();
            }


            if (input.Length == curPassword.Length && input == curPassword)
            {
                PasswordCorrect();
            }
            else if (input.Length > curPassword.Length)
            {
                PasswordFail();
            }
            else if (input.Length == curPassword.Length && input != curPassword)
            {
                PasswordFail();
            }

            if (time == 0)
            {
                AudioManager.audio_instance.StopCodeCorrect();
                AudioManager.audio_instance.StopCodeFail();
            }


            if (doorOpen)
            {
                var newRot = Quaternion.RotateTowards(doorHinge.rotation, Quaternion.Euler(0.0f, 90.0f, 0.0f), Time.deltaTime * 250);
                doorHinge.rotation = newRot;


            }
        }
    }

    void PasswordFail()
    {
        AudioManager.audio_instance.PlayCodeFail();
        time += Time.deltaTime;
        if (time >= 1.5f)
        {
            input = "";
            time = 0;
            isCorrectInput = false;
        }
    }
    void PasswordCorrect()
    {
        doorOpen = true;
        time += Time.deltaTime;
        AudioManager.audio_instance.PlayCodeCorrect();
        if (time >= 1.2f)
        {
            time = 0;
            SceneManager.LoadScene("Level_");
        }
    }

    void OnGUI()
    {
        float xMid = Screen.width;
        float yMid = Screen.height;

        if (!doorOpen)
        {
            if (onTrigger)
            {
                GUI.skin.box.fontSize = 15;
                GUI.skin.box.fontStyle = FontStyle.Bold;

                GUI.Box(new Rect(xMid /2 , yMid/3, 200, 50), "Press 'E' to open keypad");

                if (Input.GetKeyDown(KeyCode.E))
                {
                    AudioManager.audio_instance.NearKeyPad();
                    keypadScreen = true;
                    onTrigger = false;
                }
            }

            if (time != 0)
            {
                time += Time.deltaTime;

                GUI.skin.box.fontSize = 15;
                GUI.skin.box.fontStyle = FontStyle.Bold;
                GUI.color = Color.red;
            }

            if (keypadScreen)
            {
                GUI.skin.label.fontSize = 10;
                GUI.Label(new Rect(510, 350, 100, 100), "'Esc' to exit");

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    keypadScreen = false;
                }

                if (keypadScreen)
                {
                    

                    GUI.Box(new Rect(500, 10, 320, 455), "");
                    GUI.Box(new Rect(500, 20, 310, 25), input);

                    if (GUI.Button(new Rect(510, 50, 100, 100), "1"))
                    {
                        input = input + "1";
                        AudioManager.audio_instance.KeypadBeep();
                    }

                    if (GUI.Button(new Rect(610, 50, 100, 100), "2"))
                    {
                        input = input + "2";
                        AudioManager.audio_instance.KeypadBeep();
                    }

                    if (GUI.Button(new Rect(710, 50, 100, 100), "3"))
                    {
                        input = input + "3";
                        AudioManager.audio_instance.KeypadBeep();
                    }

                    /////////////////////////
                    if (GUI.Button(new Rect(510, 150, 100, 100), "4"))
                    {
                        input = input + "4";
                        AudioManager.audio_instance.KeypadBeep();
                    }

                    if (GUI.Button(new Rect(610, 150, 100, 100), "5"))
                    {
                        input = input + "5";
                        AudioManager.audio_instance.KeypadBeep();
                    }

                    if (GUI.Button(new Rect(710, 150, 100, 100), "6"))
                    {
                        input = input + "6";
                        AudioManager.audio_instance.KeypadBeep();
                    }

                    ///////////////////////////////////////
                    if (GUI.Button(new Rect(510, 250, 100, 100), "7"))
                    {
                        input = input + "7";
                        AudioManager.audio_instance.KeypadBeep();
                    }

                    if (GUI.Button(new Rect(610, 250, 100, 100), "8"))
                    {
                        input = input + "8";
                        AudioManager.audio_instance.KeypadBeep();
                    }

                    if (GUI.Button(new Rect(710, 250, 100, 100), "9"))
                    {
                        input = input + "9";
                        AudioManager.audio_instance.KeypadBeep();
                    }
                    ///////////////////////////
                    if (GUI.Button(new Rect(610, 355, 100, 100), "0"))
                    {
                        input = input + "0";
                        AudioManager.audio_instance.KeypadBeep();
                    }
                    if (PlayerItem.instance.hasKeyCode)
                    {
                        GUI.skin.box.fontSize = 20;
                        GUI.skin.box.fontStyle = FontStyle.Bold;
                        GUI.color = Color.green;
                        GUI.Box(new Rect(xPos_Mid * 1.15f, yPos_mid * 0.1f, 200, 40), " CODE : 4 5 9 2 ");
                    }
                }

            }
        }
    }
}
