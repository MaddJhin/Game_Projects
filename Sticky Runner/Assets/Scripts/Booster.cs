using UnityEngine;
using System.Collections;

public class Booster : MonoBehaviour {

	public Vector3 rotationVelocity;
	public PlatformerCharacter2D player;
	public float speedDecrease;

	// Use this for initialization
	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlatformerCharacter2D>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(rotationVelocity * Time.deltaTime);
	}

	void OnTriggerEnter2D (Collider2D collider){
		if (collider.gameObject.tag == "Player")
		{
			Debug.Log ("Player Hit Boost");
			SpeedUp();
			gameObject.SetActive (false);
		}
	}

	void SpeedUp (){
		player.SetMaxSpeed (speedDecrease);
	}

	void SpeedDown () {

	}
}
