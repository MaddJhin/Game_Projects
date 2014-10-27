using UnityEngine;
using System.Collections;

public class ParticleManager : MonoBehaviour {

	public ParticleSystem[] particleSystems;

	// Use this for initialization
	void Start () {
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameOver ();
	}
	
	void GameStart(){
		for (int i = 0; i < particleSystems.Length; i++)
		{
			particleSystems[i].enableEmission = true;
		}
	}

	void GameOver(){
		for (int i = 0; i < particleSystems.Length; i++)
		{
			particleSystems[i].enableEmission = false;
		}
	}
}
