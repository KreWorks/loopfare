using UnityEngine;
using System;

public class CarController : MonoBehaviour
{
	public float carSpeed;
	public float laneShiftingSpeed;
	public float laneWitdh = 1.0f;
	public float laneTolerance = 0.01f;
	public float explosionDuration;
	public GameObject explosionEffect;
	public GameObject smokeEffect;

	Action<FareColor> OnPickUpFare;
	Action<FareColor> OnTransferFare;

	Rigidbody rb;

	GameObject exposionEffectObject;
	GameObject smokeEffectObject;
	GameManager gameManager;

	public CarLanePosition lanePosition;

	public CarState actualState;
	public StraightGoingState straightState;
	public TurningLeftState leftState;
	public TurningRightState rightState;
	public ExplosionState explosionState;

    // Start is called before the first frame update
    void Start()
	{
		rb = GetComponentInChildren<Rigidbody>();

		straightState = new StraightGoingState(this, laneWitdh, laneTolerance);
		leftState = new TurningLeftState(this, laneWitdh, laneTolerance, laneShiftingSpeed);
		rightState = new TurningRightState(this, laneWitdh, laneTolerance, laneShiftingSpeed);
		explosionState = new ExplosionState(this, explosionDuration);

		actualState = straightState;
		lanePosition = CarLanePosition.MIDDLE;

		gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
		actualState.Going(carSpeed);

		if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
		{
			TransitionState(leftState);
		}

		if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
		{
			TransitionState(rightState);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Obstacle")
		{
			TransitionState(explosionState);
		}
		else if (other.tag == "Fare")
		{
			FareColor color = other.GetComponent<ColorController>().GetFareColor();
			OnPickUpFare?.Invoke(color);

			Destroy(other.gameObject);
		}
		else if(other.tag == "FareTarget")
		{
			FareColor color = other.GetComponent<ColorController>().GetFareColor();
			OnTransferFare?.Invoke(color);

			Destroy(other.gameObject);
		}
	}

	public void SpawnExplosion()
	{
		DisableCar();

		exposionEffectObject = Instantiate(explosionEffect, this.transform.position, explosionEffect.transform.rotation);
		exposionEffectObject.GetComponent<ParticleSystem>().Play();

		smokeEffectObject = Instantiate(smokeEffect, this.transform.position, smokeEffect.transform.rotation);
		smokeEffectObject.GetComponent<ParticleSystem>().Play();
	}

	void DisableCar()
	{
		MeshRenderer[] meshes = GetComponentsInChildren<MeshRenderer>();

		foreach (MeshRenderer mesh in meshes)
		{
			mesh.enabled = false;
		}
	}

	public void EndCar()
	{
		gameManager.EndGame(GameDatas.CollectedCoins, "You hit an obstacle");
	}

	public void SetCarVelocity(Vector3 carVelocity)
	{
		rb.velocity = carVelocity;
	}

	public void TransitionState(CarState newState)
	{
		actualState.ExitState();
		actualState = newState;
		actualState.EnterState();
	}

	public void AddListenerOnPickUpFareEvent(Action<FareColor> listener)
	{
		OnPickUpFare += listener;
	}
	public void RemoveListenerOnPickUpFareEvent(Action<FareColor> listener)
	{
		OnPickUpFare -= listener;
	}

	public void AddListenerOnTransferFareEvent(Action<FareColor> listener)
	{
		OnTransferFare += listener;
	}
	public void RemoveListenerOnTransferFareEvent(Action<FareColor> listener)
	{
		OnTransferFare -= listener;
	}
}
