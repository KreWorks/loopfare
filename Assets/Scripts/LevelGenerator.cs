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

		if(levelPartCount >= 8)
		{
			SpawnItemOnGround();
		}

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

	void SpawnFare(Vector3 position)
	{
		if (levelPartCount % fareDistance == 0 && levelPartCount != 0)
		{
			xPosition = ((xPosition + 2) % 3) - 1;
			position = new Vector3(xPosition * tileSize, 0, tileSize * levelPartCount);

			FareColor randomColor = GetRandomColor();
			Material fareMaterial = GetFareMaterial(randomColor);

			if (HasFare(randomColor))
			{
				position.y = fareTarget.transform.position.y;
				GameObject fareTargetObject = Instantiate(fareTarget, position, fareTarget.transform.rotation, this.transform);
				fareTargetObject.GetComponent<ColorController>().SetFareColor(randomColor);

				ChangeColor(fareMaterial, fareTargetObject);
				ChangeSpawnedFareBool(randomColor);
			}
			else
			{
				int gender = Random.Range(0, 2);

				position.y = fares[gender].transform.position.y;
				GameObject fareObject = Instantiate(fares[gender], position, Quaternion.identity, this.transform);
				fareObject.GetComponent<ColorController>().SetFareColor(randomColor);

				ChangeColor(fareMaterial, fareObject);
				ChangeSpawnedFareBool(randomColor);
			}
		}
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

	Material GetFareMaterial(FareColor color)
	{
		switch (color)
		{
			case FareColor.BLUE:
				return blueFareMaterial;
			case FareColor.RED:
				return redFareMaterial;
			case FareColor.GREEN:
				return greenFareMaterial;
			default:
				return blueFareMaterial;
		}
	}

	bool HasFare(FareColor color)
	{
		switch (color)
		{
			case FareColor.BLUE:
				return hasBlueFare;
			case FareColor.RED:
				return hasRedFare;
			case FareColor.GREEN:
				return hasGreenFare;
			default:
				return false;
		}
	}

	void ChangeColor(Material newMaterial, GameObject fareObject)
	{
		MeshRenderer[] meshes = fareObject.GetComponentsInChildren<MeshRenderer>();
		SkinnedMeshRenderer[] skinnedMeshes = fareObject.GetComponentsInChildren<SkinnedMeshRenderer>();

		foreach(MeshRenderer mesh in meshes)
		{
			mesh.sharedMaterial = newMaterial;
		}

		foreach(SkinnedMeshRenderer skinMesh in skinnedMeshes)
		{
			skinMesh.sharedMaterial = newMaterial;
		}
	}

	void ChangeSpawnedFareBool(FareColor color)
	{
		switch (color)
		{
			case FareColor.BLUE:
				hasBlueFare = !hasBlueFare;
				break;
			case FareColor.RED:
				hasRedFare = !hasRedFare;
				break;
			case FareColor.GREEN:
				hasGreenFare = !hasGreenFare;
				break;
		}
	}
}
