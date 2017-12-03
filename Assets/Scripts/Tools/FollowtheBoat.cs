using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowtheBoat : MonoBehaviour {
	public GameObject boat;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector2 (boat.transform.position.x - 0.4f, boat.transform.position.y + 0.1f);
	}
}
