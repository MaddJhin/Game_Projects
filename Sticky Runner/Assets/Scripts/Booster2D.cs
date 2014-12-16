using UnityEngine;
using System.Collections;

public class Booster2D : MonoBehaviour {

	public float recycleOffset, spawnChance;
	public Vector3 rotationVelocity;
	public Vector2 positionOffset;

	int[] offsetDirection;

	// Use this for initialization
	void Awake () {
		offsetDirection = new int[] {-1, 1};
		gameObject.SetActive(true);
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

	public void SpawnIfAvailable (Vector2 position){
		if (gameObject.activeSelf || spawnChance <= Random.Range (0, 100))
		{
			return;
		}
		gameObject.SetActive (true);
		transform.localPosition = position + positionOffset * offsetDirection[Random.Range(0, offsetDirection.Length)];
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
