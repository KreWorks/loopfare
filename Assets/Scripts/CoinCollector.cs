using UnityEngine;
using TMPro;

public class CoinCollector : MonoBehaviour
{
	public TMP_Text coinText;

	int coinsCollected;
	int stash;

	private void Start()
	{
		coinsCollected = 0;

		SetStash();
		SetCoinText();
	}

	void SetCoinText()
	{
		coinText.text = coinsCollected.ToString() + "/" + stash.ToString(); 
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Coin")
		{
			coinsCollected++;
			SetCoinText();
			Destroy(other.gameObject);

			if(stash <= coinsCollected)
			{
				GameManager gameManager = FindObjectOfType<GameManager>();
				gameManager.EndGame(stash);
			}
		}
	}
	
	void SetStash()
	{
		if (GameDatas.HasAbility(AbilityType.STASH_100))
		{
			stash = 100;
		}
		else if (GameDatas.HasAbility(AbilityType.STASH_50))
		{
			stash = 50;
		}
		else if (GameDatas.HasAbility(AbilityType.STASH_30))
		{
			stash = 30;
		}
		else
		{
			stash = 20;
		}
	}
}
