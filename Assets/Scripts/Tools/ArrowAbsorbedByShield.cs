using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAbsorbedByShield : MonoBehaviour {
    public bool OnceHitPlayer = true;
    public bool ifShieldOn = false;
    public bool ifexisted = true;
    public bool onceSound = true;
    public GameObject Shield;
    public DynamicLight dl;
    public AudioClip[] AlertClip;
    public bool AbsorbedArrow = false;
    // Use this for initialization
    void Start () {
        Shield = GameObject.Find("Shield");
        if (Shield.GetComponent<DynamicLight>().enabled == true)
            
            ifShieldOn = true;
        if (ifShieldOn)
        {
            dl = Shield.GetComponent<DynamicLight>();
            dl.InsideFieldOfViewEvent += onEnterFieldOfView;
        }
    }
	
	// Update is called once per frame
	void Update () {
		if(ifexisted&& AbsorbedArrow&&ifShieldOn)
        {
            Destroy(gameObject);
            ifexisted = false;
            dl.Rebuild();
        }
        else if(ifexisted&&transform.position.y<-4.5f)
        {
            Destroy(gameObject);
        }
	}
   
    
    void onEnterFieldOfView(GameObject[] g)
    {
        foreach (GameObject gs in g)
        {

            if (ifexisted && ifShieldOn)
            {
                    AbsorbedArrow = true;
            }

        }

        if (onceSound && ifShieldOn)
        {
            AudioSource.PlayClipAtPoint(AlertClip[0], Vector3.zero, 0.6f);
            onceSound = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name=="NewHero")
        {
            if (ifexisted&&!ifShieldOn&&OnceHitPlayer)
            {
                collision.gameObject.GetComponent<MonitoredByMonster>().HitByArrow = true;
                OnceHitPlayer = false;
                Destroy(gameObject);
                ifexisted = false;
            }
        }
    }

}
