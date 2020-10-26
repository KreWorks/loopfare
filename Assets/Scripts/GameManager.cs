using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	void Awake()
	{
		GameDatas.LoadData();
	}

	public void EndGame(int collectedCoins, string endString)
	{
		GameDatas.AddCollectedCoinsToBank(collectedCoins);
		GameDatas.EndGameMessage = endString;
		GameDatas.SaveData();

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
	}

	private void OnApplicationQuit()
	{
		GameDatas.SaveData();
	}
}
