using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowFollow : MonoBehaviour {
    public bool ifInSolidTalk = false;
    public int State = 0;
    public float varifySpeed = 0.2f;
    public float curAlpha = 0;
    public bool ifshow = true;
    public bool ifEmergeAll = false;
    public float MirrowLiney1 = 0.90575f;
    public float MirrowLiney2 = -1.201f;
    public bool ifJump = false;
    public bool LastFlip;
    public Vector2 playerPos;
    public PlayerControl pc;
    public GameObject player;
   public Animator ShadowAnim;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("NewHero");
        pc = player.GetComponent<PlayerControl>();
        LastFlip = pc.facingRight;
        ShadowAnim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

        if (player.transform.position.x >= -4.576f)
            ifshow = true;
        else
            ifshow = false;

            if (pc.facingRight != LastFlip)
            {
                //跟主角同步转身
                GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
                LastFlip = pc.facingRight;
            }

        if (ifshow&&State==0)
        {
            if(!ifEmergeAll)
                StartCoroutine(Emerge());

            
            //jump参数由pc里的输入传入，避免多余的输入判断
            //三动画对应三状态
            if (ifJump)
            {
                ShadowAnim.SetTrigger("HeroJump");
                playerPos = player.transform.position;
                this.transform.position = new Vector3(playerPos.x, 2 * MirrowLiney1 - playerPos.y, 0);
                if (System.Math.Abs(playerPos.y - 0.55375f) <= 0.0005f)
                    ifJump = false;
            }
            else if (!ifJump && pc.h != 0)
            {
                ShadowAnim.SetTrigger("HeroRun");
                playerPos = player.transform.position;
                this.transform.position = new Vector3(playerPos.x, 2 * MirrowLiney1 - playerPos.y, 0);
            }
            else if (pc.h == 0)
            {
                ShadowAnim.SetTrigger("HeroIdle");
                playerPos = player.transform.position;
                this.transform.position = new Vector3(playerPos.x, 2 * MirrowLiney1 - playerPos.y, 0);
            }
        }

        else if(ifshow&&State==1)
        {
            if (ifJump)
            {
                ShadowAnim.SetTrigger("HeroJump");
                if (!ifInSolidTalk)
                {
                    ShadowAnim.Play("HeroIdle", -1, 0f);
                    ShadowAnim.speed = 0;
                }
                playerPos = player.transform.position;
                this.transform.position = new Vector3(playerPos.x, 2 * MirrowLiney2 - playerPos.y, 0);
                if (System.Math.Abs(playerPos.y - 0.55375f) <= 0.0005f)
                    ifJump = false;
            }
            else if (!ifJump && pc.h != 0)
            {
                ShadowAnim.SetTrigger("HeroRun");
                if (!ifInSolidTalk)
                {
                    ShadowAnim.Play("HeroIdle", -1, 0f);
                    ShadowAnim.speed = 0;
                }
                playerPos = player.transform.position;
                this.transform.position = new Vector3(playerPos.x, 2 * MirrowLiney2 - playerPos.y, 0);
            }
            else if (pc.h == 0)
            {
                ShadowAnim.SetTrigger("HeroIdle");
                if (!ifInSolidTalk)
                {
                    ShadowAnim.Play("HeroIdle", -1, 0f);
                    ShadowAnim.speed = 0;
                }
                playerPos = player.transform.position;
                this.transform.position = new Vector3(playerPos.x, 2 * MirrowLiney2 - playerPos.y, 0);
            }
        }

        else if(!ifshow)
        {
            StartCoroutine(Fade());
        }
        
        
	}

    IEnumerator Emerge()
    {
        SpriteRenderer sp = GetComponent<SpriteRenderer>();
        for (; curAlpha < 1; curAlpha += Time.deltaTime * varifySpeed)
        {
                sp.color = new Color(1f, 1f, 1f, curAlpha);
            yield return 0;
        }

        ifEmergeAll = true;
    }
    IEnumerator Fade()
    {
        SpriteRenderer sp = GetComponent<SpriteRenderer>();
        for (; curAlpha > -0.5f; curAlpha -= Time.deltaTime * varifySpeed)
        {
            sp.color = new Color(1f, 1f, 1f, curAlpha);
            yield return 0;
        }
        ifEmergeAll = false;
    }
}
