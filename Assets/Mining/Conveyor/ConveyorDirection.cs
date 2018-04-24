using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorDirection : MonoBehaviour {

	public Vector3 MovementDirection;
	public Vector3 InputMovementDirection;

	// Use this for initialization
	void Start () {
		//Turn Movement Direction to allign with imput movement Direction
		MovementDirection = InputMovementDirection.x * gameObject.transform.right + InputMovementDirection.y * gameObject.transform.right + InputMovementDirection.z * gameObject.transform.right;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
