using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum DisplayState
{
    Play,
    Pause
}
public enum ChapterOrder
{
    ChapterPre,
    Chapter1,
    Chapter2,
    Chapter3
}
public class Manager2 : MonoBehaviour
{
    public bool ifFinished = false;
    private int SceneID;
    private ScenePortal scenedoor;
    public AudioSource BGM;
    private bool ifs = false;
    public ChapterOrder co =ChapterOrder.ChapterPre;
    private int size = 60;
    private GameObject Player;
    private DeathControl dc;
    private ResPutUp rpu;
    public DisplayState state;//展示状态,暂停或运行
    GUIStyle guiRectStyle;
    GUIStyle guiTextRectStyle1;
    GUIStyle guiTextRectStyle2;
    //按钮
    public Texture Pause_btn;
    public Texture Continue_btn;
    public Texture Replay_btn;
    public Texture Exit_btn;

    ///道具
    //chapter1
    public Texture FireCreaters_img;
    public Texture Ability1_img;
    public Texture RedPocket_img;
    public Texture Dumplings_img;
    public Texture fu_img;
    //chapter2
    public Texture Ability2_img;
    public Texture Seed_img;
    public Texture Yu_img;
    public Texture Wrench_img;
    public Texture zanzi_img;
    //chapter3
    public Texture Ability3_img;
    public Texture Zongzi_img;
    public Texture Zongye_img;
    public Texture Meat_img;
    public Texture Rice_img;
    public Texture XiongHuangFen_img;
    public Texture XiongHuangJiu_img;

