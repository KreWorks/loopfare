using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
	public float carSpeed;
	public float laneWitdh = 1.0f;
	public float laneTolerance = 0.01f;

	Rigidbody rb; 

    // Start is called before the first frame update
    void Start()
	{
		rb = GetComponent<Rigidbody>();

		rb.velocity = this.transform.forward * GetModifiedCarSpeed();
    }

    // Update is called once per frame
    void Update()
    {
		rb.velocity = this.transform.forward * carSpeed;
		
		if((this.transform.position.x -  laneTolerance) > -laneWitdh && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)))
		{
			this.transform.position += laneWitdh * Vector3.left;
		}

		if((this.transform.position.x + laneTolerance) < laneWitdh && (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)))
		{
			this.transform.position -= laneWitdh * Vector3.left;
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

			Debug.Log("You hit an obstacle");
		}
	}
}
