using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell1Controller : MonoBehaviour {

	public float Velocity = 1;

	public GameObject ExplosionAnimation;

	public Vector3 Target;
	private Vector3 InstianciatePosition, ChangePosition, NewPosition;

	private float Changex, Changey, Changez, Currentx, Currenty, Currentz;

	// Use this for initialization
	void Start () {
		InstianciatePosition = gameObject.transform.position;
		ChangePosition = new Vector3 (Target.x - InstianciatePosition.x, Target.y - InstianciatePosition.y, Target.z - InstianciatePosition.z);
		ChangePosition.Normalize ();
		NewPosition = InstianciatePosition;
	}
	
	// Update is called once per frame
	void Update () {
		NewPosition = NewPosition + ChangePosition * Velocity * Time.deltaTime;
		gameObject.transform.position = NewPosition;

		if (Target.x - InstianciatePosition.x > 0 && Target.x - NewPosition.x < 0 || Target.x - InstianciatePosition.x < 0 && Target.x - NewPosition.x > 0) {
			//SpawnAnimation
			//CauseDamage
			Destroy (gameObject);
		}
	}
}
