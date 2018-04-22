using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveConveyor : MonoBehaviour {

	public float PushDistance = 0.25f, Drag = 1, RotationalDragMult = 1;

	public Vector3 ForceDirection;

	private Rigidbody rB;


	// Use this for initialization
	void Start () {
		rB = gameObject.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		RaycastHit hit;
		Debug.DrawRay (transform.position, Vector3.down*PushDistance);
		Physics.Raycast (transform.position, Vector3.down, out hit, PushDistance);

		if (hit.collider == null) {
			ForceDirection = new Vector3 (0, 0, 0);
		} else if (hit.collider.gameObject.GetComponentInParent<ConveyorDirection> () != null) {
			ForceDirection = hit.collider.gameObject.GetComponentInParent<ConveyorDirection> ().MovementDirection;
		} else {
			//Debug.Log (hit.collider.transform.parent.gameObject.name.Substring (0, 3));
			ForceDirection = new Vector3 (0, 0, 0);
			if(hit.collider.transform.parent == null || hit.collider.transform.parent.gameObject.name.Substring (0, 3) != "Ore")
				Destroy (gameObject);
		}

		Vector3 Velocity = rB.velocity;
		ForceDirection.Set (ForceDirection.x - Velocity.x/* * Velocity.x * (Velocity.x < 0 ? -1 : 1)*/ * Drag, ForceDirection.y/* - Velocity.y * Mathf.Abs(Velocity.y) * Drag*/, ForceDirection.z - Velocity.z * /*Velocity.z * (Velocity.z < 0 ? -1 : 1) * */Drag);

		rB.AddForceAtPosition (ForceDirection*Time.fixedTime, hit.point);



	}
}