    public float screenX;
    public float screenY;
    public float scaleX;
    public float scaleY;
    ResPutUp ResPart;
    public Vector2 pointStart; // 物品列表顶点
    // Use this for initialization
    void Start()
    {
        
        if(ifFinished)
        {
            GameObject.Find("FinishedEventPoint").GetComponent<BoxCollider2D>().enabled = true;
        }
#if UNITY_STANDALONE_WIN
        GameObject.Find("ControlTexts").SetActive(false);
        GameObject.Find("Controls").SetActive(false);
#endif
#if UNITY_ANDROID
        GameObject.Find("Controls_Windows").SetActive(false);
#endif

        Physics2D.gravity = new Vector3(0f, -9.81f, 0f);

        SceneID = Application.loadedLevel;
        int RecordedScene = GameObject.Find("DataRecord").GetComponent<Data>().sceneID;
        if (RecordedScene != 0&&SceneID!=RecordedScene)
        {
            //Debug.Log("fuck");
            SceneID = GameObject.Find("DataRecord").GetComponent<Data>().sceneID;
            SceneManager.LoadScene(SceneID);
            
        }

        scenedoor =GameObject.Find("SceneDoor").GetComponentInChildren<ScenePortal>();
        if(GameObject.Find("BGM_Audio")!=null)
            BGM = GameObject.Find("BGM_Audio").GetComponent<AudioSource>();
        Player = GameObject.Find("NewHero");
        dc = Player.GetComponent<DeathControl>();
        rpu = Player.GetComponent<ResPutUp>();
       
        screenX = Screen.width;
        screenY = Screen.height;
        scaleX = screenX / 1280;
        scaleY = screenY / 720;
        guiRectStyle = new GUIStyle();
        //guiRectStyle.border = new RectOffset(0, 0, 0, 0);
        guiRectStyle.alignment = TextAnchor.MiddleCenter;
        //textstyle1
        guiTextRectStyle1 = new GUIStyle();
        guiTextRectStyle1.fontSize = (int)(32*scaleX);
        guiTextRectStyle1.fontStyle = FontStyle.Bold;
        guiTextRectStyle1.normal.textColor = Color.white;

        //textstyle2
        guiTextRectStyle2 = new GUIStyle();
        guiTextRectStyle2.fontSize = (int)(36*scaleX);
        guiTextRectStyle2.fontStyle = FontStyle.Bold;
        guiTextRectStyle2.normal.textColor = Color.white;
        guiTextRectStyle2.alignment = TextAnchor.MiddleRight;

        ResPart = GameObject.Find("NewHero").GetComponent<ResPutUp>();
        pointStart = new Vector2(1100*scaleX, 20*scaleY);


        

    }
    public void TFade()
    {
        co = ChapterOrder.ChapterPre;
        ifs = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(BGM!=null)
            if(BGM.volume<=0.1f&&!scenedoor.ifAudioFade)
                BGM.volume += Time.deltaTime / 6;
    }
    void OnGUI()
    {
          string text =rpu.resName;
        if (rpu.showtext)
        {
          
            GUIStyle bb = new GUIStyle();
            bb.fontStyle = FontStyle.Bold;
            bb.alignment = TextAnchor.MiddleCenter;
            bb.normal.background = null;    //这是设置背景填充的
            bb.normal.textColor = new Color(1, 1, 1);   //设置字体颜色的
            bb.fontSize = (int)(40 * scaleX);
            GUI.Label(new Rect(Screen.width * 0.4f, Screen.height * 0.2f, 250 * scaleX, 250 * scaleY), text, bb);
        }
        if (dc.ifdead && dc.isgrounded)
        {
            GUIStyle bb = new GUIStyle();
            bb.normal.background = null;    //这是设置背景填充的
            bb.normal.textColor = new Color(1, 1, 1);   //设置字体颜色的
            bb.fontSize = (int)(100 * scaleX);
            GUI.Label(new Rect(Screen.width * 0.3f, Screen.height * 0.3f, 250 * scaleX, 250 * scaleY), "这次，你死了", bb);
        }

        if(co==ChapterOrder.Chapter1&&!ifs)
        {
            GUIStyle bb = new GUIStyle();
            bb.normal.background = null;    //这是设置背景填充的
            bb.normal.textColor = new Color(1, 1, 1);   //设置字体颜色的
            bb.fontSize = (int)(100 * scaleX);
            GUI.Label(new Rect(Screen.width * 0.3f, Screen.height * 0.25f, 250 * scaleX, 250 * scaleY), "第一章： 春", bb);
            Invoke("TFade",01.5f);
        }

        if (state == DisplayState.Play)
        {
            
            if (GUI.Button(new Rect(20 * scaleX, 20 * scaleY,
                Pause_btn.width * scaleX, Pause_btn.height * scaleY),
                Pause_btn,
                guiRectStyle))
            {
                state = DisplayState.Pause;
                Time.timeScale = 0;
            }
        }
        if (state == DisplayState.Pause)
        {
            if (GUI.Button(new Rect(screenX * 0.5f - Exit_btn.width * 0.5f * scaleX,
                screenY * 0.4f + Replay_btn.height * 0.5f * scaleY + 10f * scaleY,
                Exit_btn.width * scaleX, Exit_btn.height * scaleY),
                Exit_btn,
                guiRectStyle))
                Application.Quit();


            if (GUI.Button(new Rect(screenX * 0.5f - Replay_btn.width * 0.5f * scaleX,
                screenY * 0.4f - Replay_btn.height * 0.5f * scaleY,
                Replay_btn.width * scaleX, Replay_btn.height * scaleY),
                Replay_btn,
                guiRectStyle))
            {
                Time.timeScale = 1;
                SceneManager.LoadScene(SceneID);
            }

            if (GUI.Button(new Rect(screenX * 0.5f - Continue_btn.width * 0.5f * scaleX,
                screenY * 0.4f - Replay_btn.height * 0.5f * scaleY - Continue_btn.height * scaleY - 10f * scaleY,
                Continue_btn.width * scaleX, Continue_btn.height * scaleY),
                Continue_btn,
                guiRectStyle))
            {
                Time.timeScale = 1;
                state = DisplayState.Play;
            }
        }

        int order = 0;
       
        // Debug.Log(ResPart.ToString());
        
        if (ResPart != null && !ResPart.Reses.FireCreaters.empty)
        {
            listing(FireCreaters_img, ref order, ResPart.Reses.FireCreaters.num.ToString()+" X ","爆竹");
        }
        if (ResPart != null && !ResPart.Reses.Ability1.empty)
        {
            listing(Ability1_img, ref order, "", "二段跳能力石");
        }
        if (ResPart != null && !ResPart.Reses.Ability2.empty)
        {
            listing(Ability2_img, ref order, "", "神秘能力石");
        }
        if (ResPart != null && !ResPart.Reses.RedPocket.empty)
        {
            listing(RedPocket_img, ref order, "","红包");
        }
        if (ResPart != null && !ResPart.Reses.Dumplings.empty)
        {
            listing(Dumplings_img, ref order, "","饺子");
        }
        if (ResPart != null && !ResPart.Reses.fu.empty)
        {
            listing(fu_img, ref order, "", "福字");
        }
        if (ResPart != null && !ResPart.Reses.Seed.empty)
        {
            listing(Seed_img, ref order, "", "柳条");
        }
        if (ResPart != null && !ResPart.Reses.Yu.empty)
        {
            listing(Yu_img, ref order, "", "遗失的玉佩");
        }
        if (ResPart != null && !ResPart.Reses.Wrench.empty)
        {
            listing(Wrench_img, ref order, "", "扳手");
        }
        if (ResPart != null && !ResPart.Reses.zanzi.empty)
        {
            listing(zanzi_img, ref order, "", "玉簪");
        }
        if (ResPart != null &&!ResPart.reses.Zongzi.empty)
        {
            listing(Zongzi_img, ref order, ResPart.Reses.Zongzi.num.ToString() + " X ", "粽子");
        }
        if (ResPart != null && !ResPart.reses.Zongye.empty)
        {
            listing(Zongye_img, ref order, "", "粽叶");
        }
        if (ResPart != null && !ResPart.reses.Meat.empty)
        {
            listing(Meat_img, ref order, "", "粽子肉馅");
        }
        
        if (ResPart != null && !ResPart.reses.Rice.empty)
        {
            listing(Rice_img, ref order, "", "米粒");
        }
        if (ResPart != null && !ResPart.reses.XiongHuangFen.empty)
        {
            listing(XiongHuangFen_img, ref order, "", "雄黄粉");
        }
        if (ResPart != null && !ResPart.reses.XiongHuangJiu.empty)
        {
            listing(XiongHuangJiu_img, ref order, "", "雄黄酒");
        }
        if (ResPart != null && !ResPart.reses.Ability3.empty)
        {
            listing(Ability3_img, ref order, "", "潜水能力石");
        }


        // Debug.Log(order.ToString());
    }
    void listing(Texture t, ref int order, string num,string description)
    {
        GUI.Label (new Rect(pointStart.x - 70* scaleX, pointStart.y + (5 + order) * scaleY,
            size * scaleX, size * scaleY),
            description,
            guiTextRectStyle2);
        GUI.Label(new Rect(pointStart.x, pointStart.y + (order * scaleY),
            size * scaleX, size * scaleY),
            t,
            guiRectStyle);
        GUI.Label(new Rect(pointStart.x - 150 * scaleX, pointStart.y +(15+ order) * scaleY,
            size * scaleX, size * scaleY),
            num,
            guiTextRectStyle1);
        order += 80*(int)scaleX;
    }
}