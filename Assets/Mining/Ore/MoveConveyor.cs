using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveConveyor : MonoBehaviour {

	public float PushDistance = 0.25f, Drag = 0.25f, RotationalDragMult = 1, InitialDrag = 0;
	public bool KillOnImpact = true;

	public Vector3 ForceDirection;

	private Rigidbody rB;


	// Use this for initialization
	void Start () {
		//Record RidigdBody of object and the inital drag, ititial drag will be used for 
		//the player when we don't want to apply drag when not interacting with the conveyor belts
		rB = gameObject.GetComponent<Rigidbody> ();
		InitialDrag = Drag;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Figure out if object is sitting on something
		RaycastHit hit;
		Debug.DrawRay (transform.position, Vector3.down*PushDistance);
		Physics.Raycast (transform.position, Vector3.down, out hit, PushDistance);

		//If not sitting on something apply no force, turn off drag for player
		if (hit.collider == null) {
			if(KillOnImpact == false)
				Drag = 0;
			ForceDirection = new Vector3 (0, 0, 0);

		//If the object is sitting on conveyor belt apply drag and get force direction of conveyor
		} else if (hit.collider.gameObject.GetComponentInParent<ConveyorDirection> () != null) {
			if(KillOnImpact == false)
				Drag = InitialDrag;
			ForceDirection = hit.collider.gameObject.GetComponentInParent<ConveyorDirection> ().MovementDirection;

		//If object is sitting on something that isn't a conveyor, kill if not player and apply no force
		} else {
			if(KillOnImpact == false)
				Drag = 0;
			ForceDirection = new Vector3 (0, 0, 0);
			if(KillOnImpact == true && (hit.collider.transform.parent == null || hit.collider.transform.parent.gameObject.name.Substring (0, 3) != "Ore"))
				Destroy (gameObject);
		}

		//Apply velocity based drag on object
		Vector3 Velocity = rB.velocity;
		ForceDirection.Set (ForceDirection.x - Velocity.x * Drag, ForceDirection.y, ForceDirection.z - Velocity.z * Drag);

		//add force at the point of contact between the object and conveyor
		rB.AddForceAtPosition (ForceDirection*Time.fixedTime, hit.point);



	}
}
