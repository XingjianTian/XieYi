using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRadius : MonoBehaviour {

	//private const float mag = 0.5f;
    public float OriginalRadius;
    public float MaxRadius = 0.8f;
    private DynamicLight dl;
    public bool bianda = true;
    void Start()
    {
        dl = GetComponent<DynamicLight>();
        OriginalRadius = dl.LightRadius;
        //StartCoroutine(updateLoop());

    }
    void Update()
    {
        if(dl.LightRadius < MaxRadius && bianda)
        {
            dl.LightRadius += Time.deltaTime/3;
        }
        else if((dl.LightRadius > MaxRadius|| !bianda)&&(dl.LightRadius>OriginalRadius))
        {
            bianda = false;
            dl.LightRadius -= Time.deltaTime/3;
        }
        else if(dl.LightRadius<OriginalRadius)
        {
            bianda = true;
        }
    }
    /*
    IEnumerator updateLoop()
    {

        while (true)
        {
            yield return new WaitForSeconds(0.05f);
            float rnd = Random.Range(-1f, 1f) * mag;
            yield return null;
            dl.LightRadius = lastOffset + rnd;
            yield return new WaitForEndOfFrame();
        }
    }*/
}
