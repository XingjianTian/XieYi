using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SolidTalk : MonoBehaviour {
    bool ifGirlGoOn = false;
    bool ifgetXiongHuangFen = false;
    bool ifgetzanzi = false;
    public List<string> juqing;

	public GameObject shield;//护盾
	public GameObject came;
	public CameraMoveWithPlayer cmwp;
    //主角对话框
    public GameObject player;
    public PlayerControl pc;
    ResPutUp rpu;
    public GameObject tm;//对话框文字
    public Text tmt;
    public GameObject tkm;//对话框本身
    public Image tkms;
    private Color duihuakuangColor;
    private Color wenziColor;
    float minAlpha = 0f;
    float maxAlpha = 1f;
    public float varifySpeed = 1f;
    public float curPlayerAlpha = 0f;

    //npc对话框
    Animator NPC_Anim;
    public GameObject npc;
    public float curNpcAlpha = 0f;
    public TextMesh Npctm;

    public bool ifOnce = false;
    public bool ifCoroutine = false;
    public bool ifTriggered = false;

    public bool ifFlip = false;
    public bool ifshieldOn = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.name == "NewHero"&&!ifTriggered)
        {
            ifTriggered = true;
        }
    }

	private string[] WarningLines = new string[] { 
		"0等等！！！", 
		"0如果你一定\n要继续前进的话", 
		"0请带上这个\n它会帮助你的" 
	};
	private string[] GirlLines = new string[] {
		"1这里是什么地方？\n你为什么在这里哭泣?",
		"0我来这里思念\n我死去的父亲",
		"0这里，是离阴间\n最近的地方",
		"1所以说我脚下的\n倒影...是阴间？",
		"0对，凡人如果下去\n就会变成这副样子",
		"0不知道我的父亲\n在下面过得好不好",
		"0可惜父亲走时\n太匆忙，太匆忙",
		"0家传的玉佩\n也不慎遗失",
		"0没留下什么信物\n以睹物思人",
		"0我只得望着这\n潺潺冥河水",
		"0兴许哪天能望见\n阴阳两隔的父亲",
		"1等等...玉佩？\n你说的是这个吗？",
		"0你怎么会有这个！",
		"1我在路边捡到的...",
		"0.........",
		"1.........",
		"0父亲乱丢东西的\n毛病还真是没变",
		"0等等！你是不是\n还有什么东西！",
		"1来这里之前，有个\n神秘人给了我这个",
		"0这是能让人安全\n进入冥界的灵石！",
		"0我来帮你启用它!",
		"1哇！倒影消失了！",
		"0有这一层护盾，你\n就可以进入阴间了",
		"0...我有个\n不情之请...",
		"0你能帮我去看看\n我的父亲吗？",
		"0让他知道我过得\n很好，无需牵挂",
		"1可能这是天意吧\n我会转告令尊的",
		"0真的吗！太好了!\n我会为你祈祷的",
		"0请带上这个发簪\n父亲会认出它的"
	};
	private string[] FatherLines = new string[] {
		"1你好，你是...\n这儿的守卫吗？",
		"0不，我是看门人\n你不能从这儿回去",
		"0等等...\n",
		"0你身上是不是带了\n来自阳间的东西？",
		"1嗯...就是这个",
		"0这是...\n我女儿的发簪!",
		"0你怎么会有这个？\n她怎么了？！",
		"1她说她的父亲\n会认出这个发簪来",
		"1我是替她来向您\n传达她的思念的",
		"0噢...我的女儿\n谢谢你，小伙子",
		"0我因为在阳间还有\n挂念，不能转生",
		"0故被选作看门人\n一直守卫在此",
		"1您说的挂念是那块家传玉佩吧?",
		"0是啊，那是我们家\n祖传的玉佩啊",
		"0你是怎么知道的？",
		"1我在阳间路上捡到的",
		"0......",
		"1......",
		"0总之还是谢谢你\n解了我的心结",
		"0现在让我为你\n开放回去的门吧"
	};

    private string[] Villager1Lines = new string[]
    {
        "1你为什么看上去这么焦急？",
        "0我们敬爱的大人\n屈原不见了!",
        "0大家都在忙着找他",
        "0你也来帮忙吧！",
    };
	private string[] MinerLines = new string[]
	{
		"1打扰了，请问您有雄黄粉吗?",
		"0我辛勤挖一天矿石\n可不是为了白给你",
		"0嘿嘿，你得\n给我点好处才行",
		"0不过看你这样子\n要这雄黄粉作甚?",
		"1我在寻找屈原的路上，被蛇挡住了去路",
		"0啊，你是在帮忙\n找屈原啊",
		"0...",
		"0那我今天挖的这\n雄黄粉，就送你了",
		"0屈原这小子\n记他账上了",
		"0一定要找到他啊！",
		"1我会尽力的，谢谢你！",
	};
	private string[] SaverLines = new string[]
	{
		"1屈原先生在\n我们的船上！",
		"1先生哀国不古，\n竟欲跳江轻生...",
		"1快来救救他！",
		"0没想到先生竟然\n会如此想不开...",
		"0我们这就叫人来！",
		"0出口在那右边，\n你先回去吧...",
		"0此事\n多谢了",
	};
    private string[] ChuanfuLines = new string[]
    {
        "0唉，虽然我承包了\n这儿的船运业务",
        "0富甲一方\n却日夜操劳",
        "0都没时间吃上\n一口热粽子...",
    };
    private string[] JumpLines = new string[]
	{
		"1！！！",
		"0虽不周於今之人兮",
		"0愿依彭咸之遗则…",
		"1不好！他跳江了！",
		"1赶紧帮忙捞人！",
		"1幸好及时赶到，他\n看来只是昏过去了"
	};
	private string[] HongBaoLines = new string[]
	{
		"1诶？那里有个红包…",
		"0小伙子…过年\n不能光想着红包啊",
		"0家人的团聚\n才是最重要的啊…",
		"1(总觉得他在提示\n我些什么)",
		"1嗯，我知道了"
	};
	private string[] FireCraker3Lines = new string[]
	{
		"1诶…这群孩子\n还不知道收好鞭炮",
		"1还要我来帮他们\n收拾摊子",
		"0阿嚏！~\n谁在说我……"
	};

    // Use this for initialization
    void Start () {

        if (this.name == "SolidTalkPoint_Warning")
			foreach (string line in WarningLines)
				juqing.Add (line);
        else if (name == "SolidTalkPoint_Girl")
			foreach (string line in GirlLines)
				juqing.Add (line);
        else if (name == "SolidTalkPoint_Father")
			foreach (string line in FatherLines)
				juqing.Add (line);
        else if (name == "SolidTalkPoint_LookForQu")
            foreach (string line in Villager1Lines)
                juqing.Add(line);
		else if (name == "SolidTalkPoint_Miner")
			foreach (string line in MinerLines)
				juqing.Add(line);
		else if (name == "SolidTalkPoint_Saver")
			foreach (string line in SaverLines)
				juqing.Add(line);
        else if (name == "SolidTalkPoint_Chuanfu")
            foreach (string line in ChuanfuLines)
                juqing.Add(line);
        else if (name == "SolidTalkPoint_JumpIntoRiver")
			foreach (string line in JumpLines)
				juqing.Add(line);
		else if (name == "SolidTalkPoint_HongBao")
			foreach (string line in HongBaoLines)
				juqing.Add(line);
		else if (name == "SolidTalkPoint_FireCraker3")
			foreach (string line in FireCraker3Lines)
				juqing.Add(line);
		

        player = GameObject.Find("NewHero");
        tkm = GameObject.Find("duihuakuang");
        tm = GameObject.Find("TextMind");
        tkms = tkm.GetComponent<Image>();
        tmt = tm.GetComponent<Text>();
        pc = player.GetComponent<PlayerControl>();
        rpu = player.GetComponent<ResPutUp>();
        Npctm = npc.GetComponentInChildren<TextMesh>();
        NPC_Anim = npc.GetComponent<Animator>();

		came = GameObject.Find("Main Camera");
		cmwp = came.GetComponent<CameraMoveWithPlayer>();

    }
    bool once = true;
	// Update is called once per frame
	void Update () {
        //警告者
        #region
        if (this.name == "SolidTalkPoint_Warning")
        {
            if (ifTriggered && !ifOnce)
            {
                // 关闭摇晃的两个东西
                //Destroy(GameObject.Find("wood"));
                //Destroy(GameObject.Find("BridgeBoard"));
                GameObject.Find("wood").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                GameObject.Find("BridgeBoard").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

                pc.allowable = false;
                pc.anim.Play("Idle", -1, 0f);
                pc.rig.velocity = new Vector2(0, 0);
                player.GetComponent<ResPutUp>().enabled = false;
                if (juqing.Count > 0)
                {
                    if (!ifCoroutine)
                        StartCoroutine(Emerge(juqing[0]));
                }

                if (npc.transform.position.x + 6.25f >= 0.05f)
                {
                    npc.transform.position = Vector3.MoveTowards(npc.transform.position, new Vector3(-6.25f, -2.15f, 0), 0.02f);
                    NPC_Anim.SetTrigger("NPCWalk");
                }
                else if (npc.transform.position.x + 7.1f >= 0.05f)
                {
                    if (once)
                    {
                        player.GetComponent<SpriteRenderer>().flipX = !player.GetComponent<SpriteRenderer>().flipX;
                        pc.facingRight = false;
                    }
                    once = false;
                    npc.transform.position = Vector3.MoveTowards(npc.transform.position, new Vector3(-7.1f, -2.15f, 0), 0.02f);
                    NPC_Anim.SetTrigger("NPCWalk");
                }
                else
                {
                    NPC_Anim.SetTrigger("NPCIdle");
                    NPC_Anim.speed = 0;
                }

            }


            if (juqing.Count == 0 && !ifCoroutine)
                ifOnce = true;

            if (ifOnce)
            {
                Npctm.text = "请一定保重！";
                player.GetComponent<ResPutUp>().enabled = true;
                player.GetComponent<ResPutUp>().showGetRes("你获得了 神秘能力石");
                player.GetComponent<ResPutUp>().reses.Ability2.getRes();
                pc.allowable = true;
                npc.GetComponent<BoxCollider2D>().enabled = true;
                Destroy(this);
            }
        }
        #endregion
        //女孩
        #region
        else if (name == "SolidTalkPoint_Girl"&&(!rpu.reses.Yu.empty||ifGirlGoOn))
        {
            ifGirlGoOn = true;
            if (ifTriggered && !ifOnce)
            {
                pc.allowable = false;
                pc.anim.Play("Idle", -1, 0f);
                pc.rig.velocity = new Vector2(0, 0);
                player.GetComponent<ResPutUp>().enabled = false;
                if (juqing.Count > 0)
                {
                    if (!ifCoroutine)
                        StartCoroutine(Emerge(juqing[0]));
                }

            }

            if (juqing.Count == 26 && !ifFlip)
            {
                npc.GetComponent<SpriteRenderer>().flipX = !npc.GetComponent<SpriteRenderer>().flipX;
                ifFlip = true;
            }
            
            if (juqing.Count == 16)
            {
                player.GetComponent<ResPutUp>().reses.Yu.clear();
            }

            if (juqing.Count == 8 && !ifshieldOn)
            {
                shield.GetComponent<MeshRenderer>().enabled = true;
                shield.GetComponent<DynamicLight>().enabled = true;
                ifshieldOn = true;
                pc.ifshieldOn = true;
                Destroy(GameObject.Find("ShadowHero(Clone)"));
            }

            if (juqing.Count == 1&&!ifgetzanzi)
            {
                player.GetComponent<ResPutUp>().showGetRes("你获得了 玉簪");
                player.GetComponent<ResPutUp>().reses.zanzi.getRes();
                ifgetzanzi = true;
            }


            if (juqing.Count == 0 && !ifCoroutine)
                ifOnce = true;

            if (ifOnce)
            {
                Npctm.text = "前路凶险\n万事小心";
                player.GetComponent<ResPutUp>().enabled = true;
                pc.allowable = true;
                Destroy(this);
            }
        }
        #endregion
        //父亲
        #region
        else if (name == "SolidTalkPoint_Father")
        {
            if (ifTriggered && !ifOnce)
            {
                pc.allowable = false;
                pc.anim.Play("Idle", -1, 0f);
                pc.rig.velocity = new Vector2(0, 0);
                player.GetComponent<ResPutUp>().enabled = false;
                if (juqing.Count > 0)
                {
                    if (!ifCoroutine)
                        StartCoroutine(Emerge(juqing[0]));
                }

            }
            if (juqing.Count == 0 && !ifCoroutine)
                ifOnce = true;
            if (ifOnce)
            {
                Npctm.text = "如果你还能见到她\n告诉她，我很想她";
                player.GetComponent<ResPutUp>().enabled = true;
                pc.allowable = true;
                Destroy(this);
            }
        }
        #endregion
        //小村民
        #region 
        else if (name == "SolidTalkPoint_LookForQu")
        {
            if (ifTriggered && !ifOnce)
            {
                //NPC_Anim.SetTrigger("NPCIdle");
                //NPC_Anim.speed = 0;
                pc.allowable = false;
                pc.anim.Play("Idle", -1, 0f);
                pc.rig.velocity = new Vector2(0, 0);
                player.GetComponent<ResPutUp>().enabled = false;
                if (juqing.Count > 0)
                {
                    if (!ifCoroutine)
                        StartCoroutine(Emerge(juqing[0]));
                }

            }
            if (juqing.Count == 0 && !ifCoroutine)
                ifOnce = true;
            if (ifOnce)
            {
                //NPC_Anim.speed = 1;
                Npctm.text = "希望屈原没有\n发生什么事...";
                player.GetComponent<ResPutUp>().enabled = true;
                pc.allowable = true;
                Destroy(this);
            }
        }
        #endregion
		//矿工
		#region 
		else if (name == "SolidTalkPoint_Miner")
		{
			if (ifTriggered && !ifOnce)
			{
				npc.GetComponent<SpriteRenderer>().flipX = false;
				//NPC_Anim.SetTrigger("NPCIdle");
				NPC_Anim.speed = 0;

				pc.allowable = false;
				pc.anim.Play("Idle", -1, 0f);
				pc.rig.velocity = new Vector2(0, 0);

				player.GetComponent<ResPutUp>().enabled = false;
				if (juqing.Count > 0)
				{
					if (!ifCoroutine)
						StartCoroutine(Emerge(juqing[0]));
				}

			}
			if (juqing.Count == 10 && !ifFlip)
			{
				npc.GetComponent<SpriteRenderer>().flipX = !npc.GetComponent<SpriteRenderer>().flipX;
				ifFlip = true;
			}
			if (juqing.Count == 3&&!ifgetXiongHuangFen)
			{
				player.GetComponent<ResPutUp>().showGetRes("你获得了 雄黄粉");
				player.GetComponent<ResPutUp>().reses.XiongHuangFen.getRes();
				ifgetXiongHuangFen = true;
			}
			if (juqing.Count == 0 && !ifCoroutine)
				ifOnce = true;
			if (ifOnce)
			{
				//NPC_Anim.speed = 1;
				Npctm.text = "希望屈原没事\n大家都很喜欢他";
                npc.GetComponent<BoxCollider2D>().enabled = true;
				player.GetComponent<ResPutUp>().enabled = true;
				pc.allowable = true;
				Destroy(this);
			}
		}
        #endregion
        //船夫
        #region 
        else if (name == "SolidTalkPoint_Chuanfu")
        {
            if (ifTriggered && !ifOnce)
            {
                //NPC_Anim.SetTrigger("NPCIdle");
                //NPC_Anim.speed = 0;
                pc.allowable = false;
                pc.anim.Play("Idle", -1, 0f);
                pc.rig.velocity = new Vector2(0, 0);
                player.GetComponent<ResPutUp>().enabled = false;
                if (juqing.Count > 0)
                {
                    if (!ifCoroutine)
                        StartCoroutine(Emerge(juqing[0]));
                }

            }
            if (juqing.Count == 0 && !ifCoroutine)
                ifOnce = true;
            if (ifOnce)
            {
                //NPC_Anim.speed = 1;
                Npctm.text = "啊,好想吃粽子啊";
                player.GetComponent<ResPutUp>().enabled = true;
                pc.allowable = true;
                Destroy(this);
            }
        }
        #endregion
        //救援
        #region 
        else if (name == "SolidTalkPoint_Saver")
		{
			if (ifTriggered && !ifOnce)
			{
				npc.GetComponent<SpriteRenderer>().flipX = false;
				//NPC_Anim.SetTrigger("NPCIdle");
				//NPC_Anim.speed = 0;
				pc.allowable = false;
				pc.anim.Play("Idle", -1, 0f);
				pc.rig.velocity = new Vector2(0, 0);
				player.GetComponent<ResPutUp>().enabled = false;
				if (juqing.Count > 0)
				{
					if (!ifCoroutine)
						StartCoroutine(Emerge(juqing[0]));
				}

			}
			if (juqing.Count == 0 && !ifCoroutine)
				ifOnce = true;
			if (ifOnce)
			{
				GameObject.Find("Duan").transform.position = new Vector2(GameObject.Find("Duan").transform.position.x, 0f);
				//NPC_Anim.speed = 1;
				Npctm.text = "此事多谢…";
				player.GetComponent<ResPutUp>().enabled = true;
				pc.allowable = true;
				Destroy(this);
				GameObject block = GameObject.Find("Block2");
				block.GetComponent<SpriteRenderer>().enabled = true;
				block.GetComponent<BoxCollider2D>().enabled = true;

				GameObject door = GameObject.Find("SceneDoor");
				foreach (Transform child in door.transform) {
					if (child.name == "SceneDoor3") {
						child.gameObject.GetComponent<SpriteRenderer>().enabled = true;
						child.gameObject.GetComponent<ScenePortal>().enabled = true;
						child.gameObject.GetComponent<BoxCollider2D>().enabled = true;
					}
					else if (child.name == "DoorBase") {
						child.gameObject.GetComponent<SpriteRenderer>().enabled = true;
					}
				}
			}
		}
		#endregion
		//屈原
		#region 
		else if (name == "SolidTalkPoint_JumpIntoRiver")
		{
			if (ifTriggered && !ifOnce)
			{
				//NPC_Anim.SetTrigger("NPCIdle");
				//NPC_Anim.speed = 0;
				pc.allowable = false;
				pc.anim.Play("Idle", -1, 0f);
				pc.rig.velocity = new Vector2(0, 0);
				player.GetComponent<ResPutUp>().enabled = false;
				if (juqing.Count > 0)
				{
					if (!ifCoroutine)
						StartCoroutine(Emerge(juqing[0]));
				}

			}
			if (juqing.Count == 5 && once)
			{
				once = false;
				StartCoroutine(JumpIntoRiver());
			}
			else if (juqing.Count == 0 && !ifCoroutine)
				ifOnce = true;
			if (ifOnce)
			{
				//NPC_Anim.speed = 1;
				player.GetComponent<ResPutUp>().enabled = true;
				Destroy(this);
			}
		}
		#endregion
		//红包
		#region 
		else if (name == "SolidTalkPoint_HongBao")
		{
			if (ifTriggered && !ifOnce)
			{
				pc.allowable = false;
				pc.anim.Play("Idle", -1, 0f);
				pc.rig.velocity = new Vector2(0, 0);
				player.GetComponent<ResPutUp>().enabled = false;

				if (juqing.Count > 0)
				{
					if (!ifCoroutine)
						StartCoroutine(Emerge(juqing[0]));
				}

			}
			if (juqing.Count == 0 && !ifCoroutine)
				ifOnce = true;
			if (ifOnce)
			{
				player.GetComponent<ResPutUp>().enabled = true;
				player.GetComponent<ResPutUp>().reses.RedPocket.getRes();
                player.GetComponent<ResPutUp>().showGetRes("你获得了 红包");

                pc.allowable = true;
				Destroy(this);
			}
		}
		#endregion
		//爆竹3
		#region 
		else if (name == "SolidTalkPoint_FireCraker3")
		{
			if (ifTriggered && !ifOnce)
			{
				NPC_Anim.SetTrigger("NPCIdle");
				NPC_Anim.speed = 0;
				pc.allowable = false;
				pc.anim.Play("Idle", -1, 0f);
				pc.rig.velocity = new Vector2(0, 0);
				player.GetComponent<ResPutUp>().enabled = false;
				if (juqing.Count > 0)
				{
					if (!ifCoroutine)
						StartCoroutine(Emerge(juqing[0]));
				}

			}
			if (juqing.Count == 0)
			{
				GameObject cam = GameObject.Find("Main Camera");
				cam.GetComponent<CameraMoveWithPlayer>().changeFollowObject(npc);
			}
			if (juqing.Count == 0 && !ifCoroutine)
				ifOnce = true;
			if (ifOnce)
			{
				NPC_Anim.speed = 1;
				player.GetComponent<ResPutUp>().enabled = true;
				pc.allowable = true;
				Destroy(this);
			}
		}
		#endregion
    }

    public IEnumerator Emerge(string onejuzi)
    {
        GameObject obj = onejuzi[0] == '0' ? npc : player;

        ifCoroutine = true;
        if (obj != null)
        {
            if (obj.CompareTag("NPC"))
            {
                Npctm.text = onejuzi.Remove(0, 1);
                juqing.RemoveAt(0);
                for (; curNpcAlpha < 1; curNpcAlpha += Time.deltaTime * varifySpeed)
                {
                    if (obj != null)
                    {
                        Npctm.color = new Color(0f, 0f, 0f, curNpcAlpha);
                        obj.transform.Find("NPCduihuakuang").GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, curNpcAlpha);
                    }
                    yield return 0;
                }
            }
            else if(obj.CompareTag("Player"))
            {
                tmt.text = onejuzi.Remove(0, 1);
                juqing.RemoveAt(0);
                for (; curPlayerAlpha < 1;curPlayerAlpha+=Time.deltaTime*varifySpeed)
                {
                    if (obj != null)
                    {
                        //对话框和文字渐显示
                        duihuakuangColor = tkms.color;
                        wenziColor = tmt.color;

                        duihuakuangColor.a = curPlayerAlpha;
                        wenziColor.a = curPlayerAlpha;

                        tkms.color = duihuakuangColor;
                        tmt.color = wenziColor;
                    }
                    yield return 0;
                }
            }
            yield return new WaitForSeconds(0.5f);
            yield return StartCoroutine(Fade(obj));
        }
    }
    public IEnumerator Fade(GameObject obj)
    {
        if (obj != null)
        {
            if (obj.CompareTag("NPC"))
            {
                for (; curNpcAlpha > -0.5f; curNpcAlpha -= Time.deltaTime * varifySpeed)
                {
                    if (obj != null)
                    {
                        Npctm.color = new Color(0f, 0f, 0f, curNpcAlpha);
                        obj.transform.Find("NPCduihuakuang").GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, curNpcAlpha);
                    }
                    yield return 0;
                }
                curNpcAlpha = 0;

            }
            else if (obj.CompareTag("Player"))
            {
                for (; curPlayerAlpha > -0.5f; curPlayerAlpha -= Time.deltaTime * varifySpeed)
                {
                    if (obj != null)
                    {
                        //对话框和文字渐显示
                        duihuakuangColor = tkms.color;
                        wenziColor = tmt.color;

                        duihuakuangColor.a = curPlayerAlpha;
                        wenziColor.a = curPlayerAlpha;

                        tkms.color = duihuakuangColor;
                        tmt.color = wenziColor;
                    }
                    yield return 0;
                }
                curPlayerAlpha = 0;
            }
            ifCoroutine = false;
            
        }

    
    }
	IEnumerator JumpIntoRiver()
	{
		GameObject boat = GameObject.Find ("Boat");
		boat.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezePositionX;
		yield return new WaitForSeconds (3);
		GameObject qy = GameObject.Find ("NPC_QuYuan");
		cmwp.ifMoveWithOther = true;
		cmwp.ifMoveWithPlayer = false;
		cmwp.FollowOther = qy;
		yield return new WaitForSeconds (7);

		if (!qy.GetComponent<Rigidbody2D> ().simulated) {
			qy.GetComponent<Rigidbody2D> ().simulated = true;
			yield return new WaitForSeconds (0.75f);
			GameObject.Find ("WaterManager").GetComponent<Water> ().Splash (qy.transform.position.x, -0.25f);
			yield return new WaitForSeconds (2);
			boat.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.None;
			yield return new WaitForSeconds (2);
			qy.GetComponent<Rigidbody2D> ().simulated = false;
			qy.transform.rotation = Quaternion.Euler(0, 0, 90);
			qy.GetComponent<FollowtheBoat> ().enabled = true;
		}
		boat.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.None;
		yield return 0;
	}
    //主角对话

}
