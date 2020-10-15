using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
	public GameObject levelPartPrefab;
	public GameObject coinPrefab;
	public GameObject obstaclePrefab;

	public int startPartCount;
	public float tileSize = 3.0f;

	public float scale = 2.0f;

	int levelPartCount;
    // Start is called before the first frame update
    void Start()
    {
		levelPartCount = 0;

		DestroyerManager dm = FindObjectOfType<DestroyerManager>();

		dm.AddListenerOnGroundHitEvent(SpawnLevelPart);

		for(int i = 0; i < startPartCount; i++)
		{
			SpawnLevelPart();
		}
    }


	void SpawnLevelPart()
	{
		Vector3 position = new Vector3(-1.5f * tileSize, 0, levelPartCount * tileSize);
		Instantiate(levelPartPrefab, position, Quaternion.identity, this.transform);

		SpawnItemOnGround();
		levelPartCount++;
	}

	void SpawnItemOnGround()
	{
		float sampleX = 1f / tileSize ;
		float sampleY = levelPartCount / tileSize;
		float randomNumber = Mathf.PerlinNoise(sampleX, sampleY);

		int randomXPosition = Random.Range(-1, 2);
		Vector3 position = new Vector3(randomXPosition * tileSize, 0, tileSize * levelPartCount);

		if (randomNumber > 0.7f)
		{
			position.y = obstaclePrefab.transform.position.y;
			Instantiate(obstaclePrefab, position, Quaternion.identity, this.transform);
		}
		else if (randomNumber < 0.4f)
		{
			position.y = coinPrefab.transform.position.y;
			Instantiate(coinPrefab, position, Quaternion.identity, this.transform);
		}
	}
}
