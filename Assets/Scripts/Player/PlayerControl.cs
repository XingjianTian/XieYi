using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum PlayerState
{
    stay,
    Jump,
    DoubleJump
};
public class PlayerControl : MonoBehaviour
{
    public bool ifshieldOn = false;
    public bool ifJumpOnQiuQian = false;
    public ShadowFollow ShadowHeroF;
    public bool ifShadowHeroExits = false;
	public bool ifJumpAgainstWall = false;
    public bool ifUpSideDown;
    public float h;
	public bool facingRight = true;			// For determining which way the player is currently facing.
	public bool jump = false;				// Condition for whether the player should jump.
    public PlayerState ps;

	public float moveForce = 80f;			// Amount of force added to move the player left and right.
	public float maxSpeed = 5f;				// The fastest the player can travel in the x axis.
	public AudioClip[] jumpClips;			// Array of clips for when the player jumps.
	public float jumpForce = 150f;			// Amount of force added when the player jumps.
	public AudioClip[] taunts;				// Array of clips for when the player taunts.
	public AudioClip[] Events;
	public float tauntProbability = 50f;	// Chance of a taunt happening.
	public float tauntDelay = 1f;			// Delay for when the taunt should happen.


	private int tauntIndex;					// The index of the taunts array indicating the most recent taunt.
	public Transform groundCheck;			// A position marking where to check if the player is grounded.
	public Transform groundCheck1;			// A position marking where to check if the player is grounded.
	public bool grounded = false;			// Whether or not the player is grounded.
	public Animator anim;					// Reference to the player's animator component.

    public bool allowable = true;
    public GameObject KeySpace;
    public Rect KeySpaceRect;
    public GameObject KeyLeft;
    public Rect KeyLeftRect;
    public GameObject KeyRight;
    public Rect KeyRightRect;
    public GameObject KeyE;
    public JumpAgainstWall jaw;
    public Volume volume;
    public Rigidbody2D rig;
    //public bool ifButtonPress = false;
  
	public spring TreeSpring;
	public TouchCode tc;
    void Awake()
	{
		// k = 0;
#if UNITY_ANDROID
        KeySpace.GetComponent<Text>().text = "跳跃";
        KeyLeft.GetComponent<Text>().text = "←";
        KeyRight.GetComponent<Text>().text = "→";
        KeyE.GetComponent<Text>().text = "■";
#endif
        ps = PlayerState.stay;
		// Setting up references.
		
	}
    void Start()
    {
        KeyLeftRect = new Rect(KeyLeft.GetComponent<RectTransform>().position, KeyLeft.GetComponent<RectTransform>().sizeDelta * 4.5f);
        KeyRightRect = new Rect(KeyRight.GetComponent<RectTransform>().position, KeyRight.GetComponent<RectTransform>().sizeDelta * 4.5f);
        
        rig = GetComponent<Rigidbody2D>();
        jaw = GetComponent<JumpAgainstWall>();
		anim = GetComponent<Animator>();
		tc = GetComponent<TouchCode> ();
        if(GameObject.Find("Tree")!=null)
		TreeSpring = GameObject.Find ("Tree").GetComponent<spring> ();

        ifShadowHeroExits= !(GameObject.Find("ShadowHero") == null);

        if (ifShadowHeroExits)
            ShadowHeroF = GameObject.Find("ShadowHero").GetComponent<ShadowFollow>();

    }
    
    void Update()
    {
        if (!allowable)
        {
            return;
        }
        // The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")) |
			Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("YellowGround"));
		grounded = (grounded || (bool)(Physics2D.Linecast(transform.position, groundCheck1.position, 1 << LayerMask.NameToLayer("Ground")) |
			Physics2D.Linecast(transform.position, groundCheck1.position, 1 << LayerMask.NameToLayer("YellowGround"))));


