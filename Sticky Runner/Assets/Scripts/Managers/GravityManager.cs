using UnityEngine;
using System.Collections;

public class GravityManager : MonoBehaviour {

	public Vector2 startingGravity;

	// Use this for initialization
	void Start () {
		GameEventManager.GameStart += GameStart;
	}

	public static void InvertGravity () {
		Physics2D.gravity = new Vector2 (0f, Physics2D.gravity.y * -1f);
	}

	void GameStart (){
		Physics2D.gravity = startingGravity;
	}
}
