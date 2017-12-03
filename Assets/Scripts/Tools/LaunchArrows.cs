using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchArrows : MonoBehaviour {
    public GameObject Arrow;
    public GameObject Arch;
    private float lastTime;
    private float curTime;
    // Use this for initialization
    void Start () {
        lastTime = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        curTime = Time.time;
        if (curTime - lastTime >= 1f)
        {
            CreateArrow();
            lastTime = curTime;
        }
    }

    void CreateArrow()
    {
        GameObject arrow = Instantiate(Arrow, Arch.transform.position, Arch.transform.rotation);
    }
}
