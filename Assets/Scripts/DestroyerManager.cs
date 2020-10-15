using UnityEngine;
using System;

public class DestroyerManager : MonoBehaviour
{
	public Action OnGroundHit; 

	private void OnTriggerEnter(Collider other)
	{
		Destroy(other.gameObject);	
	}

	private void OnCollisionEnter(Collision collision)
	{
		Destroy(collision.gameObject);
		if (collision.gameObject.tag == "GroundPart")
		{
			OnGroundHit?.Invoke();
		}
	}

	public void AddListenerOnGroundHitEvent(Action listener)
	{
		OnGroundHit += listener;
	}
	public void RemoveListenerOnGroundHitEvent(Action listener)
	{
		OnGroundHit -= listener;
	}
}
