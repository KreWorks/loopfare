using UnityEngine;
using System;

public class DestroyerManager : MonoBehaviour
{
	public Action OnGroundHit; 

	private void OnTriggerEnter(Collider other)
	{
		Destroy(other.gameObject);

		if(other.tag == "GroundPart")
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
