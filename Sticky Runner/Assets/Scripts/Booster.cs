using UnityEngine;
using System.Collections;

public class Booster : MonoBehaviour {
	 
	public Vector3 positionOffset, roationVelocity;
	public float recycleOffset, spawnChance;

	// Use this for initialization
	void Start () {
		GameEventManager.GameOver += GameOver;
		gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.localPosition.x + recycleOffset < Runner.distanceTraveled)
		{
			gameObject.SetActive (false);
			return;
		}
		transform.Rotate(roationVelocity * Time.deltaTime);
	}

	public void SpawnIfAvailable (Vector3 position){
		if (gameObject.activeSelf || spawnChance <= Random.Range (0, 100))
		{
			return;
		}
		gameObject.SetActive (true);
		transform.localPosition = position + positionOffset;
	}

	void GameOver(){
		gameObject.SetActive(false);
	}

	void OnTriggerEnter(){
		Runner.AddBoost();
		gameObject.SetActive (false);
	}
}
