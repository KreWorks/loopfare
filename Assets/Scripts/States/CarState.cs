using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CarLanePosition
{
	LEFT,
	MIDDLE, 
	RIGHT
}

public abstract class CarState
{
	protected CarController carController;
	protected float laneWidth;
	protected float laneTolerance;

	public CarState(CarController carController, float laneWidth, float laneTolerance)
	{
		this.carController = carController;
		this.laneWidth = laneWidth;
		this.laneTolerance = laneTolerance;
	}

	public virtual void EnterState() { }
	public virtual void ExitState() { }


	public virtual void TurnRight()
	{
		if (carController.lanePosition != CarLanePosition.RIGHT)
		{
			carController.TransitionState(carController.rightState);
		}
	}
	public virtual void TurnLeft()
	{
		if (carController.lanePosition != CarLanePosition.LEFT)
		{
			carController.TransitionState(carController.leftState);
		}
	}

	public virtual void ShiftCarLane(bool isLeft)
	{
		if (carController.lanePosition == CarLanePosition.MIDDLE && !isLeft)
		{
			carController.lanePosition = CarLanePosition.RIGHT;
		}
		else if(carController.lanePosition == CarLanePosition.MIDDLE && isLeft)
		{
			carController.lanePosition = CarLanePosition.LEFT;
		}
		else if((carController.lanePosition == CarLanePosition.LEFT && !isLeft) || (carController.lanePosition == CarLanePosition.RIGHT && isLeft))
		{
			carController.lanePosition = CarLanePosition.MIDDLE;
		}
	}

	public virtual float GetLanePosition()
	{
		switch (carController.lanePosition)
		{
			case CarLanePosition.LEFT:
				return -laneWidth;
			case CarLanePosition.MIDDLE:
				return 0.0f;
			case CarLanePosition.RIGHT:
				return laneWidth;
			default:
				return 0.0f;
		}
	}

	public abstract void Going(float carSpeed);
}
