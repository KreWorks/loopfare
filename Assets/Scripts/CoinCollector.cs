using UnityEngine;
using System;

public class CoinCollector : MonoBehaviour
{
	int coinsCollected;
	int stash;

	Action<int> OnCoinCollected;

	private void Start()
	{
		coinsCollected = 0;
		stash = GameDatas.GetStash();

		OnCoinCollected?.Invoke(coinsCollected);
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Coin")
		{
			coinsCollected++;
			Destroy(other.gameObject);

			OnCoinCollected?.Invoke(coinsCollected);

			if(stash <= coinsCollected)
			{
				GameManager gameManager = FindObjectOfType<GameManager>();
				gameManager.EndGame(stash, "Your stash is full.");
			}
		}
	}

	public void AddListenerOnCoinCollectedEvent(Action<int> listener)
	{
		OnCoinCollected += listener;
	}
	public void RemoveListenerOnCoinCollectedEvent(Action<int> listener)
	{
		OnCoinCollected -= listener;
	}
}
