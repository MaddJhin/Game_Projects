using UnityEngine;
using System.Collections;

public class PlatformCollision : MonoBehaviour {

		
	public BoxCollider2D platform;
	public bool oneWay = false;

	GameObject player;
	Collider2D playerBoxCollider;
	Collider2D playerCircleCollider;

	// Use this for initialization
	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerBoxCollider = player.GetComponent<BoxCollider2D>();
		playerCircleCollider = player.GetComponent<CircleCollider2D>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (oneWay)
		{
			Physics2D.IgnoreCollision (playerBoxCollider, platform.collider2D);
			Physics2D.IgnoreCollision (playerCircleCollider, platform.collider2D);
		}
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		oneWay = true;
	}
	
	
	void OnTriggerExit2D(Collider2D other)
	{
		oneWay = false;
	}
}
