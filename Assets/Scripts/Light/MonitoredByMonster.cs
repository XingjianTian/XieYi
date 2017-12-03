using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitoredByMonster : MonoBehaviour {
    public bool iffade=false;
    //Color originalColor;
    public int AlertTimes = 0;
    public int HitByArrowTimes = 0;
    public float PrevAlertTime;
    public GameObject came;
    public DynamicLight dl;
    public AudioClip []AlertClip;
    public bool SeePlayer;
    public bool HitByArrow;
    public DeathControl dc;
    void Start()
    {
        dc = GetComponent<DeathControl>();
        //originalColor = came.GetComponent<Camera>().backgroundColor;
        PrevAlertTime = 0f;
        dl = GameObject.Find("Eyelight").GetComponent<DynamicLight>();
        dl.OnExitFieldOfView += onExitFieldOfView;
        dl.InsideFieldOfViewEvent += onEnterFieldOfView;
        // yield return new WaitForEndOfFrame();
    }
    private void Update()
    {
        if (HitByArrow)
        {
            if (HitByArrowTimes < 2)
            {
                AudioSource.PlayClipAtPoint(AlertClip[0], Vector3.zero, 0.6f);
                //came.GetComponent<Camera>().backgroundColor = new Color(0.6f, 0.3f, 0.3f);
                iffade = true;
                Invoke("ChangeColor", 0.15f);
                HitByArrowTimes++;
                HitByArrow = false;
            }
            else if (HitByArrowTimes == 2 || HitByArrowTimes > 2)
            {
                dc.ifdead = true;
            }
        }
    }
    void onExitFieldOfView(GameObject g)
    {
        SeePlayer = false;
        //AlertTimes = 0;
        PrevAlertTime = 0;
    }
    void onEnterFieldOfView(GameObject[] g)
    {
        SeePlayer = false;
        for (int i = 0;i<g.Length;i++)
        {
            if (gameObject.GetInstanceID() == g[i].GetInstanceID())
                SeePlayer = true;
        }
        if (SeePlayer)
        {
            if (AlertTimes < 2)
            {
                if (PrevAlertTime + 1 < Time.time)
                {
                    PrevAlertTime = Time.time;
                    AudioSource.PlayClipAtPoint(AlertClip[0], Vector3.zero, 0.6f);
                    //came.GetComponent<Camera>().backgroundColor = new Color(0.6f, 0.3f, 0.3f);
                    iffade = true;
                    Invoke("ChangeColor", 0.15f);
                    AlertTimes++;
                }
            }
            else if (AlertTimes == 2 || AlertTimes > 2)
            {
                dc.ifdead = true;
            }
        }
        

    }
    void ChangeColor()
    {
        iffade = false;
    }

}
