using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public GameObject PlayerOverviewerPrefab;
	private GameObject PlayerOverviewer;

	private Rigidbody Ridgidbody;

	private Vector3 ForceApplying;

	public bool Left = false, Right = false, Forward = false, Backward = false, Jump = false;
	public bool OnGround = false;

	public float ForwardMovementMultiplier = 30, HorizontalMovementMultiplier = 30, JumpForceMultiplier = 2;
	public float JumpDistance = 0.5f, DragForceXGround = 0.5f, DragForceZGround = 0.5f, DragForceXAir = 0.04f, DragForceZAir = 0.04f;

	// Use this for initialization
	void Start () {
		//Add PlayerOverviewer to the Scene if it is not alreay in the scene
		if (GameObject.Find ("PlayerOverviewer") == null) {
			GameObject go = Instantiate (PlayerOverviewerPrefab, new Vector3 (0, 0, 0), Quaternion.identity);
			go.name = go.name.Replace ("(Clone)", "");
		}
		PlayerOverviewer = GameObject.Find ("PlayerOverviewer");

		Ridgidbody = GetComponent<Rigidbody>();
		if (Ridgidbody == null)
			Debug.LogError ("Player dosent have a rigidbody attached");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		GetInputs ();

		ApplyMovements ();
	}

	void GetInputs () {
		Left = PlayerOverviewer.GetComponent<PlayerOverviewer> ().MoveLeft;
		Right = PlayerOverviewer.GetComponent<PlayerOverviewer> ().MoveRight;
		Forward = PlayerOverviewer.GetComponent<PlayerOverviewer> ().MoveForward;
		Backward = PlayerOverviewer.GetComponent<PlayerOverviewer> ().MoveBackward;
		Jump = PlayerOverviewer.GetComponent<PlayerOverviewer> ().Jump;
	}

	void ApplyMovements(){

		RaycastHit hit;
		Debug.DrawRay (transform.position, transform.TransformDirection (Vector3.down)*JumpDistance);
		if (Physics.Raycast (transform.position, transform.TransformDirection (Vector3.down), JumpDistance))
			OnGround = true;
		else
			OnGround = false;

		Vector3 Velocity = Ridgidbody.velocity;

		Ridgidbody.velocity = new Vector3(Velocity.x,Velocity.y + (Jump ? 1 : 0) * JumpForceMultiplier * (OnGround ? 1 : 0),Velocity.z);

		if(OnGround)
			ForceApplying = new Vector3(
			((Right ? 1 : -1) - (Left ? 1 : -1)), 0,
			((Forward ? 1 : -1) - (Backward ? 1 : -1)));
		else
			ForceApplying = new Vector3(0,0,0);
		ForceApplying.Normalize ();

		ForceApplying.Set (ForceApplying.x * HorizontalMovementMultiplier, 0, ForceApplying.z * ForwardMovementMultiplier);

		if (OnGround)
			ForceApplying.Set (ForceApplying.x - Velocity.x * Mathf.Abs(Velocity.x) * DragForceXGround, ForceApplying.y, ForceApplying.z - Velocity.z * Mathf.Abs(Velocity.z) * DragForceZGround);
		else
			ForceApplying.Set (ForceApplying.x - Velocity.x * Mathf.Abs(Velocity.x) * DragForceXAir, ForceApplying.y, ForceApplying.z - Velocity.z * Mathf.Abs(Velocity.z) * DragForceZAir);

		Ridgidbody.AddForce (ForceApplying);
	}

}
