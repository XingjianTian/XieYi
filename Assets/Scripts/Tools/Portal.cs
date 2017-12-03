using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {
    public bool ifmoved=false;
    public bool IfDecideDirection = false;
    public GameObject player;
    public PlayerControl pc;
    public Color originalColor;
    public bool ifopen = false;
    bool once = true;
    public GameObject nextdoor;
    public Vector3 transitionto;
    public AudioClip[] dooropenvoice;
    public GameObject came;
    public CameraBlack cb;
    public float curColor;
    // Use this for initialization
    void Start () {
        
        cb = came.GetComponent<CameraBlack>();
        pc = player.GetComponent<PlayerControl>();
        if(nextdoor!=null)
            transitionto = nextdoor.GetComponent<Transform>().position;
        originalColor = came.GetComponent<Camera>().backgroundColor;
        curColor = 0f;
    }
	
	// Update is called once per frame
	void Update () {
        if (ifopen&&nextdoor!=null)
        {
            if (transform.parent.name == "Door1In")
            {
                GameObject.Find("ShadowHero(Clone)").GetComponent<ShadowFollow>().State = 1;
            }
            if(transform.parent.name=="DoorZ"&&once)
            {
                GameObject.Find("SeeLockedDoorEventPoint").GetComponent<BoxCollider2D>().enabled = false;
                //GameObject.Find("Main Camera").GetComponent<CameraMoveWithPlayer>().ifMoveWithPlayer = true;
                StartCoroutine(GameObject.Find("Main Camera").GetComponent<CameraMoveWithPlayer>().WaitAndReturnOriginalSize(0.2f));
				//StartCoroutine (lockThePos ());
                GameObject.Find("BGM_Audio").GetComponent<AudioSource>().clip = pc.GetComponent<PlayerControl>().Events[2];
                GameObject.Find("BGM_Audio").GetComponent<AudioSource>().Play();
                //came.GetComponent<Camera>().orthographicSize = 1.5f;
                once = false;
                GameObject.Find("YellowFloor1").GetComponent<SpriteRenderer>().enabled = true;
                GameObject.Find("YellowFloor1").GetComponent<BoxCollider2D>().enabled = true;
                GameObject.Find("NPC_WineMaker").GetComponentInChildren<TextMesh>().text = "你..你是怎么从\n我家后门出来的！";
            }
            GetComponent<Animator>().enabled = true;
            nextdoor.GetComponentInChildren<Animator>().enabled = true;
            //AudioSource.PlayClipAtPoint(dooropenvoice[0], Vector3.zero, 0.6f);
            //came.GetComponent<Camera>().backgroundColor = new Color(0.2f, 0.2f, 0.2f);
            if (curColor >= -0.9f)
            {
                curColor -= Time.deltaTime/4;
                cb.ma.SetFloat("_Float1", curColor);
            }
            Invoke("ChangePosition", 2f);
            //player.GetComponent<Transform>().position = transitionto;
            //ifopen = false;
        }
    }
    void ChangePosition()
    {
        if (curColor <= 0f)
        {
            curColor += Time.deltaTime / 4;
            cb.ma.SetFloat("_Float1", curColor);
        }
        if (!ifmoved)
        {
            player.GetComponent<Transform>().position = transitionto;
            player.GetComponent<ResPutUp>().Reses.Ability1.clear();
            ifmoved = true;
        }
        GetComponent<Animator>().enabled = false;
        
        ifopen = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (pc.facingRight && !IfDecideDirection)
            {
                transitionto.x += 0.1f;
                IfDecideDirection = true;
            }
            else if (!IfDecideDirection)
            {
                transitionto.x -= 0.1f;
                IfDecideDirection = true;
            }
            ifopen = true;
        }
    }
	IEnumerator lockThePos()
	{
		
        pc.allowable = false;
        pc.anim.Play("Idle", -1, 0f);
        pc.anim.speed = 0;
        pc.rig.velocity = new Vector2(0, 0);

        yield return new WaitForSeconds (3);
        pc.allowable = true;
        pc.anim.speed = 1;
    }
}
