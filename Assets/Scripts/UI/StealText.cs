using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StealText : MonoBehaviour
{
    // Start is called before the first frame update
    private Text text;

    public GameObject Slider;

    ProgressBar pgBar;

    float curTime = 0;
    float targetTime = 3f;
    void Start()
    {
        text = gameObject.GetComponent<Text>();
        if(Slider != null)
           pgBar = Slider.GetComponent<ProgressBar>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Slider != null)
        {
            if (pgBar.isStealed)
            {
                text.transform.position = Slider.transform.position;
                text.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);

                curTime += Time.deltaTime;
                if (curTime >= targetTime)
                {
                    text.transform.localScale = new Vector3(0, 0, 0);
                }
            }
        }
    }
}
