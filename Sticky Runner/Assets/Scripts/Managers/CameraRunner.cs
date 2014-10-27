using UnityEngine;
using System.Collections;

public class CameraRunner : MonoBehaviour {

	public Transform player;
	public float cameraOffset = 6f;
	public float yOffset = 5.7f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		transform.position = new Vector3 (player.position.x + cameraOffset, yOffset, -10f);
	}
}
