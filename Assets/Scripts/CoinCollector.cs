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

		FindObjectOfType<CarController>().AddListenerOnTransferFareEvent(AddFarePrice);
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Coin")
		{
			CollectCoin(1);

			Destroy(other.gameObject);

			if(stash <= coinsCollected)
			{
				GameManager gameManager = FindObjectOfType<GameManager>();
				gameManager.EndGame(stash, "Your stash is full.");
			}
		}
	}

	void AddFarePrice(FareColor color)
	{
		CollectCoin(GameDatas.GetFarePrice());
	}

	void CollectCoin(int coin)
	{
		coinsCollected += coin;
		GameDatas.CollectedCoins = coinsCollected;
		OnCoinCollected?.Invoke(coinsCollected);
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
