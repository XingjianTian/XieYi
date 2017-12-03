using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveWithPlayer : MonoBehaviour {
    public int SceneID;
    private  bool ifReturnOriginalSize;
    public PlayerControl pc;//摄像机不随主角移动时，主角禁止操作

    public bool ifMoveToSolidPoint = false;
    public Vector3 SolidtargetPosition;//移动到定点

    //跟随
    public bool ifMoveWithOther = false;
    public GameObject FollowOther;//跟随其他精灵

    public bool ifMoveWithPlayer = true;
    public Transform character;   //跟随角色 
    public float smoothTime = 0.01f;  //摄像机平滑移动的时间
    private Vector3 cameraVelocity = Vector3.zero;
    //主摄像机（有时候会在工程中有多个摄像机，但是只能有一个主摄像机吧）     
    void Start(){
        pc = GameObject.Find("NewHero").GetComponent<PlayerControl>();
    }

    void Update(){
        //跟随主角,限制
        #region
        if (ifMoveWithPlayer)
        {
            Vector3 TargetPosition = Vector3.zero;
            if (SceneID == 1)
            {
                if (character.position.x < 0)
                    TargetPosition.x = character.position.x >= -8.2f ? character.position.x : -8.2f;
                if (character.position.x > 0)
                {
                    if(character.position.y<0)
                        TargetPosition.x = character.position.x <= 26.06f ? character.position.x : 26.06f;
                    else
                        TargetPosition.x = character.position.x <= 28.91f ? character.position.x : 28.91f;
                }
                TargetPosition.y = character.position.y;
            }
            if (SceneID == 2)
            {
                if (character.position.x < 0)
                    TargetPosition.x = character.position.x >= -6.48f ? character.position.x : -6.48f;

                if (character.position.x > 0)
                    TargetPosition.x = character.position.x <= 9.2f ? character.position.x : 9.2f;
                TargetPosition.y = character.position.y;
            }
            if (SceneID == 3)
            {
                if (character.position.x < 0)
                    TargetPosition.x = character.position.x >= -6.49f ? character.position.x : -6.49f;

                if (character.position.x > 0)
                {
                    if (character.position.y > 3.94f)
                    {
                        TargetPosition.x = character.position.x <= 3.6f ? character.position.x : 3.6f;
                    }
                    else
                    {
                        TargetPosition.x = character.position.x <= 7.5f ? character.position.x : 7.5f;
                    }
                }
                TargetPosition.y = character.position.y;
            }
            /*
            transform.position = Vector3.SmoothDamp
                (transform.position,
                TargetPosition + new Vector3(0, 0, -3),
                ref cameraVelocity,
                smoothTime);*/
            transform.position = TargetPosition + new Vector3(0, 0, -3);
        }
        #endregion
        else if (ifMoveToSolidPoint)
        {
            transform.position = Vector3.SmoothDamp(transform.position, SolidtargetPosition,
            ref cameraVelocity, smoothTime * 70);
            if (ifReturnOriginalSize)
            {
                StartCoroutine(WaitAndReturnOriginalSize(6f));
            }
            /*
            else if (!ifReturnOriginalSize)
            {
                StartCoroutine(JustWaitAndReturn(7f));
            }*/
        }
        else if (ifMoveWithOther)
        {
            transform.position = Vector3.SmoothDamp
                (transform.position,
                FollowOther.transform.position + new Vector3(0, 0, -3),
                ref cameraVelocity,
                smoothTime * 80);
			// 可扩展的追随时间
			float timeOfRe = 10f;
			if (FollowOther.name == "NPC7")
				timeOfRe = 4f;
			else if (FollowOther.name == "NPC_QuYuan")
				timeOfRe = 6f;
			StartCoroutine(WaitAndReturnOriginalSize(timeOfRe));
        }

    }
    public void changeFollowObject(GameObject ob)
    {
        pc.allowable = false;
        pc.anim.Play("Idle", -1, 0f);
        pc.rig.velocity = new Vector2(0, 0);
        ifMoveWithPlayer = false;
        ifMoveWithOther = true;
        FollowOther = ob;

    }
    public void changeToSolidPoint(Vector3 point,bool ifReturn)
    {
        if (ifReturn)
        {
            pc.allowable = false;
            pc.anim.Play("Idle", -1, 0f);
            pc.rig.velocity = new Vector2(0, 0);
        }
        ifMoveWithPlayer = false;
        ifMoveToSolidPoint = true;
        SolidtargetPosition = point;
        SolidtargetPosition.z = -3f;
        ifReturnOriginalSize = ifReturn;
    }
    IEnumerator JustWaitAndReturn(float s)
    {
        yield return new WaitForSeconds(s);
        ifMoveWithPlayer = true;
        ifMoveToSolidPoint = false;
        ifMoveWithOther = false;
        pc.allowable = true;
    }
   public IEnumerator WaitAndReturnOriginalSize(float s)
    {
        yield return new WaitForSeconds(s); 
        ifMoveWithPlayer = true;
        ifMoveToSolidPoint = false;
        ifMoveWithOther = false;
        pc.allowable = true;
        GetComponent<Camera>().orthographicSize = 1.5f;
        pc.anim.speed = 1;
    }
    public void stopAllCoroutine()
    {
        StopAllCoroutines();
        ifMoveWithPlayer = true;
        ifMoveToSolidPoint = false;
        ifMoveWithOther = false;
        pc.allowable = true;
        
    }
}
