using UnityEngine;
using System.Collections;

public class BarrierBehavior : MonoBehaviour {

	public float minRotation;
	public float maxRotation;

	float rotateSpeed;

	void Start () {
		rotateSpeed = Random.Range (minRotation, maxRotation);
	}
	// Update is called once per frame
	void Update () {
		transform.Rotate (0f, 0f, rotateSpeed * Time.deltaTime);
	}

	void OnTriggerEnter2D (Collider2D collider){
		if (collider.gameObject.tag == "Player")
		{
			collider.gameObject.SetActive (false);
		}

	}
}
