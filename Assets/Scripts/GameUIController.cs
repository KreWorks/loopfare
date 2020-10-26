using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUIController : MonoBehaviour
{
	public TMP_Text coinText;

	public GameObject car;
	public Image blueFare;
	public Image redFare;
	public Image greenFare;

	public Sprite grayFareSprite;
	public Sprite blueFareSprite;
	public Sprite redFareSprite;
	public Sprite greenFareSprite;

	int stash;

	CoinCollector coinCollector;
	CarController carController;

    // Start is called before the first frame update
    void Start()
    {
		coinCollector = FindObjectOfType<CoinCollector>();
		coinCollector.AddListenerOnCoinCollectedEvent(ChangeCoinText);

		carController = FindObjectOfType<CarController>();
		carController.AddListenerOnPickUpFareEvent(ChangeFareImageColor);
		carController.AddListenerOnTransferFareEvent(ChangeFareImageColor);

		stash = GameDatas.GetStash();
		ChangeCoinText(0);

		SetFareImages();
	}

	void ChangeCoinText(int coins)
	{
		coinText.text = coins.ToString() + "/" + stash.ToString();
	}

	void SetFareImages()
	{
		blueFare.gameObject.SetActive(false);
		redFare.gameObject.SetActive(false);
		greenFare.gameObject.SetActive(false);

		if (GameDatas.HasAbility(AbilityType.FARE_BLUE))
		{
			blueFare.gameObject.SetActive(true);
			blueFare.sprite = grayFareSprite;
		}
		if (GameDatas.HasAbility(AbilityType.FARE_RED))
		{
			redFare.gameObject.SetActive(true);
			redFare.sprite = grayFareSprite;
		}
		if (GameDatas.HasAbility(AbilityType.FARE_GREEN))
		{
			greenFare.gameObject.SetActive(true);
			greenFare.sprite = grayFareSprite;
		}
	}

	void ChangeFareImageColor(FareColor color)
	{
		Sprite change = grayFareSprite;
		switch (color)
		{
			case FareColor.BLUE:
				change = blueFare.sprite == grayFareSprite ? blueFareSprite : grayFareSprite;
				blueFare.sprite = change;
				break;
			case FareColor.RED:
				change = redFare.sprite == grayFareSprite ? redFareSprite : grayFareSprite;
				redFare.sprite = change;
				break;
			case FareColor.GREEN:
				change = greenFare.sprite == grayFareSprite ? greenFareSprite : grayFareSprite;
				greenFare.sprite = change;
				break;
		}
	}
}
