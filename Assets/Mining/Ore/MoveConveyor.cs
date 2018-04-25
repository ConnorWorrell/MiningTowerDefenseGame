using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveConveyor : MonoBehaviour {

	public float RangeLow = 0f, RangeHigh = 1f;

	public float PushDistance = 0.25f, Drag = 0.25f, RotationalDragMult = 1, InitialDrag = 0, ForceLagTime = 0;
	public bool KillOnImpact = true;

	public Vector3 ForceDirection, ForceLast;

	private Rigidbody rB;


	// Use this for initialization
	void Start () {

		ForceLast = new Vector3 (0, 0, 0);

		//Record RidigdBody of object and the inital drag, ititial drag will be used for 
		//the player when we don't want to apply drag when not interacting with the conveyor belts
		rB = gameObject.GetComponent<Rigidbody> ();
		InitialDrag = Drag;

		if (ForceLagTime == 0)
			ForceLagTime = Random.Range (RangeLow, RangeHigh);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Figure out if object is sitting on something
		RaycastHit hit;
		Debug.DrawRay (transform.position, Vector3.down*PushDistance);
		Physics.Raycast (transform.position, Vector3.down, out hit, PushDistance);

		//If not sitting on something apply no force, turn off drag for player
		if (hit.collider == null) {
			ForceLast = new Vector3 (0, 0, 0);
			if (KillOnImpact == false) {
				Drag = 0;
			}
			ForceDirection = new Vector3 (0, 0, 0);

		//If the object is sitting on conveyor belt apply drag and get force direction of conveyor
		} else if (hit.collider.gameObject.GetComponentInParent<ConveyorDirection> () != null) {
			if(KillOnImpact == false)
				Drag = InitialDrag;
			ForceDirection = hit.collider.gameObject.GetComponentInParent<ConveyorDirection> ().MovementDirection;

		//If object is sitting on something that isn't a conveyor, kill if not player and apply no force
		} else {
			if (KillOnImpact == false) {
				Drag = 0;
				ForceLast = new Vector3 (0, 0, 0);
			}
			ForceDirection = new Vector3 (0, 0, 0);
			if(KillOnImpact == true && (hit.collider.transform.parent == null || hit.collider.transform.parent.gameObject.name.Substring (0, 3) != "Ore"))
				Destroy (gameObject);
		}

		//Apply velocity based drag on object
		Vector3 Velocity = rB.velocity;
		ForceDirection.Set (ForceDirection.x - Velocity.x * Drag, ForceDirection.y, ForceDirection.z - Velocity.z * Drag);

		ForceDirection = (ForceDirection + ForceLast * ForceLagTime * Time.fixedTime) / (1 + ForceLagTime * Time.fixedTime);

		//add force at the point of contact between the object and conveyor
		rB.AddForceAtPosition (ForceDirection*Time.fixedTime, hit.point);

		ForceLast = ForceDirection;



	}
}
