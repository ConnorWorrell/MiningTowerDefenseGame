using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun1Controller : MonoBehaviour {

	public float StoredPower = 0, PowerNeededToFire = 40, MaxChargeRate = 10, PowerLoss = 1;
	private float StoredPowerLast = 0;

	public bool Fire = false;

	public Vector3 Target;

	public GameObject AnimationPrefab, ShellPrefab, Shell, AnimationSpawnPoint, Animation, ShotSpawnPoint;

	public string NameOfAnimationController;

	// Use this for initialization
	void Start () {
		if (AnimationPrefab == null)
			Debug.LogWarning ("No Animation attached to Gun1");
		else {
			Animation = Instantiate (AnimationPrefab, AnimationSpawnPoint.transform);
		}
		
		if (Shell == null)
			Debug.LogWarning ("No Shell attached to Gun1");
	}
	
	// Update is called once per frame
	void Update () {
		if(Animation == null){
			Animation = Instantiate (AnimationPrefab, AnimationSpawnPoint.transform);
			Fire = false;
			StoredPower = StoredPower - PowerNeededToFire;
			Shell = Instantiate (ShellPrefab, ShotSpawnPoint.transform);
			Shell.GetComponent<Shell1Controller> ().Target = Target;
			Shell.transform.parent = null;
		}

		if (StoredPower >= PowerLoss * StoredPower * Time.deltaTime && StoredPower <= StoredPowerLast) {
			StoredPower = StoredPower - PowerLoss * StoredPower * Time.deltaTime;
			StoredPower = StoredPower < 1 ? 0 : StoredPower;
		}
		StoredPowerLast = StoredPower;

		Animation.GetComponent<BlueCircleController> ().Power = StoredPower;
		Animation.GetComponent<BlueCircleController> ().Levels = StoredPower == 0 ? 0 : 6;

		if (Fire == true && StoredPower >= PowerNeededToFire) {
			Animation.GetComponent<BlueCircleController> ().Shoot = Fire;
		}
	}
}
