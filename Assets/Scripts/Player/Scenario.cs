using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scenario : MonoBehaviour
{
    public bool ifSeeSnake = false;
    public GameObject ShadowHeroPrefab;
    public GameObject explosion;
    public GameObject came;
    public CameraMoveWithPlayer cmwp;
    public bool ifeventonce = true;
    public GameObject player;
    public ResPutUp rpu;
    public PlayerControl pc;
    public GameObject dumplings;
    public GameObject Fuzi;
    public float rotatePerFrame = 0.8f;
    public float NowRotation;
    public string script = "";
    public bool iffade = false;
    public bool ifemerge = false;
    public bool iftriggered = false;
    public bool ifshowed = false;
    public GameObject scenedoor;
    public GameObject WholeMap;
    public GameObject tm;//对话框文字
    public Text tmt;
    public GameObject tkm;//对话框本身
    //public SpriteRenderer tkms;
    public Image tkms;
    private Color duihuakuangColor;
    private Color wenziColor;
    // Use this for initialization
    void Start()
    {
        came = GameObject.Find("Main Camera");
        player = GameObject.Find("NewHero");
        rpu = player.GetComponent<ResPutUp>();
        WholeMap = GameObject.Find("BackGround");
        tkm = GameObject.Find("duihuakuang");
        tm = GameObject.Find("TextMind");
        scenedoor = GameObject.Find("SceneDoor");
        tkms = tkm.GetComponent<Image>();
        tmt = tm.GetComponent<Text>();
        cmwp = came.GetComponent<CameraMoveWithPlayer>();
        pc = player.GetComponent<PlayerControl>();
        //duihuakuangmaterial = tkm.GetComponent<SpriteRenderer>().material;
        //GetComponent<SpriteRenderer>().color = new Color(256, 256, 256, 0);//检测体本身透明
    }

    // Update is called once per frame
    //public Material duihuakuangmaterial;

    float minAlpha = 0f;
    float maxAlpha = 1f;
    float varifySpeed = 1f;
    public float curAlpha = 0f;
    // Use this for initialization


    void Update()
    {

        if (iftriggered)
        {
            if (ifemerge && !ifshowed&&script!="")
            {
                ScenarioEmerge();
            }
            if (iffade && script != "")
            {
                ScenarioFade();
            }
            //Chapter1
#region
            if (name == "RotationEventPoint")
            {
                pc.ifUpSideDown = true;
                /*
                Destroy(GameObject.Find("NPCs"));
                Destroy(GameObject.Find("Monster"));
                Destroy(GameObject.Find("fuzi(Clone)"));
                Destroy(GameObject.Find("Dumplings(Clone)"));*/
                NowRotation = player.transform.localEulerAngles.z;
                if (NowRotation < 180f)
                {
                    //rotation += Time.deltaTime;
                    came.transform.Rotate(0, 0, rotatePerFrame);
                    player.transform.Rotate(0, 0, rotatePerFrame);
                }
                else if (NowRotation >= 180f && ifshowed)
                {
                    iftriggered = false;
                }
               
            }

            else if (name == "SeeTheMonsterEventPoint")
            {
                GameObject.Find("CannonEventPoint").GetComponent<BoxCollider2D>().enabled = true;
            }
            else if (name == "ThrowDumplingsEventPoint")
            {
#if UNITY_ANDROID
                script= "在这里把饺子丢下去吸引它(按'■'键)";
#endif
                if (ifeventonce)
                {
                    if (ifshowed)
                    {
                        if ((Input.GetKeyDown("e") || rpu.if_E_Pressed)&& !pc.facingRight)
                        {
                            AudioSource.PlayClipAtPoint(pc.GetComponent<PlayerControl>().Events[1], transform.position);
                            Invoke("ThrowDumplings", 0.1f);
                            ifeventonce = false;
                        }
                    }
                }
            }
            else if (name == "SeeChunlianEventPoint")
            {
                if (ifeventonce)
                {
                    
                    if (pc.grounded)
                    {
                        pc.rig.velocity = Vector2.zero;
                        pc.anim.Play("Idle", -1, 0f);
                        pc.anim.speed = 0;
                        pc.enabled = false;
                    }
                    if (ifshowed&&pc.enabled==false)
                    {
                        GameObject chunlian = GameObject.Find("chunlian");
                        came.GetComponent<Camera>().orthographicSize = 3f;
                        cmwp.changeToSolidPoint(chunlian.transform.position, true);
                        ifeventonce = false;
                        pc.enabled = true;
                        player.GetComponent<Animator>().enabled = true;
                    }
                }
            }
            else if (name== "PutFuEventPoint")
            {
#if UNITY_ANDROID
                script = "把福字贴回去吧(按'■'键)";
#endif
                if (ifeventonce)
                {
                    if (ifshowed)
                    {
                        if ((Input.GetKeyDown("e") || rpu.if_E_Pressed) && pc.facingRight)
                        {
                            AudioSource.PlayClipAtPoint(pc.GetComponent<PlayerControl>().Events[2], transform.position);
                            StartCoroutine(FuziFunc());
                            ifeventonce = false;
                        }
                    }
                }
            }
            else if(name=="SeeFireEventPoint")
            {
                if (ifeventonce)
                {

                    pc.rig.velocity = Vector2.zero;
                    pc.anim.Play("Idle", -1, 0f);
                    pc.anim.speed = 0;
                    pc.allowable = false;
                    if (!ifshowed&& !pc.allowable)
                    {
                        //player.GetComponent()
                        
                        GameObject Fire = GameObject.Find("FireCrakerBomb");
                        cmwp.changeToSolidPoint(Fire.transform.position, true);
                        Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
                        explosion.transform.localScale = new Vector3(0.15f,0.15f,1f);
                        StartCoroutine(rpu.Emerge(GameObject.Find("NPC1 (2)")));
                        // Instantiate the explosion where the rocket is with the random rotation.
                        StartCoroutine(FireBomb(Fire.transform.position,randomRotation));
                            ifeventonce = false;
                        player.GetComponent<ResPutUp>().enabled = true;
                    }
                }
            }
            else if(name=="InChapter1EventPoint")
            {
                GameObject.Find("GameManager").GetComponent<Manager2>().co = ChapterOrder.Chapter1;
                Destroy(this);
            }
            #endregion

            //Chapter2
            #region
            else if (name == "SeedShouldHereEventPoint1")
            {
#if UNITY_ANDROID
                script = "树上好像挂着一个秋千…(按'■'键)";
#endif
				GameObject.Find("Tree").GetComponent<spring>().enabled = true;
            }
            else if (name == "SeedShouldHereEventPoint2")
            {
#if UNITY_ANDROID
                script = "可以用秋千荡过去?(按'■'键)";
#endif
                
            }
            else if(name== "IntoDarknessEventPoint")
            {
                came.GetComponent<Camera>().backgroundColor = new Color(10/255f,10/255f,10/255f,1);
            }
            else if (name == "GotWrenchEventPoint")
            {
                Destroy(GameObject.Find("IntoDarknessEventPoint"));
                came.GetComponent<Camera>().backgroundColor = new Color(43 / 255f, 82 / 255f, 92 / 255f, 1);
            }
            else if(name == "CreateShadowHeroEventPoint")
            {
                
                GameObject ShadowHero = Instantiate(ShadowHeroPrefab, new Vector3(player.transform.position.x, 2f * 0.90575f - player.transform.position.y, 0), Quaternion.identity);
                ifeventonce = false;
                Destroy(this);
            }
            else if(name == "RotationEventPoint2")
            {
                pc.ifUpSideDown = false;
                NowRotation = player.transform.localEulerAngles.z;
                if (NowRotation <= 360f&&NowRotation-180f>=0)
                {
                    //rotation += Time.deltaTime;
                    came.transform.Rotate(0, 0, rotatePerFrame);
                    player.transform.Rotate(0, 0, rotatePerFrame);
                }
                else if (player.transform.localEulerAngles.z == 0f&& ifshowed)
                {
                    iftriggered = false;
                }
                GameObject.Find("NPC_Ghost1").GetComponent<SpriteRenderer>().enabled = true;
                GameObject.Find("GroundSign").GetComponent<SpriteRenderer>().enabled = true;
                GameObject.Find("RoadSign").GetComponent<SpriteRenderer>().enabled = true;
               // GameObject.Find("Tombs").GetComponentInChildren<SpriteRenderer>().enabled = true;
                SpriteRenderer[] allChildren = GameObject.Find("Tombs").GetComponentsInChildren<SpriteRenderer>();
                foreach (var child in allChildren)
                {
                    child.enabled = true;
                }
                Physics2D.gravity = new Vector3(0, -6F, 0);
                GameObject.Find("CannotLeaveEventPoint").GetComponent<BoxCollider2D>().enabled = true;
                GameObject.Find("CannotLeaveAirWall").GetComponent<BoxCollider2D>().enabled = true;
            }
            else if(name == "DeathEventPoint")
            {
                player.GetComponent<DeathControl>().ifdead = true;
                came.GetComponent<CameraMoveWithPlayer>().ifMoveWithPlayer = false;
            }
            else if (name == "NoShieldEventPoint" && !pc.ifshieldOn)
            {

                player.GetComponent<DeathControl>().ifdead = true;
                came.GetComponent<CameraMoveWithPlayer>().ifMoveWithPlayer = false;
                Destroy(GameObject.Find("RotationEventPoint2"));
            }

            #endregion
            //Chapter3
            #region
            else if (name=="StartMakingZongZiEventPoint")
            {
                if (ifeventonce)
                {
                    //GameObject.Find("BGM_Audio").
                    AudioSource.PlayClipAtPoint(pc.GetComponent<PlayerControl>().Events[0], transform.position);
                    GameObject.Find("BGM_Audio").GetComponent<AudioSource>().clip=pc.GetComponent<PlayerControl>().Events[1];
                    GameObject.Find("BGM_Audio").GetComponent<AudioSource>().Play();
                    came.GetComponent<Camera>().orthographicSize = 1f;
                    cmwp.changeToSolidPoint(new Vector3(1.628f,0.87f,-2.5f), false);
                    ifeventonce = false;
                    //pc.enabled = true;
                    StartCoroutine(rpu.Emerge(GameObject.Find("Text_2")));
                    GameObject.Find("MakeZongziGame").GetComponent<MakeZongzi>().ifGameOn = true;
                    
                }
                
                //Destroy(this);
            }
            else if (name == "FinishedEventPoint")
            {
                if (ifeventonce)
                {
                    //GameObject.Find("BGM_Audio").
                    AudioSource.PlayClipAtPoint(pc.GetComponent<PlayerControl>().Events[0], transform.position);
                    ifeventonce = false;
                    //pc.enabled = true;
                    StartCoroutine(rpu.Emerge(GameObject.Find("Bg")));
                    StartCoroutine(rpu.Emerge(GameObject.Find("Text_2")));
                   

                }

                //Destroy(this);
            }
            else if(name== "ProcessSnakeEventPoint")
            {
                if(!ifSeeSnake && rpu.reses.XiongHuangJiu.empty)
                    StartCoroutine(SeeSnake());
                else if(!rpu.reses.XiongHuangJiu.empty)
                {
					GameObject.Find("ProcessSnakeEventPoint1").GetComponent<BoxCollider2D>().enabled = true;
                    if (ifeventonce)
                    {
                        if (ifshowed)
                        {
                            if ((Input.GetKeyDown("e") || rpu.if_E_Pressed) && !pc.facingRight)
                            {
                                //蛇受惊音效
                                AudioSource.PlayClipAtPoint(pc.GetComponent<PlayerControl>().Events[3], transform.position);
                                GameObject.Find("YouDianShe").GetComponent<Animator>().enabled = true;
                                //rpu.showGetRes("成功熏晕了蛇");
                                rpu.reses.XiongHuangJiu.clear();
                                GameObject.Find("AfterShockSnakeEventPoint").GetComponent<BoxCollider2D>().enabled = true;
                                Destroy(GameObject.Find("SeeSnakeAndReturn"));
                                ifeventonce = false;
                            }
                        }
                    }
                }
               
            }
			else if(name== "SeeCantUpEventPoint")
			{
				if (ifeventonce)
				{
					ifeventonce = false;
                    /*
					GameObject block = GameObject.Find("Block1");
					block.GetComponent<SpriteRenderer>().enabled = true;
					block.GetComponent<BoxCollider2D>().enabled = true;*/
					StartCoroutine(StoneDown(GameObject.Find("Duan")));
				}
			}
            else if(name== "GoToBoatEventPoint")
            {
                if (ifeventonce)
                {
                    pc.rig.velocity = new Vector2(0f, pc.rig.velocity.y);
                    ifeventonce = false;
                    if (!pc.facingRight)
                        pc.Flip();
                    player.GetComponent<waterPCtest>().gotoBoat();
                }
            }
			else if(name== "GetOffBoatEventPoint")
			{
				if (ifeventonce)
				{
					ifeventonce = false;
					GameObject.Find("NewHero").GetComponent<waterPCtest> ().allowable = true;
				}
			}
			else if(name== "SwitchEventPoint")
			{
				if (ifeventonce)
				{
					ifeventonce = false;
					GameObject.Find("Block").GetComponent<SpriteRenderer>().enabled = false;
					GameObject.Find("Block").GetComponent<BoxCollider2D>().enabled = false;
					transform.Rotate(new Vector3(0, 180, 0));
				}
			}
#endregion

        }

    }
    IEnumerator FireBomb(Vector2 pos, Quaternion randomRotation)
    {
        Instantiate(explosion, pos, randomRotation);
        yield return new WaitForSeconds(0.7f);
        Instantiate(explosion, new Vector2(pos.x + 0.4f, pos.y), randomRotation);
        yield return new WaitForSeconds(0.9f);
        Instantiate(explosion, new Vector2(pos.x - 0.4f, pos.y), randomRotation);

    }
    IEnumerator SeeSnake()
    {
        
        pc.allowable = false;
        ifSeeSnake = true;
        pc.Flip();
        player.GetComponent<Rigidbody2D>().AddForce(new Vector2(150f, 50f));
       
        pc.allowable = true;
        yield return 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")&&collision.name=="NewHero" && !ifshowed&&!iftriggered&&!ifemerge&&!iffade)
        {
            iftriggered = true;
            ifemerge = true;
        }
    }
    void ScenarioFade()
    {
        if (curAlpha > minAlpha)
        {
            curAlpha -= Time.deltaTime * varifySpeed;
            //if (curAlpha < maxAlpha)
            //  varifySpeed *= -1;

            //对话框和文字渐透明
            curAlpha = Mathf.Clamp(curAlpha, minAlpha, maxAlpha);
            duihuakuangColor = tkms.color;
            wenziColor = tmt.color;

            duihuakuangColor.a = curAlpha;
            wenziColor.a = curAlpha;

            tkms.color = duihuakuangColor;
            tmt.color = wenziColor;
            
            
            
        }
        else if (curAlpha <= minAlpha)
        {
            iffade = false;
            ifshowed = true;
        }
    }
    void ScenarioEmerge()
    {
        tm.GetComponent<Text>().text = script;
        if (script != "")
        {
            if (curAlpha < maxAlpha)
            {
                curAlpha += Time.deltaTime * varifySpeed;
                //if (curAlpha < maxAlpha)
                //  varifySpeed *= -1;

                //对话框和文字渐显示
                curAlpha = Mathf.Clamp(curAlpha, minAlpha, maxAlpha);
                duihuakuangColor = tkms.color;
                wenziColor = tmt.color;

                duihuakuangColor.a = curAlpha;
                wenziColor.a = curAlpha;

                tkms.color = duihuakuangColor;
                tmt.color = wenziColor;
            }
            else if (curAlpha >= maxAlpha)
            {
                Invoke("LastAndReadyToFade", 0.5f);
            }
        }
    }
    void LastAndReadyToFade()
    {
        ifemerge = false;
        iffade = true;
    }
    void ThrowDumplings()
    {
        Vector3 targetposition = new Vector3(player.transform.position.x - 0.05f, player.transform.position.y, 0);
        player.GetComponent<ResPutUp>().Reses.Dumplings.clear();
        GameObject dump = Instantiate(dumplings, targetposition, player.transform.rotation);
        dump.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 11);
        cmwp.changeFollowObject(dump);
    }


    IEnumerator FuziFunc()
    {
        GameObject fuzi = Instantiate(Fuzi, player.transform.position, player.transform.rotation);
        cmwp.changeFollowObject(fuzi);

        while (fuzi.transform.localPosition != new Vector3(23.7708f, 0.4588f, 0f)|| fuzi.transform.localScale.x < 1f)
         {
            if(fuzi.transform.localScale.x < 1f)
                fuzi.transform.localScale += new Vector3(Time.deltaTime, Time.deltaTime, 0);
            if(fuzi.transform.localPosition != new Vector3(23.7708f, 0.4588f, 0f))
                fuzi.transform.localPosition = Vector3.MoveTowards(fuzi.transform.localPosition, new Vector3(23.7708f, 0.4588f, 0f), Time.deltaTime);
            yield return 0;
         }
        cmwp.stopAllCoroutine();
        player.GetComponent<ResPutUp>().Reses.fu.clear();
        GameObject npc = GameObject.Find("NPC3");
        npc.transform.position = new Vector3(npc.transform.position.x-0.5f,npc.transform.position.y,npc.transform.position.z);
        npc.GetComponentInChildren<TextMesh>().text = "太感谢你了,春联\n的力量回来了！";
        GameObject.Find("Door1collider").GetComponent<BoxCollider2D>().enabled = true;
        GameObject.Find("DrinkWineEventPoint").GetComponent<Scenario>().script = "现在可以好好喝一碗了";
    }
   	


	IEnumerator StoneDown(GameObject stone)
	{
		while (stone.transform.position.y > -1.12f) {
			stone.transform.position = new Vector2(stone.transform.position.x, stone.transform.position.y - 0.05f);
			yield return new WaitForSeconds (0.02f);
		}
		yield return 0;
	}
}