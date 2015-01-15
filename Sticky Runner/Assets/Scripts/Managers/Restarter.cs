using UnityEngine;
using System.Collections;

public class Restarter : MonoBehaviour {

	public float timer = 1f;
	public bool gameOverTextShown = true;

	// Use this for initialization
	void Start () {
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
	}
	
	void Update () {
		if (Input.GetButtonDown ("Jump") && gameOverTextShown)
		{
			GameEventManager.TriggerGameStart ();
		}
	}

	void GameStart (){
		StopAllCoroutines ();
		gameOverTextShown = false;
		enabled = false;
	}

	void GameOver (){
		enabled = true;
		StartCoroutine (GameRestart ());
	}

	IEnumerator GameRestart (){
		yield return new WaitForSeconds (timer);
		gameOverTextShown = true;
	}

}
