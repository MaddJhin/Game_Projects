using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {

	public Text gameOverText, instructionsText, runnerText, scoreText, boosts;

	public static int score;

	Animator anim;

	void Awake (){
		anim = GetComponent<Animator>();
	}

	void Update () {
		scoreText.text = "Score: " + Mathf.Round(PlatformerCharacter2D.distanceTraveled + score);
	}

	// Use this for initialization
	void Start () {
//		instance = this;
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		gameOverText.enabled = false;	
		scoreText.text = "";
		boosts.text = "";
	}

	void GameStart(){
		gameOverText.enabled = false;
		instructionsText.enabled = false;
		runnerText.enabled = false;
		scoreText.enabled = true;
		anim.SetTrigger ("GameStart");
//		enabled = false;
	}

	void GameOver(){
		gameOverText.enabled = true;
		instructionsText.enabled = true;
		anim.SetTrigger ("GameOver");
//		enabled = true;
	}

//	public static void SetDistanceTraveled(float distance){
//		instance.scoreText.text = "Distance: " + distance.ToString("f0");
//	}

//	public static void SetBoosts(int boost){
//		instance.boosts.text = "Boosts: " + boost.ToString();
//	}
}
