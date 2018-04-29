using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOverviewer : MonoBehaviour {

	//Variables that are to be read by other things
	public bool MoveForward = false, MoveBackward = false, MoveLeft = false, MoveRight = false;
	public bool LeftClick = false, RightClick = false, CenterClick = false, Jump = false;
	public float Mousex = 0, Mousey = 0, Mousedx = 0, Mousedy = 0, MouseScrollRaw = 0, MouseScroll = 0, MouseScrollConstant = 200;

	private float MouseScrollLast = 0;

	public int NumKey = 0;

	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {

		//Movement Intentions
		MoveForward = Input.GetKey (KeyCode.W);
		MoveBackward = Input.GetKey (KeyCode.S);
		MoveLeft = Input.GetKey (KeyCode.A);
		MoveRight = Input.GetKey (KeyCode.D);
		Jump = Input.GetKey (KeyCode.Space);

		//Mouse Intentions
		LeftClick = Input.GetMouseButton (0);
		RightClick = Input.GetMouseButton (1);
		CenterClick = Input.GetMouseButton (2);
		Mousex = Input.mousePosition.x;
		Mousey = Input.mousePosition.y;
		MouseScrollRaw = Input.GetAxis ("Mouse ScrollWheel");

		MouseScroll = MouseScrollRaw * Time.deltaTime * MouseScrollConstant + MouseScrollLast; 
		MouseScrollLast = MouseScroll;

		//Mouse movement intentions
		Mousedx = Input.GetAxis ("Mouse X");
		Mousedy = Input.GetAxis ("Mouse Y");

		//Release mouse lock if escape is pressed
		if(Input.GetKeyDown(KeyCode.Escape))
			Cursor.lockState = CursorLockMode.None;

		if (Input.GetKeyDown (KeyCode.Alpha1))
			NumKey = 1;
		else if (Input.GetKeyDown (KeyCode.Alpha2))
			NumKey = 2;
		else if (Input.GetKeyDown (KeyCode.Alpha3))
			NumKey = 3;
		else if (Input.GetKeyDown (KeyCode.Alpha4))
			NumKey = 4;
		else if (Input.GetKeyDown (KeyCode.Alpha5))
			NumKey = 5;
		else if (Input.GetKeyDown (KeyCode.Alpha6))
			NumKey = 6;
		else if (Input.GetKeyDown (KeyCode.Alpha7))
			NumKey = 7;
		else if (Input.GetKeyDown (KeyCode.Alpha8))
			NumKey = 8;
		else if (Input.GetKeyDown (KeyCode.Alpha9))
			NumKey = 9;
		else if (Input.GetKeyDown (KeyCode.Alpha0))
			NumKey = 0;



	}
}
