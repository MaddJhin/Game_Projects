using UnityEngine;
using System.Collections;

public class Restarter : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
	}
	
	void Update () {
		if (Input.GetButtonDown ("Jump"))
		{
			GameEventManager.TriggerGameStart ();
		}
	}

	void GameStart (){
		enabled = false;
	}

	void GameOver (){
		enabled = true;
	}
}
