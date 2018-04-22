using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreDropperTest : MonoBehaviour {

	public GameObject Ore;

	public double DropRate = 0.5;
	private double PlannedDropTime;

	// Use this for initialization
	void Start () {
		PlannedDropTime = Time.timeSinceLevelLoad;
	}
	
	// Update is called once per frame
	void Update () {
		if (PlannedDropTime < Time.timeSinceLevelLoad) {
			Instantiate (Ore, gameObject.transform);
			PlannedDropTime = (1 / DropRate) + Time.timeSinceLevelLoad;
		}
	}
}
