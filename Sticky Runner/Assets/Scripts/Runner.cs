using UnityEngine;

public class Runner : MonoBehaviour {
	
	public static float distanceTraveled;
	
	public float acceleration;
	public Vector3 jumpVelocity, boostVelocity;
	
	public float gameOverY;

	private bool touchingPlatform;

	static int boosts;
	Rigidbody body;
	Vector3 startPosition;

	void Awake(){
		body = GetComponent<Rigidbody>();
	}

	void Start(){
		boosts = 0;
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		startPosition = transform.localPosition;
		renderer.enabled = false;
		body.isKinematic = true;
		enabled = false;
	}

	void Update() {
		distanceTraveled = transform.localPosition.x;
		GUIManager.SetDistanceTraveled(distanceTraveled);
		if (Input.GetButtonDown ("Jump"))
		{
			if (touchingPlatform)
			{
				body.AddForce (jumpVelocity, ForceMode.VelocityChange);
				touchingPlatform = false;
			}
			else if (boosts > 0)
			{
				body.AddForce (boostVelocity, ForceMode.VelocityChange);
				boosts -= 1;
				GUIManager.SetBoosts(boosts);
			}
		}

		if (transform.localPosition.y < gameOverY)
		{
			GameEventManager.TriggerGameOver();
		}
	}
	
	void FixedUpdate() {
		if(touchingPlatform == true)
		{
			body.AddForce(acceleration, 0f, 0f, ForceMode.Acceleration);
		}
	}
	
	void OnCollisionEnter() {
		touchingPlatform = true;
	}
	
	void OnCollisionExit() {
		touchingPlatform = false;
	}

	void GameStart(){
		distanceTraveled = 0;
		GUIManager.SetDistanceTraveled(distanceTraveled);
		boosts = 0;
		GUIManager.SetBoosts(boosts);
		transform.localPosition = startPosition;
		body.isKinematic = false;
		renderer.enabled = true;
		enabled = true;
	}

	void GameOver(){
		body.isKinematic = true;
		renderer.enabled = false;
		enabled = false;
	}

	public static void AddBoost() {
		boosts += 1;
		GUIManager.SetBoosts(boosts);
	}
}