        // If the jump button is pressed and the player is grounded then the player should jump.

#if UNITY_STANDALONE_WIN ||UNITY_EDITOR
        if (Input.GetButtonDown("Jump"))
        {
            if (grounded)
            {
                jump = true;
                ps = PlayerState.Jump;
                if (ifShadowHeroExits)
                {
                    ShadowHeroF.ifJump = true;
                }
            }
            else if (ps == PlayerState.Jump&&!grounded&&!jaw.IfOnTheWall&&GetComponent<ResPutUp>().Reses.Ability1.num==1)
            {
                jump = true;
                ps = PlayerState.stay;
            }
        
        }
#endif
    }

    void FixedUpdate ()
	{
        if (!allowable)
            return;
        
#if UNITY_STANDALONE_WIN
        if (jaw.ifJumpAgainstFinished)
            h = Input.GetAxis("Horizontal");
#endif

        #region 
#if UNITY_ANDROID

        h = tc.k;
#endif
        #endregion
		

        if (ifUpSideDown)
        {
            h = -h;
            Physics2D.gravity = new Vector3(0, 9.81F, 0);
        }

        anim.SetFloat("Speed", Mathf.Abs(h));
        // If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
        if (h * rig.velocity.x < maxSpeed&&jaw.ifJumpAgainstFinished)
        {
            rig.AddForce(Vector2.right * h * moveForce);
        }
        // If the player's horizontal velocity is greater than the maxSpeed...
        // if (Mathf.Abs(rig.velocity.x) > maxSpeed)

        if (TreeSpring != null)
        {
            if (Mathf.Abs(rig.velocity.x) > maxSpeed && (TreeSpring.opTime == 4 || TreeSpring.opTime == 5 ? grounded : true))
            // ... set the player's velocity to the maxSpeed in the x axis.
                rig.velocity = new Vector2(Mathf.Sign(rig.velocity.x) * maxSpeed, rig.velocity.y);
        }
        else if (Mathf.Abs(rig.velocity.x) > maxSpeed)
                rig.velocity = new Vector2(Mathf.Sign(rig.velocity.x) * maxSpeed, rig.velocity.y);

        // If the input is moving the player right and the player is facing left...
        if (!ifUpSideDown)
        {
           
                if (h > 0 && !facingRight && !jaw.IfOnTheWall&&jaw.ifJumpAgainstFinished)
                    // ... flip the player.
                    Flip();
                // Otherwise if the input is moving the player left and the player is facing right...
                else if (h < 0 && facingRight && !jaw.IfOnTheWall && jaw.ifJumpAgainstFinished)
                    // ... flip the player.
                    Flip();
            
        }
        else
        {
            if (h > 0 && facingRight && !jaw.IfOnTheWall && jaw.ifJumpAgainstFinished)
                // ... flip the player.
                Flip();
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (h < 0 && !facingRight && !jaw.IfOnTheWall && jaw.ifJumpAgainstFinished)
                // ... flip the player.
                Flip();
        }

		// If the player should jump...
		if(jump)
		{
			// Set the Jump animator trigger parameter.
			anim.SetTrigger("Jump");

			// Play a random jump audio clip.
			int i = Random.Range(0, jumpClips.Length);
            if(volume.ifvolumeon)
			    AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);

            // Add a vertical force to the player.
            if (jaw.IfOnTheWall == false)
                if(Physics2D.gravity.y<0)
                    rig.AddForce(new Vector2(0f, jumpForce));
                else
                    rig.AddForce(new Vector2(0f, -jumpForce));

            // Make sure the player can't jump again until the jump conditions from Update are satisfied.
            jump = false;
		}
	}
	
	
	public void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}


	public IEnumerator Taunt()
	{
		// Check the random chance of taunting.
		float tauntChance = Random.Range(0f, 100f);
		if(tauntChance > tauntProbability)
		{
			// Wait for tauntDelay number of seconds.
			yield return new WaitForSeconds(tauntDelay);

			// If there is no clip currently playing.
			if(!GetComponent<AudioSource>().isPlaying)
			{
                // Choose a random, but different taunt.
                if (volume.ifvolumeon)
                {
                    tauntIndex = TauntRandom();

                    // Play the new taunt.
                    GetComponent<AudioSource>().clip = taunts[tauntIndex];
                    GetComponent<AudioSource>().Play();
                }
			}
		}
	}


	int TauntRandom()
	{
		// Choose a random index of the taunts array.
		int i = Random.Range(0, taunts.Length);

		// If it's the same as the previous taunt...
		if(i == tauntIndex)
			// ... try another random taunt.
			return TauntRandom();
		else
			// Otherwise return this index.
			return i;
	}
}
