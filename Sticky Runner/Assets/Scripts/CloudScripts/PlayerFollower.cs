using UnityEngine;
using System.Collections;

public class PlayerFollower : MonoBehaviour {

	public GameObject player;

	CloudFlow[] flowScripts;

	void Start (){

		flowScripts = GetComponentsInChildren<CloudFlow>();
	}

	// Update is called once per frame
	void Update () {

		if (player.rigidbody2D.velocity.x == 0)
		{
			foreach (CloudFlow script in flowScripts)
			{
				script.enabled = false;
			}
		}

		if (player.rigidbody2D.velocity.x > 0)
		{
			foreach (CloudFlow script in flowScripts)
			{
				script.enabled = true;
			}
		}
	}
}
