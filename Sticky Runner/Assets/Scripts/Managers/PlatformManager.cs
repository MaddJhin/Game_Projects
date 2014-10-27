using UnityEngine;
using System.Collections.Generic;

public class PlatformManager : MonoBehaviour {

	public Transform platformPrefab;
	public int numberOfObjects;
	public float recycleOffset;
	public Vector3 startPosition;
	public Vector3 minSize, maxSize, minGap, maxGap;
	public float minY, maxY;

	public Material[] materials;
	public PhysicMaterial[] physicsMaterials;
	public Booster booster;

	Queue<Transform> objectQueue;
	Vector3 nextPosition;

	// Use this for initialization
	void Start () {

		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		objectQueue = new Queue<Transform>(numberOfObjects);

		for (int i = 0; i < numberOfObjects; i++)
		{
			objectQueue.Enqueue((Transform)Instantiate (
				platformPrefab, new Vector3(0f, 0f, -200f), Quaternion.identity));
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

	void GameOver(){
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
		booster.SpawnIfAvailable(position);

		Transform o = objectQueue.Dequeue();
		o.localScale = scale;
		o.localPosition = position;
		int materialIndex = Random.Range (0, materials.Length);
		o.collider.material = physicsMaterials[materialIndex];
		o.renderer.material = materials[materialIndex];
		objectQueue.Enqueue (o);

		nextPosition += new Vector3 (
			Random.Range (minGap.x, maxGap.x) + scale.x,
			Random.Range (minGap.y, maxGap.y),
			Random.Range (minGap.z, maxGap.z));

		if (nextPosition.y < minY)
		{
			nextPosition.y = minY + maxGap.y;
		}

		if (nextPosition.y > maxY)
		{
			nextPosition.y = maxY - maxGap.y;
		}
	}
}
