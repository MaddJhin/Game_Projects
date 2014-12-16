using UnityEngine;
using System.Collections;

public class CoinTrigger : MonoBehaviour {

	public int scoreValue = 10;

	void OnTriggerEnter2D (){
		GUIManager.score += scoreValue;
		Destroy(this.gameObject);
		Debug.Log (this.gameObject + "Destroyed by Coin Trigger");
	}
}
