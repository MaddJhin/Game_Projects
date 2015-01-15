using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {

	public Text gameOverText, instructionsText, runnerText, scoreText, boostGainNotice, pauseText;

	public static int score;

	private static GUIManager instance;
	Animator anim;
	Animator boosterAnim;
	bool gamePaused = false;


	void Awake (){
		instance = this;
		anim = GetComponent<Animator>();
		boosterAnim = boostGainNotice.GetComponent<Animator>();
	}

	void Start () {
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		gameOverText.enabled = false;	
		scoreText.text = "";
		boostGainNotice.text = "";
		boostGainNotice.enabled = true;
		pauseText.enabled = false;
	}

	void Update () {
		scoreText.text = "Score: " + Mathf.Round(PlatformerCharacter2D.distanceTraveled + score);

		if (Input.GetKeyDown (KeyCode.Escape) && !gamePaused)
		{
			PauseGame();
		}
		else if (Input.GetKeyDown (KeyCode.Escape) && gamePaused)
		{
			ResumeGame();
		}
	}

	void GameStart(){
		gameOverText.enabled = false;
		instructionsText.enabled = false;
		runnerText.enabled = false;
		boostGainNotice.enabled = true;
		scoreText.enabled = true;
		score = 0;
		anim.SetTrigger ("GameStart");
		anim.ResetTrigger ("GameOver");
	}

	void GameOver(){
		gameOverText.enabled = true;
		instructionsText.enabled = true;
		boostGainNotice.text = "";
		anim.SetTrigger ("GameOver");
		boosterAnim.ResetTrigger ("BoosterGain");
	}

	void PauseGame() {
		Time.timeScale = 0;
		pauseText.enabled = true;
		gamePaused = true;
	}

	void ResumeGame() {
		Time.timeScale = 1;
		pauseText.enabled = false;
		gamePaused = false;
	}

	public static void BoostNotice(string boost){
		instance.boostGainNotice.text = boost;
		instance.boosterAnim.Play ("BoostGain 0");
	}
}
