using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class OreTextureRamdom : MonoBehaviour {

	public int Count = 4;
	public GameObject DisplayObject;

	void GenerateOre (){
		GameObject Obj = Instantiate (DisplayObject);
		Obj.transform.parent = gameObject.transform;
		Obj.transform.localPosition = new Vector3 (Random.value, Random.value*0.05f, Random.value) - new Vector3(0.5f,0,0.5f);
		Obj.transform.localRotation = Quaternion.Euler (Random.value * 360, Random.value * 360, Random.value * 360);
	}

	// Use this for initialization
	void Start () {
		for (int i = 1; i <= (Count*(transform.localScale.x + transform.localScale.y + transform.localScale.z)*(transform.localScale.x + transform.localScale.y + transform.localScale.z))/9; i++) {
			GenerateOre ();
		}
	}
}
