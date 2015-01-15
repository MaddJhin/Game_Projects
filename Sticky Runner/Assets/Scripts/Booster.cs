using UnityEngine;
using System.Collections;

public class Booster : MonoBehaviour {

	public enum BoosterBehaviors {								// List of different booster types
		SpeedUp,
		SpeedDown,
		Invincibility
	}

	public Vector3 rotationVelocity;							// Vector for rotation during runtime. 
	public PlatformerCharacter2D player;						// Reference to the player. Needed to change maxSpeed on Player
	public float speedDecrease = -5f;							// Value the slowing booster slows by
	public float speedIncrease = 5f;							// Value the boosting booster increases by
	public Material[] materials; 								// Material Array. Each booster type picks the relevant material

	float speedChange;											// Actual variable used when changing speeds. Each booster type sets the value
	int chooseBooster;											// Int to choose which behavior 
	string boosterNotice;

	static int boosterBehaviorCount = 3;						// How many boosters behavior variations there are
	public bool setInvincible;									// Keeps track if player is set invincible 

	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlatformerCharacter2D>();
	}

	void Start () {
		Initialize ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(rotationVelocity * Time.deltaTime);
	}

	void OnTriggerEnter2D (Collider2D collider){
		if (collider.gameObject.tag == "Player")
		{
			Debug.Log ("Player Hit Boost");
			player.SetMaxSpeed (speedChange);
			GUIManager.BoostNotice (boosterNotice);
			if (setInvincible == true)
			{
				player.BecomeInvincible();
			}
			this.renderer.enabled = false;
		}
	}

	void SpeedUp (){
		speedChange = speedIncrease;
		renderer.material = materials[chooseBooster];
		boosterNotice = "Speed Boost!";
		setInvincible = false;
	}

	void SpeedDown () {
		speedChange = speedDecrease;
		renderer.material = materials[chooseBooster];
		boosterNotice = "Slowed Down!";
		setInvincible = false;
	}

	void Invincibility () {
		speedChange = 0f;
		renderer.material = materials[chooseBooster];
		boosterNotice = "Invincibility!";
		setInvincible = true;
	}

	public void Initialize () {
		this.renderer.enabled = true;
		chooseBooster = Random.Range (0, boosterBehaviorCount);
		if (chooseBooster == 0)
		{
			SpeedUp();
		}
		else if (chooseBooster == 1)
		{
			SpeedDown();
		}
		else if (chooseBooster == 2)
		{
			Invincibility();
		}
	}
}
