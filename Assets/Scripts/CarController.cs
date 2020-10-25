using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
	public float carSpeed;
	public float laneWitdh = 1.0f;
	public float laneTolerance = 0.01f;
	public float explosionDuration;
	public GameObject explosionEffect;

	Rigidbody rb;
	bool isExploded;
	float explosionTime;
	GameObject exposionEffectObject;
	GameManager gameManager;

    // Start is called before the first frame update
    void Start()
	{
		rb = GetComponent<Rigidbody>();
		isExploded = false;
		explosionTime = 0.0f;

		rb.velocity = this.transform.forward * GetModifiedCarSpeed();

		gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
		if (!isExploded)
		{
			rb.velocity = this.transform.forward * carSpeed;

			if ((this.transform.position.x - laneTolerance) > -laneWitdh && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)))
			{
				this.transform.position += laneWitdh * Vector3.left;
			}

			if ((this.transform.position.x + laneTolerance) < laneWitdh && (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)))
			{
				this.transform.position -= laneWitdh * Vector3.left;
			}
		}
		else
		{
			rb.velocity = Vector3.zero;

			explosionTime += Time.deltaTime;
			if(explosionDuration <= explosionTime)
			{
				Destroy(this.gameObject);
				Destroy(exposionEffectObject);
			}
		}

	}

	float GetModifiedCarSpeed()
	{
		if (GameDatas.HasAbility(AbilityType.SPEED_175))
		{
			return carSpeed * 1.75f;
		}
		else if (GameDatas.HasAbility(AbilityType.SPEED_150))
		{
			return carSpeed * 1.5f;
		}
		else if(GameDatas.HasAbility(AbilityType.SPEED_125))
		{
			return carSpeed * 1.25f;
		}
		else
		{
			return carSpeed;
		}
	}

	private void OnTriggerEnter(Collider other)
	{

		if(other.tag == "Obstacle")
		{
			isExploded = true;
			explosionTime = 0.0f;

			exposionEffectObject = Instantiate(explosionEffect, this.transform.position, explosionEffect.transform.rotation);
			exposionEffectObject.GetComponent<ParticleSystem>().Play();

			this.gameObject.SetActive(false);
			gameManager.EndGame(10, "You hit an obstacle");
		}
		else if (other.tag == "Fare")
		{
			Destroy(other.gameObject);
			Debug.Log("You picked up a fare");
		}
		else if(other.tag == "FareTarget")
		{
			Destroy(other.gameObject);
			Debug.Log("You liftexd the fare");
		}
	}
}
