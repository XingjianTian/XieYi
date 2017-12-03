using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterPCtest : MonoBehaviour {

	public bool onBoat = false;
	public bool inWater = false;
	public GameObject hero;
	public PlayerControl pc;
	public GameObject boat;
	public Rigidbody2D boat_rig, rig;
	public Animator anim;

	const float moveForce = 7f;
	const float maxSpeed = 1.1f;

	public bool allowable = false;
	public TouchCode tc;
	private float h;
	// Use this for initialization
	void Start () {
		//gotoBoat ();
		tc = GetComponent<TouchCode>();
	}
	
	// Update is called once per frame
	void Update(){
#if UNITY_STANDALONE_WIN
		if (allowable && onBoat && Input.GetKey("space")) {
			getoutBoat ();
		}
#endif
#if UNITY_ANDROID
		if (allowable && onBoat && tc.jumpOut) {
			getoutBoat ();
		}
#endif
	}
	void FixedUpdate () {
		if (onBoat) {
			/*if (pc.allowable == true) {
				pc.allowable = false;
			}*/
			hero.transform.position = new Vector2 (boat.transform.position.x, boat.transform.position.y);
			// boat control
#if UNITY_STANDALONE_WIN ||UNITY_EDITOR
			h = Input.GetAxis ("Horizontal");
#endif
#if UNITY_ANDROID
			h = tc.k;
#endif
			if (h * boat_rig.velocity.x < maxSpeed) {
				boat_rig.AddForce (Vector2.right * h * moveForce);
				boat_rig.AddForce (new Vector2 (-boat_rig.velocity.x, 0));
			}
			if (Mathf.Abs (boat_rig.velocity.x) > maxSpeed)
				boat_rig.velocity = new Vector2 (Mathf.Sign (boat_rig.velocity.x) * maxSpeed, boat_rig.velocity.y);
		} 
		else if (allowable && inWater){
			if (rig.velocity.y < -maxSpeed / 0.8f) {
				rig.velocity = new Vector2 (rig.velocity.x, -maxSpeed / 0.8f);
			} else if (rig.velocity.y > maxSpeed / 0.8f) {
				rig.velocity = new Vector2 (rig.velocity.x, maxSpeed / 0.8f); 
			} 
#if UNITY_STANDALONE_WIN
			if (Input.GetKey ("space")) {
				rig.AddForce (new Vector2 (0, 11.8f * rig.mass));
			} else {
				rig.AddForce (new Vector2 (0, 7.8f * rig.mass));
			}
#endif
#if UNITY_ANDROID
			if (tc.jumpOut) {
				rig.AddForce (new Vector2 (0, 11.8f * rig.mass));
			} else {
				rig.AddForce (new Vector2 (0, 7.8f * rig.mass));
			}
#endif
			rig.AddForce (new Vector2 (-rig.velocity.x * 20, 0));
		}
	}

	public void gotoBoat()
	{
		onBoat = true;
		//hero.GetComponent <PlayerControl> ().anim.Play("Idle", -1, 0f);
		hero.GetComponent <Rigidbody2D> ().mass = 0.01f;
		//hero.GetComponent <PlayerControl> ().allowable = false;
		hero.GetComponent <PlayerControl> ().enabled = false;
		boat_rig.constraints = RigidbodyConstraints2D.None;
		boat.GetComponent<BoxCollider2D> ().isTrigger = false;
	}
	public void getoutBoat()
	{
		onBoat = false;
		//hero.GetComponent <BoxCollider2D> ().enabled = true;
		//hero.GetComponent <Rigidbody2D> ().simulated = true;
		hero.GetComponent <Rigidbody2D> ().mass = 1f;
		hero.GetComponent <PlayerControl> ().allowable = true;
		hero.GetComponent <PlayerControl> ().enabled = true;
		boat_rig.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
		//boat.GetComponent<BoxCollider2D> ().isTrigger = true;
	}
}
