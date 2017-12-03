using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showTest : MonoBehaviour {

    public float curAlpha = 1f;
    public bool iffade = true;
    public float maxAlpha = 1f;
    public float minAlpha = 0f;
    public float varifySpeed = 0.4f;
    public GameObject tkm;
    public SpriteRenderer tkms;
    public GameObject tm;
    public Text tmt;
    private Color duihuakuangColor;
    private Color wenziColor;
    // Use this for initialization
    void Start () {
        tkms = tkm.GetComponent<SpriteRenderer>();
        tmt = tm.GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
		if(iffade)
        {
            ScenarioFade();
        }
	}
    void ScenarioFade()
    {
        if (curAlpha > minAlpha)
        {
            curAlpha -= Time.deltaTime * varifySpeed;
            //if (curAlpha < maxAlpha)
            //  varifySpeed *= -1;

            //对话框渐透明
            curAlpha = Mathf.Clamp(curAlpha, minAlpha, maxAlpha);
            duihuakuangColor = tkms.material.color;
            duihuakuangColor.a = curAlpha;
            tkms.material.color = duihuakuangColor;

            //文字渐透明
            wenziColor = tmt.color;
            wenziColor.a = curAlpha;
            tmt.color = wenziColor;
        }
        else if (curAlpha <= minAlpha)
        {
            iffade = false;
            Destroy(this);
        }
    }
}
