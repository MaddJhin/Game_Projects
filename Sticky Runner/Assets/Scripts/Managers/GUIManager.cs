using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {

	public Text gameOverText, instructionsText, runnerText, distanceTraveled, boosts;

	static GUIManager instance;

	// Use this for initialization
	void Start () {
		instance = this;
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		gameOverText.enabled = false;	
		distanceTraveled.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Jump"))
		{
			GameEventManager.TriggerGameStart ();
		}
	}

	void GameStart(){
		gameOverText.enabled = false;
		instructionsText.enabled = false;
		runnerText.enabled = false;
		enabled = false;
	}

	void GameOver(){
		gameOverText.enabled = true;
		instructionsText.enabled = true;
		enabled = true;
	}

	public static void SetDistanceTraveled(float distance){
		instance.distanceTraveled.text = distance.ToString("f0");
	}

	public static void SetBoosts(int boost){
		instance.boosts.text = "Boosts: " + boost.ToString();
	}
}
