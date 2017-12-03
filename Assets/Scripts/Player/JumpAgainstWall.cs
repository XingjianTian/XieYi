using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAgainstWall : MonoBehaviour {
    public bool IfOnTheWall = false;
    public float WallJumpForce = 10f;
    public bool JumpAgainst = false;
    public bool ifJumpAgainstFinished = true;
    public Animator anim;
    public PlayerControl pc;
    public DeathControl dc;
    Rigidbody2D rig;
    // Use this for initialization
    void Start () {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        pc = GetComponent<PlayerControl>();
        dc = GetComponent<DeathControl>();
	}

	// Update is called once per frame
	void Update ()
    {
        if (!pc.allowable) return;

        if (IfOnTheWall && (pc.ifJumpAgainstWall||Input.GetButtonDown("Jump")))
        {
            JumpAgainst = true;
            ifJumpAgainstFinished = false;
        }
        #region
        if (JumpAgainst&&!ifJumpAgainstFinished)
        {
            rig.velocity = Vector2.zero;
            bool iffacingright = pc.facingRight;
            if (!pc.ifUpSideDown)//人正着
            {
                if (!iffacingright)//面向左
                {
                    //GetComponent<PlayerControl>().Flip();
                    anim.SetBool("WallRide", false);
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 0.9f) * WallJumpForce);
                    anim.SetTrigger("Jump");
                    pc.ifJumpAgainstWall = false;
                }
                else//面向右
                {
                    //GetComponent<PlayerControl>().Flip();
                    anim.SetBool("WallRide", false);
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 0.9f) * WallJumpForce);
                    anim.SetTrigger("Jump");
                    pc.ifJumpAgainstWall = false;
                }
            }
            else//人反着
            {
                if (!iffacingright)
                {
                    //GetComponent<PlayerControl>().Flip();
                    anim.SetBool("WallRide", false);
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(1, -0.9f) * WallJumpForce);
                    anim.SetTrigger("Jump");
                    pc.ifJumpAgainstWall = false;
                }
                else
                {
                    //GetComponent<PlayerControl>().Flip();
                    anim.SetBool("WallRide", false);
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, -0.9f) * WallJumpForce);
                    anim.SetTrigger("Jump");
                    pc.ifJumpAgainstWall = false;
                }
            }
            JumpAgainst = false;
        }
        #endregion
        if(pc.grounded)
        {
            ifJumpAgainstFinished = true;
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Walls") && !dc.isgrounded&&!JumpAgainst)
        {
            anim.SetBool("WallRide",true);
            if (!IfOnTheWall)
                pc.Flip();
            IfOnTheWall = true;
        }
    }
    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Walls") && !dc.isgrounded&&!JumpAgainst)
        {
            anim.SetBool("WallRide",true);
            if(!IfOnTheWall)
                pc.Flip();
            IfOnTheWall = true;
            GetComponent<Rigidbody2D>().gravityScale = 0.5f;
        }
    }
    void OnCollisionExit2D(Collision2D col)
    {
        anim.SetBool("WallRide", false);
        //anim.Play("Idle");
        GetComponent<Rigidbody2D>().gravityScale = 1f;
        IfOnTheWall = false;
    }
}
