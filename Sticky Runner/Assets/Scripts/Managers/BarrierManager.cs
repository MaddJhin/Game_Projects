using UnityEngine;
using System.Collections.Generic;

public class BarrierManager : MonoBehaviour {

	public BarrierBehavior[] spawnPrefabs;
	public Vector2 startPosition;
	public Vector2 minGap, maxGap;
	public int numberOfObjects;
	public float recycleOffset;
	
	Vector2 nextPosition;
	Queue<BarrierBehavior> groundQueue;
	
	
	// Use this for initialization
	void Start () {
		GameEventManager.GameStart += GameStart;
		groundQueue = new Queue<BarrierBehavior>(numberOfObjects);
		for (int i = 0; i < numberOfObjects; i++) 
		{
			groundQueue.Enqueue(Instantiate(spawnPrefabs[Random.Range(0, spawnPrefabs.Length)], startPosition, Quaternion.identity) as BarrierBehavior);
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
		BarrierBehavior obj = groundQueue.Dequeue();
		obj.transform.localPosition = nextPosition;
		obj.startingPos = nextPosition;
		nextPosition.x += obj.collider2D.bounds.size.x + Random.Range(minGap.x, maxGap.x);
		nextPosition.y = Random.Range(minGap.y, maxGap.y);
		obj.Initialize ();
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
