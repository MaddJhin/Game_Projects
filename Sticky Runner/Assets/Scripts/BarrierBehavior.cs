using UnityEngine;
using System.Collections;

public class BarrierBehavior : MonoBehaviour {

	public enum BehaviorOption {
		Clockwise,
		CounterClockwise,
		Horizontal,
		Vertical
	}

	private delegate void BehaviorDelegate ();
	
	public float minRotation, maxRotation, minSpeed, maxSpeed;
	public const int Count = 4;
	public BehaviorOption behavior;
	public Vector3 startingPos;

	PlatformerCharacter2D player;
	
	float rotateSpeed;
	float speed;
	BehaviorDelegate behaviorDelegate;

	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlatformerCharacter2D>();
	}

	void Start () {
		startingPos = GetComponent<Transform>().position;
		Initialize ();
	}

	// Update is called once per frame
	void Update () {

		if (behavior == BehaviorOption.Clockwise)
		{
			behaviorDelegate = Clockwise;
		}
		else if (behavior == BehaviorOption.CounterClockwise)
		{
			behaviorDelegate = CounterClockwise;
		}
		else if (behavior == BehaviorOption.Horizontal)
		{
			behaviorDelegate = Horizontal;
		}
		else if (behavior == BehaviorOption.Vertical)
		{
			behaviorDelegate = Vertical;
		}

		behaviorDelegate();
	}
	
	void OnTriggerEnter2D (Collider2D collider){
		if (collider.gameObject.tag == "Player" && player.invincible == false)
		{
			GameEventManager.TriggerGameOver ();
		}
	}

	public void Initialize (){
		rotateSpeed = Random.Range (minRotation, maxRotation);
		speed = Random.Range (minSpeed, maxSpeed);
		behavior = RandomValue;
	}
	
	void Clockwise (){
		transform.position = startingPos;
		transform.Rotate (0f, 0f, -rotateSpeed * Time.deltaTime);
	}
	
	void CounterClockwise (){
		transform.position = startingPos;
		transform.Rotate (0f, 0f, rotateSpeed * Time.deltaTime);
	}
	
	void Horizontal (){
		transform.eulerAngles = new Vector3 (0f, 0f, 90f);
		transform.position = startingPos + transform.up * Mathf.Sin (Time.time * speed);
	}
	
	void Vertical (){
		transform.eulerAngles = new Vector3 (0f, 0f, 0f);
		transform.position = startingPos + transform.up * Mathf.Sin (Time.time * speed);
	}

	private BehaviorOption RandomValue {
		get {
			return (BehaviorOption)Random.Range(0, Count);
		}
	}
}