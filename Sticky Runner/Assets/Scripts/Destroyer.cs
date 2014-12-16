using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D other){

		if (other.gameObject.tag == "Player")
		{
			GameEventManager.TriggerGameOver();
			return;
		}

		if (other.gameObject.tag == "Coin")
		{
			Destroy (other.gameObject);
			Debug.Log (other.gameObject + "Destroyed by Destroyer");
		}

//		if (other.gameObject.transform.parent)
//		{
//			Destroy (other.gameObject.transform.parent.gameObject);
//		}
//
//		else
//		{
//			Destroy (other.gameObject);
//		}
	}
		
}
