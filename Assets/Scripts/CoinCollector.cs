using UnityEngine;
using TMPro;

public class CoinCollector : MonoBehaviour
{
	public TMP_Text coinText;

	int coinsCollected; 

	private void Start()
	{
		coinsCollected = 0;

		SetCoinText();
	}

	void SetCoinText()
	{
		coinText.text = coinsCollected.ToString() + "/20"; 
		//TODO Update to get the stash value
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Coin")
		{
			coinsCollected++;
			SetCoinText();
			Destroy(other.gameObject);
		}
	}
}
