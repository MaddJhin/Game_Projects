using UnityEngine;

public class PlatformerCharacter2D : MonoBehaviour {

	public static float distanceTraveled;				// For determining how far the player has moved. 

	bool facingRight = true;							// For determining which way the player is currently facing.
	bool normalGravity = true;							// 

	[SerializeField] float maxSpeed = 10f;				// The fastest the player can travel in the x axis.
	[SerializeField] float jumpForce = 400f;			// Amount of force added when the player jumps.	

	[Range(0, 1)]
	[SerializeField] float crouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	
	[SerializeField] bool airControl = false;			// Whether or not a player can steer whiSle jumping;
	[SerializeField] LayerMask whatIsGround;			// A mask determining what is ground to the character
	
	Transform groundCheck;								// A position marking where to check if the player is grounded.
	float groundedRadius = .2f;							// Radius of the overlap circle to determine if grounded
	bool grounded = false;								// Whether or not the player is grounded.
	Transform ceilingCheck;								// A position marking where to check for ceilings
	float ceilingRadius = .01f;							// Radius of the overlap circle to determine if the player can stand up
	Animator anim;										// Reference to the player's animator component.

	bool doubleJump = false;							// Bool to veryfy if double jump has been used. 
	Vector2 startPosition;								// Start position set initially then used to reposition player on Game Restart

	//Gradual Speedup Variables
	public float speedUpInterval;						// Interval between each speedup
	public float speedIncrease;							// Amount by which max speed is increased every interval. 
	float elapsedTime = 0f;								// Record of how much time passess between intervals then reset


	static int boosts;

    void Awake() {
		// Setting up references.
		groundCheck = transform.Find("GroundCheck");
		ceilingCheck = transform.Find("CeilingCheck");
		anim = GetComponent<Animator>();
	}

	void Start () {
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		startPosition = transform.localPosition;
		renderer.enabled = false;
		rigidbody2D.isKinematic = true;
		enabled = false;
	}

	void Update () {
		distanceTraveled = transform.localPosition.x;

		elapsedTime += Time.deltaTime;
		if (elapsedTime >= speedUpInterval)
		{
			maxSpeed += speedIncrease;
			elapsedTime = 0;
		}
	}

	void FixedUpdate() {
		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround);
		anim.SetBool("Ground", grounded);

		// Set the vertical animation
		anim.SetFloat("vSpeed", rigidbody2D.velocity.y);

		if (grounded)
		{
			doubleJump = false;
		}
	}


	public void Move(float move, bool crouch, bool jump) {
		// If crouching, check to see if the character can stand up
		if(!crouch && anim.GetBool("Crouch"))
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if( Physics2D.OverlapCircle(ceilingCheck.position, ceilingRadius, whatIsGround))
				crouch = true;
		}

		// Set whether or not the character is crouching in the animator
		anim.SetBool("Crouch", crouch);

		//only control the player if grounded or airControl is turned on
		if(grounded || airControl)
		{
			// Reduce the speed if crouching by the crouchSpeed multiplier
			move = (crouch ? move * crouchSpeed : move);

			// The Speed animator parameter is set to the absolute value of the horizontal input.
			anim.SetFloat("Speed", Mathf.Abs(move));

			// Move the character
			rigidbody2D.velocity = new Vector2 (move * maxSpeed, rigidbody2D.velocity.y);
			
			// If the input is moving the player right and the player is facing left...
			if(move > 0 && !facingRight)
				// ... flip the player.
				Flip();
			// Otherwise if the input is moving the player left and the player is facing right...
			else if(move < 0 && facingRight)
				// ... flip the player.
				Flip();
		}

        // If the player should jump...
        if ((grounded || !doubleJump) && jump) {
            // Add a vertical force to the player.
            anim.SetBool("Ground", false);

			rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, 0);

            rigidbody2D.AddForce(new Vector2(0f, jumpForce));

			if (!grounded)
			{
				doubleJump = true;
			}
        }
	}

	
	void Flip () {
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public void VerticalFlip (){
		// Switch the way the player is labelled as facing.
		normalGravity = !normalGravity;
		
		// Multiply the player's y local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.y *= -1;
		transform.localScale = theScale;
		jumpForce = jumpForce * -1;
		Debug.Log ("Switched Gravity");
	}

	public static void AddBoost() {
		boosts += 1;
		//GUIManager.SetBoosts (boosts);
	}

	void GameOver (){
		renderer.enabled = false;
		rigidbody2D.isKinematic = true;
		enabled = false;
	}

	void GameStart (){
		boosts = 0;
		//GUIManager.SetBoosts (boosts);
		distanceTraveled = 0f;
		transform.localPosition = startPosition;
		renderer.enabled = true;
		rigidbody2D.isKinematic = false;
		if (!normalGravity)
		{
			VerticalFlip ();
		}
		elapsedTime = 0;
		maxSpeed = 10f;
		enabled = true;
	}

	public void SetMaxSpeed (float speed){
		if (maxSpeed + speed >= 10f)
		{
			maxSpeed += speed;
		}
		else{
			maxSpeed = 10f;
		}
	}
}
