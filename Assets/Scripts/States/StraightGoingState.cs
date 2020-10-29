using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightGoingState : CarState
{

	public StraightGoingState(CarController carController, float laneWidth, float laneTolerance) : base(carController, laneWidth, laneTolerance)
	{
	}

	public override void Going(float carSpeed)
	{
		carController.SetCarVelocity(carController.transform.forward * carSpeed * GameDatas.GetCarSpeedMultiplier());
	}

}
