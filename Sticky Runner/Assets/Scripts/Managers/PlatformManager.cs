using UnityEngine;
using System.Collections.Generic;

public class PlatformManager : MonoBehaviour {

	public GameObject[] spawnPrefabs;
	public Vector2 startPosition;
	public Vector2 minGap, maxGap;
	public int numberOfObjects;
	public float recycleOffset;

	public Booster2D booster;

	Queue<GameObject> objectQueue;
	Vector3 nextPosition;

	// Use this for initialization
	void Start () {

		objectQueue = new Queue<GameObject>(numberOfObjects);
		for (int i = 0; i < numberOfObjects; i++) 
		{
			objectQueue.Enqueue(Instantiate(spawnPrefabs[Random.Range(0, spawnPrefabs.Length)]) as GameObject);
		}
		nextPosition = startPosition;
		for (int i = 0; i < numberOfObjects; i++) 
		{
			Recycle ();
		}
	}

	void Update () {
		if (objectQueue.Peek().transform.localPosition.x + recycleOffset < PlatformerCharacter2D.distanceTraveled)
		{
			Recycle ();
		}
	}

	void Recycle (){

		Vector2 position = nextPosition;
		booster.SpawnIfAvailable(position);

		GameObject obj = objectQueue.Dequeue();
		obj.transform.localPosition = position;
		objectQueue.Enqueue (obj);

		nextPosition.x += obj.collider2D.bounds.size.x + Random.Range(minGap.x, maxGap.x);
		nextPosition.y = Random.Range(minGap.y, maxGap.y);
	}
}
