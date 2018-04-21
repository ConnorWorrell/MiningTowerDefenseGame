using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour {

	//Game Objects that are needed for the player to function
	public GameObject PlayerOverviewerPrefab, SpawnPoint, BlueCircle;
	public GameObject Circle;

	//PowerSupplied is ammount of power put into circle, power supplied multiplier changes rate of power supply
	public float PowerSupplied = 0, PowerSuppliedMultiplier = 20;

	//LeftClick for to add power, right click to subtract power
	public bool Shooting = false, UnShooting = false;

	// Use this for initialization
	void Start () {
		//Add PlayerOverviewer to the Scene if it is not alreay in the scene
		if (GameObject.Find ("PlayerOverviewer") == null) {
			GameObject go = Instantiate (PlayerOverviewerPrefab, new Vector3 (0, 0, 0), Quaternion.identity);
			go.name = go.name.Replace ("(Clone)", "");
		}
	}
	
	// Update is called once per frame
	void Update () {
		//GetShooting and Unshooting from player overviewer
		Shooting = GameObject.Find ("PlayerOverviewer").GetComponent<PlayerOverviewer> ().LeftClick;
		UnShooting = GameObject.Find ("PlayerOverviewer").GetComponent<PlayerOverviewer> ().RightClick;

		//If there is not a circle present no power is supplied
		if(Circle == null)
			PowerSupplied = 0;

		// If power is being removed from the circle, and circle has no power and not holding circle power, remove circle
		if (UnShooting && PowerSupplied == 0 && !Shooting)
			Destroy (Circle);

		//If adding or removing power from circle
		if (Shooting || UnShooting) {
			//if no circle make a circle
			if (Circle == null && Shooting)
				Circle = Instantiate (BlueCircle, SpawnPoint.transform);
			//Calculate power being supplied to the circle
			PowerSupplied = PowerSupplied + PowerSuppliedMultiplier * Time.deltaTime * ((Shooting ? 1 : 0) - (UnShooting ? 1 : 0));
			PowerSupplied = PowerSupplied < 0 ? 0 : PowerSupplied > 40 ? 40 : PowerSupplied;
			//Send power to circle if there is a circle
			if(Circle != null)
				Circle.GetComponent<BlueCircleController> ().Power = PowerSupplied;
		} else {
			//If Not fuleing or unfueling, and circle has power, shoot circle
			if (Circle != null && PowerSupplied > 0)
				Circle.GetComponent<BlueCircleController> ().Shoot = true;
			//If not fueling or unfueling and circle has no pwer, remove circle
			else if (PowerSupplied == 0)
				Destroy (Circle);
		}

	}
}
