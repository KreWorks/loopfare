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

		rb.velocity = this.transform.forward * carSpeed;
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
}
