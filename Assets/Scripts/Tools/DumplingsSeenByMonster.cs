using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumplingsSeenByMonster : MonoBehaviour {
    public GameObject came;
    public bool ifrotate = false;
    public bool onceSound = true;
    public Vector3 targetPosition;
    public GameObject Monster;
    public GameObject MonsterEye;
    public DynamicLight dl;
    public AudioClip[] AlertClip;
    public bool SeeDumplings = false;
    public float NowRotation;
    public float MaxRotation = 180f;
    public float rotatePerFrame = 1f;
    void Start()
    {
        came = GameObject.Find("Main Camera");
        Monster = GameObject.Find("Monster");
        MonsterEye = GameObject.Find("Eyelight");
        dl = MonsterEye.GetComponent<DynamicLight>();
        dl.InsideFieldOfViewEvent += onEnterFieldOfView;
        targetPosition = new Vector3(11f, Monster.transform.position.y, 0f);
        // yield return new WaitForEndOfFrame();
    }
    void Update()
    {
        if (SeeDumplings)
        {
            SeeDumplings = true;
            NowRotation = MonsterEye.transform.localEulerAngles.z;
            if (NowRotation < MaxRotation&&!ifrotate)
            {
                //rotation += Time.deltaTime;
                MonsterEye.transform.Rotate(0, 0, rotatePerFrame);
                NowRotation = MonsterEye.transform.localEulerAngles.z;
                if (NowRotation >= MaxRotation)
                    ifrotate = true;
            }
            else if(NowRotation>MaxRotation&&!ifrotate)
            {
                MonsterEye.transform.Rotate(0, 0, -rotatePerFrame);
                NowRotation = MonsterEye.transform.localEulerAngles.z;
                if (NowRotation <= MaxRotation)
                    ifrotate = true;
            }
            Monster.transform.position = Vector3.MoveTowards
                (Monster.transform.position, targetPosition, 1f * Time.deltaTime);
            MonsterEye.GetComponent<MonsterMonitor>().enabled = false;
            if (Monster.transform.position == targetPosition)
            {
                Monster.isStatic = true;
                came.GetComponent<CameraMoveWithPlayer>().stopAllCoroutine();
            }
        }
    }
    void onEnterFieldOfView(GameObject[] g)
    {
        for (int i = 0; i < g.Length; i++)
        {
            if (gameObject.GetInstanceID() == g[i].GetInstanceID())
                SeeDumplings = true;
        }

        if (onceSound)
        {
            AudioSource.PlayClipAtPoint(AlertClip[0], Vector3.zero, 0.6f);
            onceSound = false;
        }
    }

}
