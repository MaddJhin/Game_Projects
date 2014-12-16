using UnityEngine;
using System.Collections.Generic;

public class GroundManager : MonoBehaviour {

	public GameObject[] spawnPrefabs;
	public Vector2 startPosition;
	public Vector2 minGap, maxGap;
	public int numberOfObjects;
	public float recycleOffset;
	
	Vector2 nextPosition;
	Queue<GameObject> groundQueue;


	// Use this for initialization
	void Start () {
		GameEventManager.GameStart += GameStart;
		groundQueue = new Queue<GameObject>(numberOfObjects);
		for (int i = 0; i < numberOfObjects; i++) 
		{
			groundQueue.Enqueue(Instantiate(spawnPrefabs[Random.Range(0, spawnPrefabs.Length)]) as GameObject);
		}
		nextPosition = startPosition;
		for (int i = 0; i < numberOfObjects; i++) 
		{
			Recycle ();
		}
	}
	
	void Update () {
		if (groundQueue.Peek().transform.localPosition.x + recycleOffset < PlatformerCharacter2D.distanceTraveled)
		{
			Recycle ();
		}
	}

	void Recycle () {
		GameObject obj = groundQueue.Dequeue();
		obj.transform.localPosition = nextPosition;
		nextPosition.x += obj.collider2D.bounds.size.x + Random.Range(minGap.x, maxGap.x);
		nextPosition.y = Random.Range(minGap.y, maxGap.y);
		groundQueue.Enqueue (obj);
	}

	void GameStart (){
		nextPosition = startPosition;
		for (int i = 0; i < numberOfObjects; i++) 
		{
			Recycle ();
		}
	}
}
