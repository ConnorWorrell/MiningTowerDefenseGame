using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueCircleController : MonoBehaviour {

	//Different Circles That will be rotated to make effect
	public GameObject Circle1, Circle2, Circle3, Circle4, Circle5, Circle6;

	//Power is main control, Levels is number of circles shown, MaxPower is maximum Power, Spin constant is a spin multiplier, Depression Constant is a depression multiplier
	public float Power = 0, Levels = 6, MaxPower = 40,  SpinConstant = 71, DepressionConstant = 100;
	//Spin is Current Spin Speed, Depression is current depression, unlock depression is the depression where circle1 and circle2 are no longer spinning at the same speed
	// Max spin is unused Max Depression is unused
	private float Spin = 0, Depression = 0, UnlockDepression = .15f, MaxSpin = 315, MaxDepression = 1;
	//Rotation difference is degree seperation between circle1 and circle2, Circle1Rot is Circle1's z rotation, Circle2Rot is Circle2's z rotation
	//DifferenceScalar is multiplier to spin that re alligns circles1 and circles2, SpinAddition is added spin speed to realign cicle1 and circle2
	private float RotationDifference = 0, Circle1Rot = 0, Circle2Rot = 0, DifferenceScalar = 7, SpinAddition = 0;
	//TargetAngles is the angles that circle1 can allign with circle2 at
	private float[] TargetAngles = {0, 45, 90, 135, 180, 225, 270, 315, 360};
	//TargetAngle should be unused, TargetAngleLowest is used to sellect angle circle1 and circle2 will align at, CorrespongingTargetAngle is target angle circle1 and circle2 will allign at
	private float TargetAngle = 360, TargetAngleLowest = 0, CorrespondingTargetAngle = 0;
	//Shoot plays shoot animation when true
	public bool Shoot = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//Limit Power between 0 and MaxPower
		if (Power < 0)
			Power = 0;
		else if (Power > MaxPower)
			Power = MaxPower;

		//Calculate Spin and depression
		Spin = (Power / MaxSpin) * SpinConstant;
		Depression = (Power / (MaxDepression*DepressionConstant));

		//Apply depression to circles
		Circle1.transform.localPosition = new Vector3 (0, 0, Depression*2.5f);
		Circle2.transform.localPosition = new Vector3 (0, 0, Depression*2.0f);
		Circle3.transform.localPosition = new Vector3 (0, 0, Depression*1.5f);
		Circle4.transform.localPosition = new Vector3 (0, 0, Depression*1.25f);
		Circle5.transform.localPosition = new Vector3 (0, 0, Depression*1.1f);
		Circle6.transform.localPosition = new Vector3 (0, 0, Depression);

		//Circle1 and Circle2 locked together
		if (Depression < UnlockDepression) {
			//Calculate Difference between circle1 and circle2's angles
			Circle1Rot = Circle1.transform.localEulerAngles.z > 180 ? Circle1.transform.localEulerAngles.z - 360f : Circle1.transform.localEulerAngles.z;
			Circle2Rot = Circle2.transform.localEulerAngles.z > 180 ? Circle2.transform.localEulerAngles.z - 360f : Circle2.transform.localEulerAngles.z;
			RotationDifference = Circle1Rot - Circle2Rot > 0 ? Circle1Rot - Circle2Rot : Circle1Rot - Circle2Rot + 360;
			RotationDifference = RotationDifference == 360 ? 0 : RotationDifference;

			//Pick angle that circle1 and circle2 will allign at
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

			//Calculate spin that will be added to circle1 to allign with circle2
			if (RotationDifference < CorrespondingTargetAngle)
				SpinAddition = 1 * DifferenceScalar * Mathf.Abs(RotationDifference-CorrespondingTargetAngle);
			else if (RotationDifference > CorrespondingTargetAngle)
				SpinAddition = -1 * DifferenceScalar * Mathf.Abs(RotationDifference-CorrespondingTargetAngle);
			else
				SpinAddition = 0;

			//Apply rotation to circle1 and circle2
			Circle1.transform.Rotate (0, 0, (Spin * 16 + SpinAddition) * Time.deltaTime);
			Circle2.transform.Rotate (0, 0, (Spin * 16) * Time.deltaTime);
		} else {
			//If no allignment requrired between circle 1 and circle 2 apply rotation to circle1 and circle2
			Circle1.transform.Rotate (0, 0, Spin * 32 * Time.deltaTime);
			Circle2.transform.Rotate (0, 0, Spin * 16 * Time.deltaTime);
			TargetAngle = 50;
			TargetAngleLowest = 360;
		}
		//Apply rotation to circles 3 through 6
		Circle3.transform.Rotate (0, 0, Spin * 8 * Time.deltaTime);
		Circle4.transform.Rotate (0, 0, Spin * 4 * Time.deltaTime);
		Circle5.transform.Rotate (0, 0, Spin * 2 * Time.deltaTime);
		Circle6.transform.Rotate (0, 0, Spin * Time.deltaTime);
	}
}
