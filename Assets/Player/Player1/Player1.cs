using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour {

	public Transform Trackview;

	public int ObjectHoldingId = 0;
	private int ObjectHoldingIdLast = 0;

	public GameObject Holding, Camera;

	//Game Objects that are needed for the player to function
	public GameObject PlayerOverviewerPrefab, SpawnPoint, ConveyorHoldPosition;
	public GameObject HeldObject, ConveyorStraightBasicPrefab;

	//PowerSupplied is ammount of power put into circle, power supplied multiplier changes rate of power supply
	public float PowerStorage = 0, PowerTransferRate = 10, PowerRechargeRate = 0;
	private float PowerTransferRateToGun = 0;

	//LeftClick for to add power, right click to subtract power
	public bool Shooting = false, Charging = false;
	private bool LeftClickLast = false;

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
		if (Holding != null){
			if(Holding.GetComponent<ID> () != null)
				ObjectHoldingId = Holding.GetComponent<ID> ().ObjID;
			else
				Debug.LogError("Player Holding Object Without ID Number");
		} else
			ObjectHoldingId = 0;

		if (ObjectHoldingId != ObjectHoldingIdLast) {
			Destroy (HeldObject);
		}
		switch (ObjectHoldingId) {
			case 1: //Gun1
				Gun1 ();
				break;
			case 2: //Conveyor Straight Basic PrePlace
				ConveyorStraightBasicPrePlace ();
				break;
			default://Nothing or unknown
				break;
		}
		ObjectHoldingIdLast = ObjectHoldingId;
	}

	void ConveyorStraightBasicPrePlace() {
		//if object is not being held the make it held
		if(HeldObject == null)
			HeldObject = Instantiate (Holding, ConveyorHoldPosition.transform);

		//Find where player is looking
		RaycastHit hit;
		Debug.DrawRay (Camera.transform.position, Camera.transform.forward);
		Physics.Raycast (Camera.transform.position, Camera.transform.forward, out hit, 5);

		//If nothing that is snapable was hit then don't snap to anything
		if (hit.collider == null || hit.collider.gameObject.GetComponentInParent<ConveyorDirection> () == null) {
			HeldObject.transform.localPosition = new Vector3 (0, 0, 0);
			HeldObject.transform.localRotation = Quaternion.Euler(new Vector3 (0, 0, 0));
		} else {
			//If something snapable is found then snap to it
			Trackview = hit.collider.gameObject.GetComponentInParent<ConveyorDirection> ().gameObject.transform;

			Debug.Log ((Mathf.Abs (Vector3.Dot (Camera.transform.forward, Trackview.right)) > Mathf.Abs (Vector3.Dot (Camera.transform.forward, Trackview.forward)) && Vector3.Dot (Camera.transform.forward, Trackview.forward) < 0 && Vector3.Dot (Camera.transform.forward, Trackview.right) > 0));

			//Warning Apply Sunscreeen and Consume medication before continuing following code may cause cancer
			//Code for snapping an object to another object, has issues between angles of 108 and 45 degrees
			HeldObject.transform.position = hit.collider.gameObject.GetComponentInParent<ConveyorDirection> ().gameObject.transform.position
			//Code is as follows: Correcting for the (Det1 < Det2), and Det1 is negative, (Det1 > Det2), and Det2 is negative, and Both Det1 and Det2 are positive, lead to the snapping happening on the wrong side, --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------\/ is where we begin calculating the position that the snapping happens, wether the camera forward is more in the direction of trackview right or left, then the respective track view is applied or is 0
			+ (((Mathf.Abs (Vector3.Dot (Camera.transform.forward, Trackview.right)) < Mathf.Abs (Vector3.Dot (Camera.transform.forward, Trackview.forward)) && Vector3.Dot (Camera.transform.forward, Trackview.right) < 0 && Vector3.Dot (Camera.transform.forward, Trackview.forward) > 0) || (Mathf.Abs (Vector3.Dot (Camera.transform.forward, Trackview.right)) > Mathf.Abs (Vector3.Dot (Camera.transform.forward, Trackview.forward)) && Vector3.Dot (Camera.transform.forward, Trackview.forward) < 0 && Vector3.Dot (Camera.transform.forward, Trackview.right) > 0) || (Vector3.Dot (Camera.transform.forward, Trackview.right) > 0 && Vector3.Dot (Camera.transform.forward, Trackview.forward) > 0)) ? -1 : 1) * (Mathf.Abs (Vector3.Dot (Camera.transform.forward, Trackview.right)) > Mathf.Abs (Vector3.Dot (Camera.transform.forward, Trackview.forward)) && Mathf.Abs (Vector3.Dot (Camera.transform.forward, Trackview.right)) > Mathf.Abs (Vector3.Dot (Camera.transform.forward, Trackview.up)) ? Trackview.right : new Vector3 (0, 0, 0)) +
			(((Mathf.Abs (Vector3.Dot (Camera.transform.forward, Trackview.right)) < Mathf.Abs (Vector3.Dot (Camera.transform.forward, Trackview.forward)) && Vector3.Dot (Camera.transform.forward, Trackview.right) < 0 && Vector3.Dot (Camera.transform.forward, Trackview.forward) > 0) || (Mathf.Abs (Vector3.Dot (Camera.transform.forward, Trackview.right)) > Mathf.Abs (Vector3.Dot (Camera.transform.forward, Trackview.forward)) && Vector3.Dot (Camera.transform.forward, Trackview.forward) < 0 && Vector3.Dot (Camera.transform.forward, Trackview.right) > 0) || (Vector3.Dot (Camera.transform.forward, Trackview.right) > 0 && Vector3.Dot (Camera.transform.forward, Trackview.forward) > 0)) ? -1 : 1) * (Mathf.Abs (Vector3.Dot (Camera.transform.forward, Trackview.forward)) > Mathf.Abs (Vector3.Dot (Camera.transform.forward, Trackview.right)) && Mathf.Abs (Vector3.Dot (Camera.transform.forward, Trackview.forward)) > Mathf.Abs (Vector3.Dot (Camera.transform.forward, Trackview.up)) ? Trackview.forward : new Vector3 (0, 0, 0)) +
				(((Mathf.Abs (Vector3.Dot (Camera.transform.forward, Trackview.right)) > Mathf.Abs (Vector3.Dot (Camera.transform.forward, Trackview.forward)) && Vector3.Dot (Camera.transform.forward, Trackview.forward) < 0 && Vector3.Dot (Camera.transform.forward, Trackview.right) > 0) || ((Mathf.Abs (Vector3.Dot (Camera.transform.forward, Trackview.right)) > Mathf.Abs (Vector3.Dot (Camera.transform.forward, Trackview.forward)) && Vector3.Dot (Camera.transform.forward, Trackview.forward) < 0 && Vector3.Dot (Camera.transform.forward, Trackview.right) > 0))==false) ? 1 : -1) * (Vector3.Dot (Camera.transform.forward, Trackview.up) > 0 ? -1 : 1) * (Mathf.Abs (Vector3.Dot (Camera.transform.forward, Trackview.up)) > Mathf.Abs (Vector3.Dot (Camera.transform.forward, Trackview.right)) && Mathf.Abs (Vector3.Dot (Camera.transform.forward, Trackview.up)) > Mathf.Abs (Vector3.Dot (Camera.transform.forward, Trackview.forward)) ? Trackview.up : new Vector3 (0, 0, 0));
			HeldObject.transform.rotation = hit.collider.gameObject.GetComponentInParent<ConveyorDirection> ().gameObject.transform.rotation;
		}

		if (GameObject.Find ("PlayerOverviewer").GetComponent<PlayerOverviewer> ().LeftClick == true && LeftClickLast == false) {
			GameObject Temp = Instantiate (ConveyorStraightBasicPrefab, HeldObject.transform);
			Temp.transform.parent = null;
		}
		LeftClickLast = GameObject.Find ("PlayerOverviewer").GetComponent<PlayerOverviewer> ().LeftClick;
	}

	void Gun1 (){

		if (HeldObject == null)
			SpawnGunAtPosition ();

		PowerStorage = PowerStorage + PowerRechargeRate * Time.deltaTime;

		//GetShooting and Unshooting from player overviewer
		Shooting = GameObject.Find ("PlayerOverviewer").GetComponent<PlayerOverviewer> ().LeftClick;
		Charging = GameObject.Find ("PlayerOverviewer").GetComponent<PlayerOverviewer> ().RightClick;

		HeldObject.GetComponent<Gun1Controller> ().Fire = Shooting;
		if (Charging && PowerStorage > PowerTransferRateToGun * Time.deltaTime) {
			HeldObject.GetComponent<Gun1Controller> ().StoredPower = HeldObject.GetComponent<Gun1Controller> ().StoredPower + PowerTransferRateToGun * Time.deltaTime;
			PowerStorage = PowerStorage - PowerTransferRateToGun * Time.deltaTime;
		}
	}

	void SpawnGunAtPosition (){
		//If we should be hodling something, but we aren't then spawn it
		if (Holding != null && HeldObject == null) {
			HeldObject = Instantiate (Holding, SpawnPoint.transform);
		//If we are holding something that we shouldn't be holding then delete it
		} else if (Holding != null && HeldObject != null) {
			Destroy (HeldObject);
			HeldObject = Instantiate (Holding, SpawnPoint.transform);
		}

		//Grab power transfer rate from gun
		PowerTransferRateToGun = HeldObject.GetComponent<Gun1Controller> ().MaxChargeRate < PowerTransferRate ? HeldObject.GetComponent<Gun1Controller> ().MaxChargeRate : PowerTransferRate;

	}
}
