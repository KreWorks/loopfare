using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FareColor
{
	BLUE, 
	RED,
	GREEN
}

public class LevelGenerator : MonoBehaviour
{
	public GameObject levelPartPrefab;
	public GameObject coinPrefab;
	public GameObject obstaclePrefab;
	public List<GameObject> fares;
	public GameObject fareTarget;

	public Material blueFareMaterial;
	public Material redFareMaterial;
	public Material greenFareMaterial;

	public int startPartCount;
	public float tileSize = 3.0f;

	public float scale = 2.0f;

	int levelPartCount;
	int xPosition;
	int fareDistance;

	bool hasBlueFare;
	bool hasRedFare;
	bool hasGreenFare;

	// Start is called before the first frame update
	void Start()
	{
		levelPartCount = 0;
		xPosition = 0;

		fareDistance = GameDatas.GetFareDistance();
		hasBlueFare = false;
		hasRedFare = false;
		hasGreenFare = false;

		DestroyerManager dm = FindObjectOfType<DestroyerManager>();
		dm.AddListenerOnGroundHitEvent(SpawnLevelPart);

		for (int i = 0; i < startPartCount; i++)
		{
			SpawnLevelPart();
		}
	}

	void SpawnLevelPart()
	{
		Vector3 position = new Vector3(-1.5f * tileSize, 0, levelPartCount * tileSize);
		Instantiate(levelPartPrefab, position, Quaternion.identity, this.transform);

		SpawnItemOnGround();

		if (GameDatas.HasAbility(AbilityType.FARE_BLUE))
		{
			SpawnFare(position);
		}

		levelPartCount++;
	}

	void SpawnItemOnGround()
	{
		float perlinValue = Mathf.PerlinNoise(Time.time, 0.1f);

		if (levelPartCount % 4 == 0)
		{
			int xOffset = Random.Range(-1, 2);
			xPosition = xPosition + xOffset;
			xPosition = xPosition < -1 ? -1 : (xPosition > 1 ? 1 : xPosition);
		}

		Vector3 position = new Vector3(xPosition * tileSize, 0, tileSize * levelPartCount);

		if (perlinValue < 0.5f && levelPartCount % 3 != 0)
		{
			position.y = coinPrefab.transform.position.y;
			Instantiate(coinPrefab, position, Quaternion.identity, this.transform);
		}
		else if (perlinValue > 0.45f && levelPartCount % 3 == 0)
		{
			position.y = obstaclePrefab.transform.position.y;
			Instantiate(obstaclePrefab, position, Quaternion.identity, this.transform);
		}
	}

	private Vector3 SpawnFare(Vector3 position)
	{
		if (levelPartCount % fareDistance == 0 && levelPartCount != 0)
		{
			xPosition = ((xPosition + 2) % 3) - 1;
			position = new Vector3(xPosition * tileSize, 0, tileSize * levelPartCount);

			GameObject fare = Instantiate(fares[0], position, Quaternion.identity, this.transform);
			fare.GetComponent<MeshRenderer>().sharedMaterial = blueFareMaterial;

		}

		return position;
	}

	FareColor GetRandomColor()
	{
		int endRange = GameDatas.HasAbility(AbilityType.FARE_GREEN) ? 3 : GameDatas.HasAbility(AbilityType.FARE_RED) ? 2 : 1; 
		int randomInt = Random.Range(0, endRange);

		switch (randomInt)
		{
			case 0:
				return FareColor.BLUE;
			case 1:
				return FareColor.RED;
			case 2:
				return FareColor.GREEN;
			default:
				return FareColor.BLUE;
		}
	}
}
