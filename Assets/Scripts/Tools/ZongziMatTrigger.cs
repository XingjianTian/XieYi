using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZongziMatTrigger : MonoBehaviour {
    bool ifTriggered = false;
    MakeZongzi manager;
    public int MatId;
	// Use this for initialization
	void Start () {
        manager = GameObject.Find("MakeZongziGame").GetComponent<MakeZongzi>();
        switch (name)
        {
            case "ZongYePrefab(Clone)": MatId =0; break;
            case "RouXianPrefab(Clone)": MatId = 1;break;
            case "RicePrefab(Clone)": MatId = 2;break ;
            default:break;
        }

	}
	
	// Update is called once per frame
	void Update () {
        
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "NewHero"&&!ifTriggered)
        {
            manager.Xulie += MatId.ToString();
            ifTriggered = true;
            Destroy(gameObject);
        }
    }
}
