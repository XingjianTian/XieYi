using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchCode : MonoBehaviour {
	public float k;
	public PlayerControl pc;
	public waterPCtest wpc;
	public bool jumpOut;
	public void Start()
	{
		k = 0;
		pc = GetComponent<PlayerControl> ();
		wpc = GetComponent<waterPCtest> ();
		jumpOut = false;
	}

	#if UNITY_ANDROID
	public void Button_Left_Down()
	{
		if ((wpc != null && wpc.onBoat) || pc.jaw.ifJumpAgainstFinished) {
			//h = h + (-1 - h) * 0.5f;
			k = k + (-1 - k)*0.5f;
		}
	}
	public void Button_Left_Press()
	{
		if ((wpc != null && wpc.onBoat) || pc.jaw.ifJumpAgainstFinished) {
			//h = h + (-1 - h) * 0.5f;
			k = k + (-1 - k)*0.5f;
		}
	}
	public void Button_Right_Down()
	{
		if ((wpc != null && wpc.onBoat) || pc.jaw.ifJumpAgainstFinished) {
			//h = h + (-1 - h) * 0.5f;
			k = k + (1 - k)*0.5f;
		}
	}
	public void Button_Right_Press()
	{
		if ((wpc != null && wpc.onBoat) || pc.jaw.ifJumpAgainstFinished) {
			//h = h + (-1 - h) * 0.5f;
			k = k + (1 - k)*0.5f;
		}
	}
	public void Button_Dir_Up()
	{
		k *= 0.5f;
		jumpOut = false;
		//h = 0;
	}
	public void Button_Jump_Down()
	{
		if (wpc != null && (wpc.onBoat || wpc.inWater))
			jumpOut = true;
		pc.ifJumpOnQiuQian = true;

		if (pc.jaw.IfOnTheWall)
			pc.ifJumpAgainstWall = true;
		if (pc.grounded) {
			pc.jump = true;
			pc.ps = PlayerState.Jump;

			if (pc.ifShadowHeroExits) {
				pc.ShadowHeroF.ifJump = true;
			}
		} else if (pc.ps == PlayerState.Jump && !pc.grounded && !pc.jaw.IfOnTheWall && GetComponent<ResPutUp> ().Reses.Ability1.num == 1) {
			pc.jump = true;
			pc.ps = PlayerState.stay;
		}
	}
	public void Button_Jump_Up()
	{
		pc.ifJumpOnQiuQian = false;
		jumpOut = false;
	}
	#endif
		
	// Update is called once per frame
	void Update () {
		
	}
	void FixedUpdate()
	{
		if (Input.touchCount == 0) {
			k *= 0.5f;
			//jumpOut = false;
			//h = 0;
		}
	}
}
