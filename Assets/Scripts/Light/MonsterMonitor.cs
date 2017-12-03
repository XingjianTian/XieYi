using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMonitor : MonoBehaviour {

    public float MinRotation = 90;
    public float MaxRotation = 270;
    public bool TurnRight = true;
    public float rotatePerFrame = 0.3f;
    public float NowRotation;
    void Start () {
    }
   
    void Update () {
        NowRotation = transform.localEulerAngles.z;
        if (NowRotation < MaxRotation && TurnRight)
        {
            //rotation += Time.deltaTime;
            transform.Rotate(0, 0, rotatePerFrame);
        }
        else if ((NowRotation > MaxRotation || !TurnRight)
            && (NowRotation > MinRotation))
        {
            TurnRight = false;
            //rotation -= Time.deltaTime;
            transform.Rotate(0, 0, -rotatePerFrame);
        }
        else if (NowRotation < MinRotation)
        {
            TurnRight = true;
            transform.Rotate(0, 0, rotatePerFrame);
        }
    }
}
