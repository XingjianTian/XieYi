using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeZongzi : MonoBehaviour
{
    //完成后门浮现
    SpriteRenderer DoorColliderS;
    SpriteRenderer DoorBaseS;
    Color DoorColiiderColor;
    Color DoorBaseColor;
    float minAlpha = 0f;
    float maxAlpha = 1f;
    float varifySpeed = 1f;
    public float curAlpha = 0f;

    //声效
    public AudioClip[] audioc;

    //掉落范围
    public Vector3 screenPos;
    public Vector3 moveWidth;
    public bool ifGameOn = true;
    ResPutUp r;
    //三种物品
    public GameObject ZongYePrefab;
    public GameObject MeatInsidePrefab;
    public GameObject RicePrefab;

    //此次掉落的物品
    private GameObject NewThing;
    public float maxWidth;
    private float time = 1;

    //计数
    public string Xulie = "";
    public int CompleteZongziNum = 0;
    bool once1 = true;
    bool once2 = true;
    // Use this for initialization
    void Start()
    {
        DoorColliderS = GameObject.Find("Door1collider").GetComponent<SpriteRenderer>();
        DoorBaseS = GameObject.Find("Door1Base").GetComponent<SpriteRenderer>();
        r = GameObject.Find("NewHero").GetComponent<ResPutUp>();
        //将屏幕的宽度转换成世界坐标.
        screenPos = new Vector3(Screen.width, 0, 0) * 0.67f;
        moveWidth = Camera.main.ScreenToWorldPoint(screenPos) / 2.5f;
        //获取掉落物品自身的宽度.
        float ThingWidth = ZongYePrefab.GetComponent<Renderer>().bounds.extents.x;
        //计算掉落物品实例化位置的宽度.
        maxWidth = moveWidth.x - ThingWidth;
    }
    void FixedUpdate()
    {
        if (ifGameOn && once1)
        {
            //GameObject.Find("BGM_Audio").GetComponent<AudioSource>().clip = ;
            once1 = false;
        }
        if (!ifGameOn)
            return;

        time -= Time.deltaTime;
        if (time < 0)
        {
            //产生一个随机数，代表实例化下一个保龄球所需的时间。
            time = Random.Range(0.75f, 1f);
            /*在保龄球实例化位置的宽度内产生一个随机数，来控制实例化的保龄球的位	置.*/
            float posX = Random.Range(0.3f, 3f);
            Vector3 spawnPosition = new Vector3(posX, transform.position.y, 0);
            //实例化保龄球，10秒后销毁.
            int n = Random.Range(0, 3);
            switch (n)
            {
                case 0: NewThing = Instantiate(ZongYePrefab, spawnPosition, Quaternion.identity); break;
                case 1: NewThing = Instantiate(MeatInsidePrefab, spawnPosition, Quaternion.identity); break;
                case 2: NewThing = Instantiate(RicePrefab, spawnPosition, Quaternion.identity); break;
                default: break;
            }
            Destroy(NewThing, 5);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (CompleteZongziNum < 2)
        {
            if (Xulie.Length == 1)
            {
                if (Xulie != "0")
                {
                    //错误顺序音效后期添加
                    //AudioSource.PlayClipAtPoint(, transform.position);
                    r.showGetRes("制作工序不对！");
                    Xulie = "";
                    ClearZong();
                }
                else
                {
                    if (r.reses.Zongye.empty)
                    {
                        r.reses.Zongye.getRes();
                        r.showGetRes("你获得了 粽叶");
                    }
                }

            }
            else if (Xulie.Length == 2)
            {
                if (Xulie != "01")
                {
                    //错误顺序音效后期添加
                    //AudioSource.PlayClipAtPoint(, transform.position);
                    r.showGetRes("制作工序不对！");
                    Xulie = "";
                    ClearZong();
                }
                else
                {
                    if (r.reses.Meat.empty)
                    {
                        r.reses.Meat.getRes();
                        r.showGetRes("你获得了 粽子肉馅");
                    }
                }
            }
            else if (Xulie.Length == 3)
            {
                if (Xulie != "012")
                {
                    //错误顺序音效后期添加
                    //AudioSource.PlayClipAtPoint(, transform.position);
                    r.showGetRes("制作工序不对！");
                    Xulie = "";
                    ClearZong();
                }
                else
                {
                    if (r.reses.Rice.empty)
                    {
                        r.reses.Rice.getRes();
                        r.showGetRes("你获得了 米粒");
                        StartCoroutine(Wait());
                    }
                    //合成粽子
                    
                }
            }
        }
        else
        {
            if (once2)
            {
                GameObject.Find("Text_2").GetComponent<TextMesh>().text = "恭喜你通过了试炼！\n拿到了需要的粽子！\n回去的大门已经为你打开";
                StartCoroutine(GameObject.Find("NewHero").GetComponent<ResPutUp>().Emerge(GameObject.Find("Text_2")));
                once2 = false;
            }
            ifGameOn = false;
            //changeBGM
            DoorEmerge();

        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.5f);
        ClearZong();
        r.reses.Zongzi.getRes();
        r.showGetRes("成功制作一个粽子！");
        Xulie = "";
        CompleteZongziNum+=1;

    }
    void ClearZong()
    {
        r.reses.Zongye.clear();
        r.reses.Meat.clear();
        r.reses.Rice.clear();
    }
    void DoorEmerge()
    {
        if (curAlpha < maxAlpha)
        {
            curAlpha += Time.deltaTime * varifySpeed;
            //对话框和文字渐显示
            curAlpha = Mathf.Clamp(curAlpha, minAlpha, maxAlpha);
            DoorColiiderColor = DoorColliderS.color;
            DoorColiiderColor.a = curAlpha;
            DoorColliderS.color = DoorColiiderColor;

            DoorBaseColor = DoorBaseS.color;
            DoorBaseColor.a = curAlpha;
            DoorBaseS.color = DoorColiiderColor;
        }
        if(curAlpha>=maxAlpha )
        {
            GameObject.Find("Door1collider").GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
