using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class OreTextureRamdom : MonoBehaviour {

	//Count is the number of ore to spawn per 1^2 unit of ore ground
	public int Count = 4;
	public GameObject DisplayObject;

	//Function that puts one DisplayObject in the plane of itself with a random position and rotation
	void GenerateOre (){
		GameObject Obj = Instantiate (DisplayObject);
		Obj.transform.parent = gameObject.transform;
		Obj.transform.localPosition = new Vector3 (Random.value, Random.value*0.05f, Random.value) - new Vector3(0.5f,0,0.5f);
		Obj.transform.localRotation = Quaternion.Euler (Random.value * 360, Random.value * 360, Random.value * 360);
	}

	// Use this for initialization
	void Start () {
		//Generate the number of ore specified by count
		for (int i = 1; i <= (Count*(transform.localScale.x + transform.localScale.y + transform.localScale.z)*(transform.localScale.x + transform.localScale.y + transform.localScale.z))/9; i++) {
			GenerateOre ();
		}
	}
}
