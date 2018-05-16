using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreExtractor : MonoBehaviour {

	//Drop ID is the id of the ore that it is sitting on, 0 is nothing, 1 is debug
	public int DropID = 0;
	public GameObject DropPosition, DropObject;
	public double DropRate;
	private double DropTimeNext;

	// Use this for initialization
	void Start () {
		//Record time to start dropping ore
		DropTimeNext = Time.timeSinceLevelLoad;
	}
	
	// Update is called once per frame
	void Update () {
		//Start dropping ore if drop id is not 0
		if (DropTimeNext < Time.timeSinceLevelLoad && DropID != 0) {
			Instantiate (DropObject, DropPosition.transform);
			DropTimeNext = (1 / DropRate) + Time.timeSinceLevelLoad;
		}
	}
}
