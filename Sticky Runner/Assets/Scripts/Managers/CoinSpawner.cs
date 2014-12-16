using UnityEngine;
using System.Collections;

public class CoinSpawner : MonoBehaviour {

	public GameObject coinPrefab;
	public int coinCount;
	public float spawnWait, waveWait, startWait;
	public float minY, maxY;

	GameObject[] existingCoins;

	// Use this for initialization
	void Start () {
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
	}
	
	IEnumerator SpawnCoins ()
	{
		yield return new WaitForSeconds(startWait);

		while (true){

			Vector2 spawnPosition = new Vector2 (transform.position.x, Random.Range (minY, maxY));
			Quaternion spawnQuaternion = Quaternion.identity;

			for (int i = 0; i < coinCount; i++)
			{
				Instantiate (coinPrefab, spawnPosition, spawnQuaternion);
				spawnPosition.y += 0.63f;
				Instantiate (coinPrefab, spawnPosition, spawnQuaternion);
				spawnPosition.x += 0.63f;
				spawnPosition.y -= 0.63f;
				yield return new WaitForSeconds (spawnWait);
			}

			yield return new WaitForSeconds (waveWait);
		}
	}

	void GameStart (){
		existingCoins = GameObject.FindGameObjectsWithTag ("Coin");
		foreach (GameObject coin in existingCoins)
		{
			Destroy(coin);
		}
		StartCoroutine ("SpawnCoins");
	}

	void GameOver (){
		StopCoroutine ("SpawnCoins");
	}

}
