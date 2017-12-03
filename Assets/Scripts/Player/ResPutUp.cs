using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResPutUp : MonoBehaviour {
    public float varifySpeed = 1.2f;
    private bool ifFireCrackerGotFromNpc = false;
    private bool ifSeedGotFromNpc = false;
    private bool ifBridgeDown = false;
    public bool if_E_P = false;
    public string resName;
    public bool showtext;
    public bool ifShieldOn = false;
    public GameObject shield;
	public GameObject cannon;
    public GameObject tree;
    public CannonShot cs;
    public bool ifCoroutine = false;
    public RaycastHit2D hit;
    public float curAlpha = 0;
    //private GameObject hero;
    public resList reses;
    private PlayerControl playerControl;

	public bool lastE;

    public resList Reses
    {
        get
        {
            return reses;
        }
    }
    
    public void Button_E_Down()
    {
		lastE = if_E_P;
		if_E_P = true;
    }
    public void Button_E_Up()
    {
		lastE = if_E_P;
        if_E_P = false;
    }
    public bool if_E_Pressed
    {
        get
        {
			if (if_E_P != lastE && if_E_P == true)
				return true;
            else
                return false;
        }
    }
    // Use this for initialization
    void Start() {
        lastE = false; 
        reses = new resList();
        if (cannon != null)
            cs = cannon.GetComponent<CannonShot>();
        playerControl = GetComponent<PlayerControl>();
		tree = GameObject.Find ("Tree");
    }
    public void showGetRes(GameObject obj)
    {
        resName = "你获得了 ";
        AudioSource.PlayClipAtPoint(GetComponent<PlayerControl>().Events[0], transform.position);
        switch (obj.name)
        {
			case "Ability1": resName += "二段跳能力石"; break;
            case "Ability3": resName += "潜水能力石"; break;
            case "FireCraker": resName += "爆竹"; break;
            case "RedPocket": resName += "红包"; break;
            case "NPC1": resName += "饺子"; break;
            case "NPC1 (1)": resName += "爆竹"; break;
            case "NPC_ChaLiu": resName += "柳条"; break;
            case "fu": resName += "缺失的福字"; break;
            case "Yu": resName += "遗失的玉佩"; break;
            case "Wrench": resName += "扳手"; break;
        }
        showtext = true;
        StartCoroutine(fadetext());

    }
    public void showGetRes(string objName)
    {
        AudioSource.PlayClipAtPoint(GetComponent<PlayerControl>().Events[0], transform.position);
        resName = objName;
        showtext = true;
        StartCoroutine(fadetext());
    }
    IEnumerator fadetext()
    {
        yield return new WaitForSeconds(1f);
        showtext = false;
    }
    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey("e"))
        //{
        hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.05f), playerControl.facingRight ^ playerControl.ifUpSideDown ? Vector2.right : Vector2.left, 0.2f, 1 << LayerMask.NameToLayer("res"));
        if (hit.collider != null&&playerControl.grounded)
        {
            //Debug.Log("fuck");
            if (hit.collider.gameObject.CompareTag("res"))
            {
                if (hit.collider.gameObject.name == "FireCraker" && !reses.FireCreaters.full)
                {

                    if (Input.GetKey("e") || if_E_Pressed)
                    {
                        showGetRes(hit.collider.gameObject);
                        reses.FireCreaters.getRes(hit.collider.gameObject);
                        Destroy(hit.collider.gameObject);
                        GameObject.Find("GotFirstFireCrackerEventPoint").GetComponent<BoxCollider2D>().enabled = true;
                    }
                }
                else if (hit.collider.gameObject.name == "Ability1" && !reses.Ability1.full)
                {
                    if (!ifCoroutine)
                    {
#if UNITY_ANDROID
                        hit.collider.gameObject.GetComponentInChildren<TextMesh>().text = "靠近按“■”键试试";
#endif
#if UNITY_STANDALONE_WIN
                        hit.collider.gameObject.GetComponentInChildren<TextMesh>().text = "靠近按“E”键试试";
#endif
                        StartCoroutine(Emerge(hit.collider.gameObject));
                        ifCoroutine = true;
                    }
                    if (Input.GetKey("e") || if_E_Pressed)
                    {
                        showGetRes(hit.collider.gameObject);
                        reses.Ability1.getRes(hit.collider.gameObject);
                        curAlpha = 0;
                        GameObject.Find("GotDoubleJumpEventPoint").GetComponent<BoxCollider2D>().enabled = true;
                        StartCoroutine(Emerge(GameObject.Find("Text_1 (1)")));
                        Destroy(hit.collider.gameObject);
                    }
                    //Debug.Log(hit.collider.gameObject.name);
                    //Destroy(hit.collider.gameObject);
                }
				else if (hit.collider.gameObject.name == "Ability3" && !reses.Ability3.full)
				{
					if (!ifCoroutine)
					{
                        
						StartCoroutine(Emerge(hit.collider.gameObject));
						ifCoroutine = true;
					}
					if (Input.GetKey("e") || if_E_Pressed)
					{
						showGetRes(hit.collider.gameObject);
						reses.Ability3.getRes(hit.collider.gameObject);
						curAlpha = 0;
						GameObject.Find("GotDivingEventPoint").GetComponent<BoxCollider2D>().enabled = true;
						StartCoroutine(Emerge(GameObject.Find("Text_1 (1)")));
						Destroy(hit.collider.gameObject);
						//注释掉的是改变水体透明度
						/*GameObject waterManager = GameObject.Find ("WaterManager");
						foreach (Transform child in waterManager.transform) {
							if (child.name == "WaterMesh(Clone)") {
								Color c = child.GetComponent<MeshRenderer> ().material.color;
								child.GetComponent<MeshRenderer> ().material.color = new Color (c.r, c.g, c.b, 0.6f);
							}
						}*/
					}
					//Debug.Log(hit.collider.gameObject.name);
					//Destroy(hit.collider.gameObject);
				}
                
                else if (hit.collider.gameObject.name == "fu" && !reses.fu.full)
                {
                    if (Input.GetKey("e") || if_E_Pressed)
                    {
                        showGetRes(hit.collider.gameObject);
                        GameObject.Find("PutFuEventPoint").GetComponent<BoxCollider2D>().enabled = true;
                        GameObject.Find("BeforePutFuEventPoint").GetComponent<BoxCollider2D>().enabled = false;
                        reses.fu.getRes(hit.collider.gameObject);
                        Destroy(hit.collider.gameObject);
                    }
                }
                else if (hit.collider.gameObject.name == "Yu" && !reses.Yu.full)
                {
                    if (Input.GetKey("e") || if_E_Pressed)
                    {
                        showGetRes(hit.collider.gameObject);
                        GameObject.Find("GotYuEventPoint").GetComponent<BoxCollider2D>().enabled = true;
                        reses.Yu.getRes(hit.collider.gameObject);
                        Destroy(hit.collider.gameObject);
                    }
                }
                else if (hit.collider.gameObject.name == "Wrench" && !reses.Wrench.full)
                {
                    if (Input.GetKey("e") || if_E_Pressed)
                    {
                        //GameObject.Find("NPC_Qiao").GetComponentInChildren<TextMesh>().text = "噢你真是太棒了！让我\n为你把桥放下来吧";

                        showGetRes(hit.collider.gameObject);
                        GameObject.Find("GotWrenchEventPoint").GetComponent<BoxCollider2D>().enabled = true;
                        reses.Wrench.getRes(hit.collider.gameObject);
                        Destroy(hit.collider.gameObject);
                    }
                }
            }
            else if (hit.collider.gameObject.CompareTag("onlyTrigger"))
            {
                if (hit.collider.gameObject.name == "Cannon"/* && reses.FireCreaters.full*/)
                {
                    if (Input.GetKeyDown("e") || if_E_Pressed)
                    {
                        if (reses.FireCreaters.numOfRes > 0)
                        {
                            cs.FireCrackersAdded++;
                            reses.FireCreaters.numOfRes--;
                            if (cs.FireCrackersAdded == 3)
                            {
                                cannon.GetComponentInChildren<TextMesh>().text = "准备发射";
                                //cannon.GetComponentInChildren<TextMesh>().color = new Color(1f,1f,1f,0f);
                                if (Input.GetKeyDown("e") || if_E_Pressed)
                                {
                                    StartCoroutine(cs.RollToShot());
                                    reses.FireCreaters.clear();
                                    hit.collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                                }
                            }
                            else
                            {
                                if ((Input.GetKeyDown("e") || if_E_Pressed) && curAlpha == 0)
                                {
                                    StartCoroutine(Emerge(hit.collider.gameObject));
                                }
                            }
							lastE = true;
                            // if_E_P = false;
                        }


                    }
                }
            }
            else if (hit.collider.gameObject.CompareTag("NPC"))
            {
                // Debug.Log("fuck");
                if (hit.collider.gameObject.name == "NPC1")
                {
                    if (!ifCoroutine)
                    {
                        ifCoroutine = true;
                        StartCoroutine(Emerge(hit.collider.gameObject));

                    }
                    if (Input.GetKeyDown("e") || if_E_Pressed)
                    {
                        if (reses.RedPocket.full)
                        {
                            hit.collider.gameObject.GetComponentInChildren<TextMesh>().text = "太谢谢你了,听说\n年兽喜欢吃饺子！";
                            reses.RedPocket.clear();
                            reses.Dumplings.getRes();
                            showGetRes(hit.collider.gameObject);
                            GameObject.Find("GotDumplingsEventPoint").GetComponent<BoxCollider2D>().enabled = true;
                            GameObject.Find("ThrowDumplingsEventPoint").GetComponent<BoxCollider2D>().enabled = true;

                        }
                    }
                }
                else if (hit.collider.gameObject.name == "NPC1 (1)")
                {
                    if (!ifCoroutine)
                    {
                        ifCoroutine = true;
                        if (ifFireCrackerGotFromNpc)
                            hit.collider.gameObject.GetComponentInChildren<TextMesh>().text = "爆竹都给你了！";
                        StartCoroutine(Emerge(hit.collider.gameObject));
                        Debug.Log("hit.collider.gameObject.name");
                    }
                    if (!ifFireCrackerGotFromNpc)
                    {
                        //Debug.Log("fuck");
                        reses.FireCreaters.getRes();
                        showGetRes(hit.collider.gameObject);
                        ifFireCrackerGotFromNpc = true;
                    }
                }
                else if (hit.collider.gameObject.name == "NPC_ChaLiu")
                {
                    if (!ifCoroutine)
                    {
                        ifCoroutine = true;
                        //if (ifSeedGotFromNpc)
                        //hit.collider.gameObject.GetComponentInChildren<TextMesh>().text = "年轻就是好啊！";
                        StartCoroutine(Emerge(hit.collider.gameObject));

                    }
                    if (Input.GetKeyDown("e") || if_E_Pressed)
                    {
                        if (!ifSeedGotFromNpc)
                        {
                            hit.collider.gameObject.GetComponentInChildren<TextMesh>().text = "太谢谢你了！\n一路顺风！";
                            reses.Seed.getRes();
                            GameObject.Find("GotLiuTiaoEventPoint").GetComponent<BoxCollider2D>().enabled = true;
                            showGetRes(hit.collider.gameObject);
                            ifSeedGotFromNpc = true;

                        }
                    }
                }
                else if (hit.collider.gameObject.name == "NPC_Qiao")
                {
                    if (!ifCoroutine)
                    {
                        ifCoroutine = true;
                        StartCoroutine(Emerge(hit.collider.gameObject));

                    }

                    if (Input.GetKeyDown("e") || if_E_Pressed)
                    {
                        if (reses.Wrench.full)
                        {
                            hit.collider.gameObject.GetComponentInChildren<TextMesh>().text = "真是太棒了，让我\n为你把桥放下来";
							//卸载秋千的脚本，降低消耗
							GameObject.Find ("Tree").GetComponent<spring> ().enabled = false;
                            reses.Wrench.clear();
                            //放桥Coroutine
                            if (!ifBridgeDown)
                            {
                                BridgeDown();
                            }
                        }
                    }
                }
                else if(hit.collider.gameObject.name=="NPC_WineMaker")
                {
                    if (!ifCoroutine)
                    {
                        ifCoroutine = true;
                        StartCoroutine(Emerge(hit.collider.gameObject));

                    }

                    if (Input.GetKeyDown("e") || if_E_Pressed)
                    {
                        if (reses.XiongHuangFen.full)
                        {
                            hit.collider.gameObject.GetComponentInChildren<TextMesh>().text = "不错...让我为你\n酿一壶雄黄酒吧";
                            reses.XiongHuangFen.clear();
                            reses.XiongHuangJiu.getRes();
                            showGetRes("你获得了 雄黄酒");
                            
                            StartCoroutine(ChangeWineMakerText(hit.collider.gameObject));
							lastE = true;
                        }
                    }
                    
                }
                else if(hit.collider.gameObject.name== "NPC_ChuanFu")
                {
                    if (!ifCoroutine)
                    {
                        ifCoroutine = true;
                        StartCoroutine(Emerge(hit.collider.gameObject));
                    }

                    if (Input.GetKeyDown("e") || if_E_Pressed)
                    {
                        if (reses.Zongzi.full)
                        {
                            AudioSource.PlayClipAtPoint(playerControl.GetComponent<PlayerControl>().Events[0], transform.position);
							hit.collider.gameObject.GetComponentInChildren<TextMesh>().text = "一个粽子就想坐船\n你当我是什么人！";
                            reses.Zongzi.numOfRes = 1;
                            if (!ifCoroutine)
                            {
                                ifCoroutine = true;
                                StartCoroutine(Emerge(hit.collider.gameObject));
                            }
                        }
                        else if(!reses.Zongzi.empty)
                        {
                            AudioSource.PlayClipAtPoint(playerControl.GetComponent<PlayerControl>().Events[0], transform.position);
                            hit.collider.gameObject.GetComponentInChildren<TextMesh>().text = "嘿嘿,两个差不多\n自己上去吧";
                            reses.Zongzi.numOfRes = 1;
                            reses.Zongzi.clear();
                            if (!ifCoroutine)
                            {
                                ifCoroutine = true;
                                StartCoroutine(Emerge(hit.collider.gameObject));
                            }
                            Destroy(GameObject.Find("BoundaryEventPoint (2)"));
                        }
						lastE = true;
                    }
                }
                else if(hit.collider.gameObject.name== "SeeSnakeAndReturn")
                {
                    if (!ifCoroutine && reses.XiongHuangJiu.empty)
                    {
                        playerControl.enabled = false;
                        playerControl.Flip();
                        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(180f, 50f));
                        ifCoroutine = true;
                        StartCoroutine(SeeSnake());
                    }
                    else if(!ifCoroutine&&!reses.XiongHuangJiu.empty)
                    {
                        hit.collider.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
                    }
                    
                }
                else
                {
                    if (!ifCoroutine)
                    {
                        StartCoroutine(Emerge(hit.collider.gameObject));
                        ifCoroutine = true;
                    }
                }

            }
			else if ((Input.GetKeyDown("e") || if_E_Pressed) && hit.collider.gameObject.CompareTag("Tree"))
            {
				if (!tree.GetComponent<SpriteRenderer> ().enabled) {
					if (reses.Seed.numOfRes > 0) {
						AudioSource.PlayClipAtPoint (playerControl.Events [1], transform.position);
						StartCoroutine (SwingCreateor ());
						tree.GetComponent<SpriteRenderer> ().enabled = true;
						tree.GetComponent<Animator> ().enabled = true;
					}
				} else if (reses.Seed.numOfRes == 0) {
					GameObject.Find ("Tree").GetComponent<spring> ().opTime += 1;
				}
				lastE = true;
            }
        }
        /*
        else if ((Input.GetKeyDown("e") || if_E_Pressed) && hit.collider == null && reses.Ability2.full)
        {
            bool ifcouroutine = true;
            if (!ifShieldOn)
            {
                shield.GetComponent<MeshRenderer>().enabled = true;
                shield.GetComponent<DynamicLight>().enabled = true;
                ifShieldOn = true;
                if (ifcouroutine)
                {
                    StartCoroutine(ShieldFade());
                }
            }
        }*/
    }
    IEnumerator ChangeWineMakerText(GameObject npcWineMaker)
    {
        yield return new WaitForSeconds(5f);
        npcWineMaker.GetComponentInChildren<TextMesh>().text = "唉，我这个老酒鬼\n不中用了哦";
        
    }
    IEnumerator SeeSnake()
    {
        yield return new WaitForSeconds(0.3f);
        playerControl.enabled = true;
        ifCoroutine = false;
    }
    public IEnumerator ShieldFade()
    {
        yield return new WaitForSeconds(5f);
        shield.GetComponent<MeshRenderer>().enabled = false;
        shield.GetComponent<DynamicLight>().enabled = false;
        ifShieldOn = false;

    }
	public IEnumerator SwingCreateor()
	{
		--reses.Seed.numOfRes;
		yield return new WaitForSeconds (2.5f);
		GameObject.Find ("SeedShouldHereEventPoint1").GetComponent<BoxCollider2D>().enabled = true;
	}


    public IEnumerator Emerge(GameObject obj)
    {
        if (obj != null && obj.CompareTag("BgEmerge"))
        {
            SpriteRenderer sp = obj.GetComponent<SpriteRenderer>();
            for (; curAlpha < 1; curAlpha += Time.deltaTime * varifySpeed)
            {
                sp.color = new Color(1f, 1f, 1f, curAlpha);
                yield return 0;
            }
            yield return new WaitForSeconds(0.5f);
            yield return StartCoroutine(Fade(obj));
        }
        else if (obj != null)
        {
            TextMesh tm = obj.GetComponentInChildren<TextMesh>();
            for (; curAlpha < 1; curAlpha += Time.deltaTime * varifySpeed)
            {
                if (obj != null && obj.CompareTag("res"))
                    tm.color = new Color(1f, 1f, 1f, curAlpha);
                else if (obj != null && obj.CompareTag("onlyTrigger"))
                {
                    tm.color = new Color(1f, 1f, 1f, curAlpha);
                }
                else if (obj != null && obj.CompareTag("NPC"))
                {
                    tm.color = new Color(0f, 0f, 0f, curAlpha);
                    obj.transform.Find("NPCduihuakuang").GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, curAlpha);
                }
                else if (obj != null && obj.CompareTag("EventText"))
                {
                    tm.color = new Color(1f, 1f, 1f, curAlpha);
                }
                yield return 0;
            }

            yield return new WaitForSeconds(0.5f);
            yield return StartCoroutine(Fade(obj));

        }
    }
    public IEnumerator Fade(GameObject obj)
    {
        if (obj != null && obj.CompareTag("BgEmerge"))
        {
            SpriteRenderer sp = obj.GetComponent<SpriteRenderer>();
            for (; curAlpha > -0.5f; curAlpha -= Time.deltaTime * varifySpeed)
            {
                sp.color = new Color(1f, 1f, 1f, curAlpha);
                yield return 0;
            }
            ifCoroutine = false;
            curAlpha = 0f;
        }
        else if (obj != null)
        {
            TextMesh tm = obj.GetComponentInChildren<TextMesh>();
            
            for (; curAlpha > -0.5f; curAlpha -= Time.deltaTime * varifySpeed)
            {
                if (obj != null && obj.CompareTag("res"))
                    tm.color = new Color(1f, 1f, 1f, curAlpha);
                else if (obj != null && obj.CompareTag("onlyTrigger"))
                {
                    tm.color = new Color(1f, 1f, 1f, curAlpha);
                }
                else if (obj != null && obj.CompareTag("NPC"))
                {
                    tm.color = new Color(0f, 0f, 0f, curAlpha);
                    obj.transform.Find("NPCduihuakuang").GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, curAlpha);
                }
                else if (obj != null && obj.CompareTag("EventText"))
                {
                    tm.color = new Color(1f, 1f, 1f, curAlpha);
                }
                yield return 0;
            }

            ifCoroutine = false;
            curAlpha = 0f;
        }
    }
	public void BridgeDown ()
	{
		Debug.Log("BridgeDown");
		GameObject BridgeBoard = GameObject.Find ("BridgeBoard");
		Rigidbody2D BridgeBoard_Rigidbody2D = BridgeBoard.GetComponent<Rigidbody2D> ();
		BridgeBoard_Rigidbody2D.freezeRotation = false;
		BridgeBoard_Rigidbody2D.AddForce (new Vector2 (-2f, 0));
		BridgeBoard.GetComponent<BoxCollider2D> ().isTrigger = false;
		ifBridgeDown = true;
		return;
	}
}

 
public class resList
{
    //chapter1
    public Res FireCreaters;
    public Res Ability1;
    public Res RedPocket;
    public Res Dumplings;
    public Res fu;
    //chapter2
    public Res Ability2;
    public Res Seed;
    public Res Yu;
    public Res Wrench;
    public Res zanzi;
    //chapter3
    public Res Zongzi;
    public Res Zongye;
    public Res Meat;
    public Res Rice;
    public Res XiongHuangFen;
    public Res XiongHuangJiu;
	public Res Ability3;
    // private Vector2 headPoint;
    public resList()
    {
        //chapter1
        FireCreaters = new Res(0, 3);
		Ability1 = new Res(0, 1);
        Ability3 = new Res(0, 1);
        RedPocket = new Res(0, 1);
        Dumplings = new Res(0, 1);
        Seed = new Res(0, 1);
        fu = new Res(0, 1);
        //chapter2
        Yu = new Res(0, 1);
        Ability2 = new Res(0, 1);
        Wrench = new Res(0, 1);
        zanzi = new Res(0, 1);
        //chapter3
        Zongzi = new Res(0, 2);
        Zongye = new Res(0, 1);
        Meat = new Res(0, 1);
        Rice = new Res(0, 1);
        XiongHuangFen = new Res(0, 1);
        XiongHuangJiu = new Res(0, 1);
    }
}
// 物品组信息的基类
public class Res
{
    public int numOfRes;
    private int maxOfRes;
    public Res(int numHad, int numMax)
    {
        numOfRes = numHad;
        maxOfRes = numMax;
    }
    public bool ifmorethan1
    {
        get
        {
            return maxOfRes >= 1;
        }
    }

    public int num
    {
        get
        {
            return numOfRes;
        }
    }
    public bool empty
    {
        get
        {
            return numOfRes == 0;
        }
    }
    public int fullNum
    {
        set
        {
            maxOfRes = value;
        }
    }
    
    public bool full
    {
        get
        {
            return numOfRes == maxOfRes;
        }
    }
    public void getRes(GameObject o)
    {
        if (this.empty)
        {
            // obj = (GameObject)GameObject.Instantiate(o);
        }
        ++numOfRes;
        numOfRes = System.Math.Min(numOfRes, maxOfRes);
    }
    public void getRes()
    {
        ++numOfRes;
        numOfRes = System.Math.Min(numOfRes, maxOfRes);
    }
    public void clear()
    {
        numOfRes = 0;
    }
    // public GameObject obj;
}


