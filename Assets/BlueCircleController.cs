using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueCircleController : MonoBehaviour {


	public GameObject Circle1, Circle2, Circle3, Circle4, Circle5, Circle6;

	public float Power = 0, Levels = 6, MaxPower = 40,  SpinConstant = 71, DepressionConstant = 100;
	private float Spin = 0, Depression = 0, UnlockDepression = .15f, MaxSpin = 315, MaxDepression = 1;

	private float RotationDifference = 0, Circle1Rot = 0, Circle2Rot = 0, DifferenceScalar = 7, SpinAddition = 0;

	private float[] TargetAngles = {0, 45, 90, 135, 180, 225, 270, 315, 360};

	private float TargetAngle = 360, TargetAngleLowest = 0, CorrespondingTargetAngle = 0;

	public bool Shoot = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Power < 0)
			Power = 0;
		else if (Power > MaxPower)
			Power = MaxPower;

		Spin = (Power / MaxSpin) * SpinConstant;
		Depression = (Power / (MaxDepression*DepressionConstant));

		//if (Depression * 2.5f > MaxDepression)
		//	Depression = MaxDepression / 2.5f;
		//else if (Depression <0 && Shoot == false)
		//	Depression = 0;

		//if (Spin * 32 > MaxSpin)
		//	Spin = MaxSpin / 32;
		//else if (Spin * 32 < -MaxSpin)
		//	Spin = -MaxSpin / 32;

		Circle1.transform.localPosition = new Vector3 (0, 0, Depression*2.5f);
		Circle2.transform.localPosition = new Vector3 (0, 0, Depression*2.0f);
		Circle3.transform.localPosition = new Vector3 (0, 0, Depression*1.5f);
		Circle4.transform.localPosition = new Vector3 (0, 0, Depression*1.25f);
		Circle5.transform.localPosition = new Vector3 (0, 0, Depression*1.1f);
		Circle6.transform.localPosition = new Vector3 (0, 0, Depression);

		if (Depression < UnlockDepression) {
			Circle1Rot = Circle1.transform.localEulerAngles.z > 180 ? Circle1.transform.localEulerAngles.z - 360f : Circle1.transform.localEulerAngles.z;
			Circle2Rot = Circle2.transform.localEulerAngles.z > 180 ? Circle2.transform.localEulerAngles.z - 360f : Circle2.transform.localEulerAngles.z;
			RotationDifference = Circle1Rot - Circle2Rot > 0 ? Circle1Rot - Circle2Rot : Circle1Rot - Circle2Rot + 360;
			RotationDifference = RotationDifference == 360 ? 0 : RotationDifference;

			if (TargetAngleLowest == 360) {
				for (int i = 0; i < TargetAngles.Length; i++) {
					if (TargetAngle > Mathf.Abs (TargetAngles [i] - RotationDifference)) {
						if (TargetAngleLowest > Mathf.Abs (TargetAngles [i] - RotationDifference)) {
							TargetAngleLowest = Mathf.Abs (TargetAngles [i] - RotationDifference);
							CorrespondingTargetAngle = TargetAngles [i];
						}
					}
				}
			}

			if (RotationDifference < CorrespondingTargetAngle)
				SpinAddition = 1 * DifferenceScalar * Mathf.Abs(RotationDifference-CorrespondingTargetAngle);
			else if (RotationDifference > CorrespondingTargetAngle)
				SpinAddition = -1 * DifferenceScalar * Mathf.Abs(RotationDifference-CorrespondingTargetAngle);
			else
				SpinAddition = 0;

			Circle1.transform.Rotate (0, 0, (Spin * 16 + SpinAddition) * Time.deltaTime);
			Circle2.transform.Rotate (0, 0, (Spin * 16) * Time.deltaTime);
		} else {
			Circle1.transform.Rotate (0, 0, Spin * 32 * Time.deltaTime);
			Circle2.transform.Rotate (0, 0, Spin * 16 * Time.deltaTime);
			TargetAngle = 50;
			TargetAngleLowest = 360;
		}
		Circle3.transform.Rotate (0, 0, Spin * 8 * Time.deltaTime);
		Circle4.transform.Rotate (0, 0, Spin * 4 * Time.deltaTime);
		Circle5.transform.Rotate (0, 0, Spin * 2 * Time.deltaTime);
		Circle6.transform.Rotate (0, 0, Spin * Time.deltaTime);
	}
}
