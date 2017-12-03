using UnityEngine;
using System.Collections;

public class Clickspawn : MonoBehaviour {

    public GameObject Brick;
	private Water water;
	bool begin = false;
	void start() {
		//water = GetComponent<Water> ();
	}
	void Update () {
		if (!begin) {
			begin = true;
			StartCoroutine (Glue());
		}
	}
	IEnumerator Glue()
	{
		//Debug.Log("begin");
		while (true) {
			if (water == null) {
				water = GetComponent<Water> ();
				//print ("null");
				continue;
			}
			int i = UnityEngine.Random.Range (0, water.xpositions.Length - 1);
			water.Splash (water.xpositions [i], -0.1f);
			yield return new WaitForSeconds(0.5f);
		}
		//yield return 0;
	}
}
