using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DigipenLogo : MonoBehaviour
{
    public GameObject logo;

    private float curTime;
    private float targetTime;

    void Start()
    {
        curTime = 0;
        targetTime = 3.5f;
    }

    // Update is called once per frame
    void Update()
    {
        SplashLogo();
    }

    void SplashLogo()
    {
        curTime += Time.deltaTime;
        if (Input.anyKey || curTime >= targetTime)
        {
            ChangeScene();
        }
    }

    void ChangeScene()
    {
        SceneManager.LoadScene("GameLogo");
    }

}
