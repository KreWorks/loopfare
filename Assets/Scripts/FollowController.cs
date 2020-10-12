using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowController : MonoBehaviour
{
	public GameObject objectToFollow;
	public Vector3 offsetVector;

    // Start is called before the first frame update
    void Start()
    {
		this.transform.position = objectToFollow.transform.position + offsetVector;
    }

	// Update is called once per frame
	void LateUpdate()
	{
		this.transform.position = objectToFollow.transform.position + offsetVector;

		if (this.GetComponent<Camera>() != null)
		{
			this.transform.LookAt(objectToFollow.transform.position);
		}
	}
}
