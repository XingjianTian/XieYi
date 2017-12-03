using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour {

    
    public float startTime;
    public float circleTime;
    public float MinRadius=0f;
    public float MaxRadius=1.2f;
    public float CurRadius;
    private DynamicLight dl;
    public float timer;
    public bool ifflash = true;
    public float zoomSpeed;
    void Start()
    {
        dl = GetComponent<DynamicLight>();
        //StartCoroutine(updateLoop());
        CurRadius = dl.LightRadius;
        timer = startTime;
    }
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = circleTime;
            if(ifflash)
                StartCoroutine(ZoomOut());
        }
    }
    public IEnumerator ZoomIn()
    {
        while (dl.LightRadius > MinRadius)
        {
            dl.LightRadius -= Time.deltaTime*zoomSpeed;
            yield return 0;
        }
        ifflash = true;
        yield return 0;
    }
    public IEnumerator ZoomOut()
    {
        ifflash = false;
        while (dl.LightRadius < MaxRadius)
        {
            dl.LightRadius += Time.deltaTime*zoomSpeed;
            yield return 0;
        }
        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(ZoomIn());
    }
    
}
