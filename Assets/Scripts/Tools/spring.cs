using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class spring : MonoBehaviour {

    //操控跳跃
    public bool ifjump = false;


    SpringJoint2D myJoint;
    GameObject hero, wood;
	Rigidbody2D hero_Rigidbody2D, wood_Rigidbody2D;
	PlayerControl hero_PlayerControl;
	BoxCollider2D hero_BoxCollider2D;
    LineRenderer line;
	public bool isHanging;
	public int opTime;
	public float lenOfLine;
    private float frequency;
	public Vector2 hangPoint;
	private Vector3 AVelocity = Vector3.zero;
	private float springLenth;
	public Vector2 wagForceL = new Vector2 (1f, 0);
	public Vector2 wagForceR = new Vector2 (-1f, 0);

	public TouchCode tc;
    bool lastE;

    // Use this for initialization
    
    void Start () {
        lastE = false;
		opTime = 0;
		hero = GameObject.Find("NewHero");
		wood = GameObject.Find("wood");
		hero_Rigidbody2D = hero.GetComponent<Rigidbody2D> ();
        wood_Rigidbody2D = wood.GetComponent<Rigidbody2D> ();
		hero_PlayerControl = hero.GetComponent<PlayerControl> ();
		hero_BoxCollider2D = hero.GetComponent<BoxCollider2D> ();
		tc = hero.GetComponent<TouchCode> ();
		myJoint = wood.AddComponent<SpringJoint2D>();
        myJoint.enabled = false;
        isHanging = false;
		line = wood.GetComponent<LineRenderer>();
        frequency = 100;

		springLenth = lenOfLine;
		hangPoint = new Vector2(transform.position.x, transform.position.y + 1.1f);
	}

	// Update is called once per frame
	void Update () {
		if (wood == null) {
			return;
		}
        if (!isHanging && opTime == 1)
        {
            opTime += 1;
			isHanging = true;
            if (myJoint == null)
            {
				myJoint = wood.AddComponent<SpringJoint2D>();
            }
			StartCoroutine (CreateSpring (hangPoint));
        }
		else if (isHanging && opTime == 3)
        {
			opTime += 1;
			hero_PlayerControl.grounded = false;
			hero_PlayerControl.enabled = false;
			hero_Rigidbody2D.mass = 0.1f;
			// hero_BoxCollider2D.isTrigger = true;
        }
        if (isHanging && opTime == 5) {
			if (hero_PlayerControl.grounded) {
				opTime = 2;
			}
		}
        if (isHanging)
        {
			line.SetPosition(0, wood.transform.position);
			line.SetPosition(1, hangPoint);
        }

	}
	void FixedUpdate () {
		if (isHanging && opTime == 4)
		{
			//hero.transform.position = Vector3.SmoothDamp(hero.transform.position, new Vector2(wood.transform.position.x, wood.transform.position.y - 0.19f), ref AVelocity, 0.03f);
			hero.transform.position = new Vector2(wood.transform.position.x, wood.transform.position.y - 0.19f);
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
			if (Input.GetKey ("space"))
			{
				hero_Rigidbody2D.mass = 1f;
				hero_PlayerControl.enabled = true;
				// hero_BoxCollider2D.isTrigger = false;
				hero_Rigidbody2D.velocity = new Vector2 (hero_Rigidbody2D.velocity.x * 2.5f, hero_Rigidbody2D.velocity.y * 4.0f);
				opTime += 1;
				//GroundedBack ();
			}
			else
			{
				if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
				{
					wood_Rigidbody2D.AddForce (wagForceL);
				}
				if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
				{
					wood_Rigidbody2D.AddForce (wagForceR);
				}
			}
#endif
#if UNITY_ANDROID
			if (hero_PlayerControl.ifJumpOnQiuQian)
			{
				hero_Rigidbody2D.mass = 1f;
				hero_PlayerControl.enabled = true;
				// hero_BoxCollider2D.isTrigger = false;
				hero_Rigidbody2D.velocity = new Vector2(hero_Rigidbody2D.velocity.x * 2.5f, hero_Rigidbody2D.velocity.y * 4.0f);
				opTime += 1;
				//ifjump = false;
				//GroundedBack ();
			}
			else
			{
				if (tc.k < 0)
				{
					wood_Rigidbody2D.AddForce(wagForceL);
				}
				if (tc.k > 0)
				{
					wood_Rigidbody2D.AddForce(wagForceR);
				}
			}
#endif
		}
	}
	private IEnumerator CreateSpring(Vector2 aim)
    {
		myJoint.connectedBody = GetComponent<Rigidbody2D>();
		myJoint.connectedAnchor = aim;
        myJoint.frequency = frequency;
        myJoint.enableCollision = true;
        myJoint.enabled = true;
		myJoint.distance = 0;
        isHanging = true;

        line.enabled = true;

		wood.GetComponent<SpriteRenderer> ().enabled = true;
		wood.GetComponent<BoxCollider2D> ().enabled = true;
		wood.GetComponent<Rigidbody2D> ().simulated = true;
		for (float i = 0; i < lenOfLine; i += 0.01f) {
			myJoint.distance = i;
			yield return 0;
		}
		GameObject.Find ("SeedShouldHereEventPoint2").GetComponent<BoxCollider2D> ().enabled = true;
		yield return 0;
    }
    private void springBreak()
    { 
        myJoint.enabled = false;
		isHanging = false;
        line.enabled = false;
    }
	/*IEnumerator GroundedBack()
	{
		yield return 0;
	}*/
}