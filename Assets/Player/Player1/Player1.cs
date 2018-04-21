using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour {

	//Game Objects that are needed for the player to function
	public GameObject PlayerOverviewerPrefab, SpawnPoint;
	public GameObject GunPrefab, Gun;

	//PowerSupplied is ammount of power put into circle, power supplied multiplier changes rate of power supply
	public float PowerStorage = 0, PowerTransferRate = 10, PowerRechargeRate = 0;
	private float PowerTransferRateToGun = 0;

	//LeftClick for to add power, right click to subtract power
	public bool Shooting = false, Charging = false;

	// Use this for initialization
	void Start () {
		//Add PlayerOverviewer to the Scene if it is not alreay in the scene
		if (GameObject.Find ("PlayerOverviewer") == null) {
			GameObject go = Instantiate (PlayerOverviewerPrefab, new Vector3 (0, 0, 0), Quaternion.identity);
			go.name = go.name.Replace ("(Clone)", "");
		}

		SpawnGunAtPosition ();

	}
	
	// Update is called once per frame
	void Update () {

		PowerStorage = PowerStorage + PowerRechargeRate * Time.deltaTime;

		//GetShooting and Unshooting from player overviewer
		Shooting = GameObject.Find ("PlayerOverviewer").GetComponent<PlayerOverviewer> ().LeftClick;
		Charging = GameObject.Find ("PlayerOverviewer").GetComponent<PlayerOverviewer> ().RightClick;

		Gun.GetComponent<Gun1Controller> ().Fire = Shooting;
		if (Charging && PowerStorage > PowerTransferRateToGun * Time.deltaTime) {
			Gun.GetComponent<Gun1Controller> ().StoredPower = Gun.GetComponent<Gun1Controller> ().StoredPower + PowerTransferRateToGun * Time.deltaTime;
			PowerStorage = PowerStorage - PowerTransferRateToGun * Time.deltaTime;
		}


	}

	void SpawnGunAtPosition (){
		if (GunPrefab != null && Gun == null) {
			Gun = Instantiate (GunPrefab, SpawnPoint.transform);
		} else if (GunPrefab != null && Gun != null) {
			Destroy (Gun);
			Gun = Instantiate (GunPrefab, SpawnPoint.transform);
		}

		PowerTransferRateToGun = Gun.GetComponent<Gun1Controller> ().MaxChargeRate < PowerTransferRate ? Gun.GetComponent<Gun1Controller> ().MaxChargeRate : PowerTransferRate;

	}
}
