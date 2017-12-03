using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {
	GameObject hero;
	ResPutUp hero_rpu;

	// Use this for initialization
	void Start () {
		hero = GameObject.Find ("NewHero");
		hero_rpu = hero.GetComponent<ResPutUp> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//transform.rotation = Quaternion.Euler(0, 0, 0.1f*transform.rotation.z);
		GetComponent<Rigidbody2D>().AddTorque(-2f * transform.rotation.z - 0.004f*GetComponent<Rigidbody2D>().angularVelocity, ForceMode2D.Force);
	}

	void OnTriggerStay2D(Collider2D Hit) {
#if UNITY_STANDALONE_WIN
		if (Input.GetKey("e") && Hit.name == "NewHero") {
			GameObject.Find ("NewHero").GetComponent<waterPCtest> ().gotoBoat ();
		}
#endif
#if UNITY_ANDROID
		if (hero_rpu.if_E_Pressed && Hit.name == "NewHero") {
			GameObject.Find ("NewHero").GetComponent<waterPCtest> ().gotoBoat ();
			hero_rpu.lastE = true;
		}
#endif
	}
}
