using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurningLeftState : CarState
{
	float laneShiftingSpeed;

	public TurningLeftState(CarController carController, float laneWidth, float laneTolerance, float laneShiftingSpeed) : base(carController, laneWidth, laneTolerance)
	{
		this.laneShiftingSpeed = laneShiftingSpeed;
	}

	public override void EnterState()
	{
		ShiftCarLane(true);
	}

	public override void Going(float carSpeed)
	{
		Vector3 speed = carController.transform.forward * carSpeed * GameDatas.GetCarSpeedMultiplier();
		speed -= carController.transform.right * laneShiftingSpeed;

		carController.SetCarVelocity(speed);

		float goalLanePos = GetLanePosition();

		if (Mathf.Abs(carController.transform.position.x - goalLanePos) < laneTolerance)
		{
			carController.TransitionState(carController.straightState);
		}
	}
}
