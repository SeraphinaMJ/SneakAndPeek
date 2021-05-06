using UnityEngine;
using UnityEngine.SceneManagement;

public class ShoesExit : MonoBehaviour
{
    public Texture HudBox;
    public Texture ShoeHud;

    private GameObject player;
    private GameObject exit;
    private GameObject shoes;
    private GameObject light_;

    static bool hasShoes;
    public bool shoesToMain;

    private Renderer rend;
    private Renderer rend_shoes;
 
    private bool readyToExit;
    private bool nearShoes;

    private float xPos_Mid = Screen.width / 2;
    private float yPos_mid = Screen.height / 2;

    private float Width = Screen.width;
    private float Height = Screen.height;

    private float width_Ratio = 0.96f;
    private float height_Ratio = 0.03f;

    public bool isInsight;
    public bool isRestart;
    private float curTime;

    public static ShoesExit instance;//

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        exit = GameObject.FindGameObjectWithTag("Shoe_Exit");

        shoes = GameObject.FindGameObjectWithTag("shoes");
        light_ = GameObject.FindGameObjectWithTag("shoeLight");

        rend = exit.GetComponent<Renderer>();
        rend_shoes = shoes.GetComponent<Renderer>();
        instance = this;
        isInsight = false;
        isRestart = false;
        curTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ExitCheck();
        ShoesCheck();
    }

    void ExitCheck()
    {
        if (Vector3.Distance(player.transform.position, exit.transform.position) <= 2.5f)
        {
            readyToExit = true;
            rend.material.color = Color.yellow;
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (hasShoes)
                {
                    shoesToMain = true;
                    SceneManager.LoadScene("Level_");
                }
            }
        }
        else
        {
            readyToExit = false;
            rend.material.color = Color.blue;
        }
    }

    void ShoesCheck()
    {
        isInsight = Script_AIController.instance.isTargetInsight;

        if (Vector3.Distance(player.transform.position, shoes.transform.position) <= 2.5f)
        {
            rend_shoes.material.color = Color.yellow;
            if (!hasShoes)
            {
                nearShoes = true;
                light_.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (!isInsight)
                    {
                        hasShoes = true;
                        AudioManager.audio_instance.GetItem();
                    }
                    else
                    {
                        isRestart = true;
                    }
                }
            }
        }
        else
        {
            rend_shoes.material.color = Color.blue;
            nearShoes = false;
            light_.SetActive(false);
        }

        if(isRestart)
        {
            PlayerMovement.instance.charSpeed = 0;
            Timer();
        }

        if(hasShoes)
        {
            light_.SetActive(false);
        }
    }

    void Timer()
    {
        curTime += Time.deltaTime;
        if (curTime >= 2.5f)
        {  
            isRestart = false;
            SceneManager.LoadScene("Level_Turorial");          
        }
    }
    public void OnGUI()
    {
        if (hasShoes && readyToExit)
        {
            GUI.skin.box.fontSize = 20;
            GUI.skin.box.fontStyle = FontStyle.Bold;
            GUI.Box(new Rect(xPos_Mid, yPos_mid, 250, 40), "Press 'E' to Exit");
        }
        
        if(!(hasShoes) && readyToExit)
        {
            GUI.skin.box.fontSize = 20;
            GUI.skin.box.fontStyle = FontStyle.Bold;
            GUI.color = Color.yellow;
            GUI.Box(new Rect(xPos_Mid, yPos_mid, 330, 40), "You have to steal the shoes !!");
        }

        if (nearShoes && !(hasShoes) && !(isRestart))
        {
            GUI.skin.box.fontSize = 20;
            GUI.skin.box.fontStyle = FontStyle.Bold;
            GUI.Box(new Rect(xPos_Mid, yPos_mid, 370, 40), "Press 'E' to Steal Shoes");

            GUI.skin.box.fontSize = 20;
            GUI.color = Color.yellow;
            GUI.Box(new Rect(xPos_Mid * 0.8f, (yPos_mid * 2) * 0.88f, 400, 40), " TIP : Press 'Space Bar' to stealth ");
        }

        if (hasShoes)
        {
            GUI.DrawTexture(new Rect(Width * width_Ratio - 10, Height * height_Ratio, 50, 50), HudBox, ScaleMode.ScaleToFit, true, 1);
            GUI.DrawTexture(new Rect(Width * width_Ratio - 7, Height * height_Ratio, 45, 45), ShoeHud, ScaleMode.ScaleToFit, true, 1);
        }

        if (isRestart)
        {
            GUI.skin.box.fontSize = 40;
            GUI.color = Color.red;
            GUI.Box(new Rect(xPos_Mid * 0.8f, (yPos_mid * 2) * 0.88f, 600, 60), " YOU ARE CAUGHT !! ");
        }



    }

}
