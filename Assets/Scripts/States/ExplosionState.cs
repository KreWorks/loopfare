using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionState : CarState
{
	float explosionTimer;
	float explodingTime;

	public ExplosionState(CarController carController, float explodingTime) : base(carController, 0, 0)
	{
		this.explodingTime = explodingTime;
	}

	public override void EnterState()
	{
		base.EnterState();
		explosionTimer = 0.0f;

		carController.SpawnExplosion();
	}
	public override void TurnLeft()
	{
		return;
	}

	public override void TurnRight()
	{
		return;
	}

	public override void Going(float carSpeed)
	{
		carController.SetCarVelocity(Vector3.zero);
		explosionTimer += Time.deltaTime;
		Debug.Log("Exploding state " + explosionTimer);
		if (explosionTimer >= explodingTime)
		{
			carController.EndCar();
		}
	}
}
