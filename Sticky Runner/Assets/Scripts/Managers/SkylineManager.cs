using UnityEngine;
using System.Collections.Generic;

public class SkylineManager : MonoBehaviour {

	public Transform prefab;
	public int numberOfObjects;
	public float recycleOffset;
	public Vector3 startPosition;
	public Vector3 minSize, maxSize;

	Queue<Transform> objectQueue;
	Vector3 nextPosition;

	// Use this for initialization
	void Start () {
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		objectQueue = new Queue<Transform>(numberOfObjects);

		for (int i = 0; i < numberOfObjects; i++)
		{
			objectQueue.Enqueue((Transform)Instantiate(
				prefab, new Vector3(0, 0, -200), Quaternion.identity));
		}
		enabled = false;
	}

	void GameStart(){
		nextPosition = startPosition;

		for (int i = 0; i < numberOfObjects; i++)
		{
			Recycle();
		}
		enabled = true;
	}

	void GameOver (){
		enabled = false;
	}

	void Update () {
		if (objectQueue.Peek().localPosition.x + recycleOffset < Runner.distanceTraveled)
		{
			Recycle();
		}
	}

	void Recycle (){
		Vector3 scale = new Vector3 (
			Random.Range (minSize.x, maxSize.x),
			Random.Range (minSize.y, maxSize.y),
			Random.Range (minSize.z, maxSize.z));

		Vector3 position = nextPosition;
		position.x += scale.x * 0.5f;
		position.y += scale.y * 0.5f;

		Transform o = objectQueue.Dequeue();
		o.localScale = scale;
		o.localPosition = position;
		nextPosition.x += scale.x;
		objectQueue.Enqueue (o);
	}
}
