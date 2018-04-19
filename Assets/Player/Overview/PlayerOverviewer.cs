using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOverviewer : MonoBehaviour {

	//Variables that are to be read by other things
	public bool MoveForward = false, MoveBackward = false, MoveLeft = false, MoveRight = false;
	public bool LeftClick = false, RightClick = false, CenterClick = false;
	public float Mousex = 0, Mousey = 0;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		//Movement Intentions
		MoveForward = Input.GetKey (KeyCode.W);
		MoveBackward = Input.GetKey (KeyCode.S);
		MoveLeft = Input.GetKey (KeyCode.A);
		MoveRight = Input.GetKey (KeyCode.D);

		//Mouse Intentions
		LeftClick = Input.GetMouseButton (0);
		RightClick = Input.GetMouseButton (1);
		CenterClick = Input.GetMouseButton (2);
		Mousex = Input.mousePosition.x;
		Mousey = Input.mousePosition.y;


	}
}
