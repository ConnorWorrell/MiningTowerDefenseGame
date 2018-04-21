using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public GameObject PlayerOverviewerPrefab, Camera;
	private GameObject PlayerOverviewer;

	private Rigidbody Ridgidbody;

	private Vector3 ForceApplying, CameraRotation;

	public bool Left = false, Right = false, Forward = false, Backward = false, Jump = false;
	public bool OnGround = false;

	public float ForwardMovementMultiplier = 30, HorizontalMovementMultiplier = 30, JumpForceMultiplier = 2;
	public float JumpDistance = 0.5f, DragForceXGround = 0.5f, DragForceZGround = 0.5f, DragForceXAir = 0.04f, DragForceZAir = 0.04f;
	public float RotationMultX = 100, RotationMultY = 70;
	public float CameraMinRot = -115, CameraMaxRot = 115;
	private float Mousedx = 0, Mousedy = 0;

	// Use this for initialization
	void Start () {
		//Add PlayerOverviewer to the Scene if it is not alreay in the scene
		if (GameObject.Find ("PlayerOverviewer") == null) {
			GameObject go = Instantiate (PlayerOverviewerPrefab, new Vector3 (0, 0, 0), Quaternion.identity);
			go.name = go.name.Replace ("(Clone)", "");
		}
		PlayerOverviewer = GameObject.Find ("PlayerOverviewer");

		//Find Rigidbody component
		Ridgidbody = GetComponent<Rigidbody>();
		if (Ridgidbody == null)
			Debug.LogError ("Player dosent have a rigidbody attached");

		//initilise Camera rotation variable
		CameraRotation = new Vector3 (0, 0, 0);
	}
	
	void Update () {
		GetInputs ();
		RotateCamera ();
	}

	void FixedUpdate () {
		ApplyMovements ();
	}

	//Does the vertical rotation of the camera, and horizontal rotation of the main body
	void RotateCamera () {
		//Apply rotation to the main body
		gameObject.transform.Rotate (new Vector3 (0, Mousedx * RotationMultX * Time.deltaTime, 0));

		//Apply Rotation to a variable which will be written to camera transform
		CameraRotation = new Vector3 (-1 * Mousedy * RotationMultY * Time.deltaTime + CameraRotation.x, 0, 0);

		//Limit camera rotation
		if (CameraRotation.x > CameraMaxRot)
			CameraRotation = new Vector3 (CameraMaxRot, CameraRotation.y, CameraRotation.z);
		else if (CameraRotation.x < CameraMinRot)
			CameraRotation = new Vector3 (CameraMinRot, CameraRotation.y, CameraRotation.z);
		//Write camera rotation to the camera object
		Camera.transform.localEulerAngles = CameraRotation;
	}

	void GetInputs () {
		//Input inputs from PlayerOverviewer
		Left = PlayerOverviewer.GetComponent<PlayerOverviewer> ().MoveLeft;
		Right = PlayerOverviewer.GetComponent<PlayerOverviewer> ().MoveRight;
		Forward = PlayerOverviewer.GetComponent<PlayerOverviewer> ().MoveForward;
		Backward = PlayerOverviewer.GetComponent<PlayerOverviewer> ().MoveBackward;
		Jump = PlayerOverviewer.GetComponent<PlayerOverviewer> ().Jump;

		Mousedx = PlayerOverviewer.GetComponent<PlayerOverviewer> ().Mousedx;
		Mousedy = PlayerOverviewer.GetComponent<PlayerOverviewer> ().Mousedy;
	}

	void ApplyMovements(){
		//Determine wether an object is beneath the player
		RaycastHit hit;
		Debug.DrawRay (transform.position, transform.TransformDirection (Vector3.down)*JumpDistance);
		if (Physics.Raycast (transform.position, transform.TransformDirection (Vector3.down), JumpDistance))
			OnGround = true;
		else
			OnGround = false;

		//Read the velocity of the player
		Vector3 Velocity = Ridgidbody.velocity;

		//Apply jump velocity if the player has the intention of jumping
		Ridgidbody.velocity = new Vector3(Velocity.x,Velocity.y + (Jump ? 1 : 0) * JumpForceMultiplier * (OnGround ? 1 : 0),Velocity.z);

		//Apply running force if on ground, otherwise don't apply any force
		if(OnGround)
			ForceApplying = new Vector3(
			((Right ? 1 : -1) - (Left ? 1 : -1)), 0,
			((Forward ? 1 : -1) - (Backward ? 1 : -1)));
		else
			ForceApplying = new Vector3(0,0,0);
		//Normalise force so going diagonally isn't twice the speed of going forward/backward
		ForceApplying.Normalize ();

		//Multiply normalized force by scaling factors
		ForceApplying.Set (ForceApplying.x * HorizontalMovementMultiplier, 0, ForceApplying.z * ForwardMovementMultiplier);

		//Rotate forces to allign with rotation of camera
		ForceApplying = ForceApplying.x * gameObject.transform.right + gameObject.transform.forward * ForceApplying.z + gameObject.transform.up * ForceApplying.y;

		//Apply friction/air resistance porportional to v|v| depending on wether the player is on the ground
		if (OnGround)
			ForceApplying.Set (ForceApplying.x - Velocity.x * Mathf.Abs(Velocity.x) * DragForceXGround, ForceApplying.y, ForceApplying.z - Velocity.z * Mathf.Abs(Velocity.z) * DragForceZGround);
		else
			ForceApplying.Set (ForceApplying.x - Velocity.x * Mathf.Abs(Velocity.x) * DragForceXAir, ForceApplying.y, ForceApplying.z - Velocity.z * Mathf.Abs(Velocity.z) * DragForceZAir);

		Debug.Log (gameObject.transform.forward);

		//Apply the force ot the body
		Ridgidbody.AddForce (ForceApplying);
	}

}
