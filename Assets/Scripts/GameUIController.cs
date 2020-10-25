using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUIController : MonoBehaviour
{
	public TMP_Text coinText;
	int stash;

	CoinCollector coinCollector;

    // Start is called before the first frame update
    void Start()
    {
		coinCollector = FindObjectOfType<CoinCollector>();
		coinCollector.AddListenerOnCoinCollectedEvent(ChangeCoinText);

		stash = GameDatas.GetStash();
	}

	void ChangeCoinText(int coins)
	{
		coinText.text = coins.ToString() + "/" + stash.ToString();
	}
}
