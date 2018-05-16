using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreExtractor : MonoBehaviour {

	public int DropID = 0;
	public GameObject DropPosition, DropObject;
	public double DropRate;
	private double DropTimeNext;

	// Use this for initialization
	void Start () {
		DropTimeNext = Time.timeSinceLevelLoad;
	}
	
	// Update is called once per frame
	void Update () {
		if (DropTimeNext < Time.timeSinceLevelLoad && DropID != 0) {
			Instantiate (DropObject, DropPosition.transform);
			DropTimeNext = (1 / DropRate) + Time.timeSinceLevelLoad;
		}
	}
}
