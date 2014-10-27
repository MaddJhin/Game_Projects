using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Vector2 gravity;

	// Use this for initialization
	void Start () {
		Physics2D.gravity = gravity;
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKey (KeyCode.R))
		{
			Application.LoadLevel (0);
		}
	}

	public static void InvertGravity () {
		Physics2D.gravity = new Vector2 (0f, Physics2D.gravity.y * -1f);
	}

}
