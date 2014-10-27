using UnityEngine;
using System.Collections;

public class Boster2D : MonoBehaviour {

	public float recycleOffset;
	public Vector3 rotationVelocity;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.localPosition.x + recycleOffset < PlatformerCharacter2D.distanceTraveled)
		{
			gameObject.SetActive (false);
			return;
		}
		transform.Rotate(rotationVelocity * Time.deltaTime);
	}

	void OnTriggerEnter2D (Collider2D collider){
		if (collider.gameObject.tag == "Player")
		{
			Debug.Log ("Player Hit Boost");
			PlatformerCharacter2D.AddBoost();
			gameObject.SetActive (false);
		}
	}
}
