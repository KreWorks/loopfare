using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BoolInt
{
	FALSE = 0,
	TRUE = 1
}

public static class GameDatas
{
	private static int collectecCoins, bankAccount;
	private static float musicVolume;
	private static bool musicToggle;

	private static string endGameMessage;

	private static Dictionary<AbilityType, bool> abilities;

	public static int CollectedCoins
	{
		get
		{
			return collectecCoins;
		}
		set
		{
			collectecCoins = value;
		}
	}

	public static int BankAccount
	{
		get
		{
			return bankAccount;
		}
		set
		{
			bankAccount = value;
		}
	}

	public static float MusicVolume
	{
		get
		{
			return musicVolume;
		}
		set
		{
			musicVolume = value;
		}
	}

	public static bool MusicToggle
	{
		get
		{
			return musicToggle;
		}
		set
		{
			musicToggle = value;
		}
	}

	public static string EndGameMessage
	{
		get
		{
			return endGameMessage;
		}
		set
		{
			endGameMessage = value;
		}
	}

	public static bool HasAbility(AbilityType type)
	{
		bool result;

		if (abilities.TryGetValue(type, out result))
		{
			return result;
		}
		else
		{
			return false;
		}
	}

	public static void SetAbility(AbilityType type, bool value)
	{
		if(abilities == null)
		{
			abilities = new Dictionary<AbilityType, bool>();
		}

		if (abilities.ContainsKey(type))
		{
			abilities[type] = value;
		}
		else
		{
			abilities.Add(type, value);
		}		
	}

	public static void SpendMoney(int cost)
	{
		bankAccount -= cost;
	}

	public static void AddCollectedCoinsToBank(int collected)
	{
		collectecCoins = collected;
		bankAccount += collected;
	}

	public static void SaveData()
	{
		PlayerPrefs.SetInt("CollectedCoins", collectecCoins);
		PlayerPrefs.SetInt("BankAccount", bankAccount);

		PlayerPrefs.SetFloat("MusicVolume", musicVolume);
		PlayerPrefs.SetInt("MusicToggle", (int)(musicToggle ? BoolInt.TRUE : BoolInt.FALSE));

		foreach (AbilityType ability in System.Enum.GetValues(typeof(AbilityType)))
		{
			string abilityName = System.Enum.GetName(typeof(AbilityType), ability);

			PlayerPrefs.SetInt(abilityName, (int)(HasAbility(ability) ? BoolInt.TRUE : BoolInt.FALSE));
		}
	}

	public static void LoadData()
	{
		collectecCoins = PlayerPrefs.GetInt("CollectedCoins", collectecCoins);
		bankAccount = PlayerPrefs.GetInt("BankAccount", bankAccount);

		musicVolume = PlayerPrefs.GetFloat("MusicVolume", musicVolume);
		musicToggle = PlayerPrefs.GetInt("MusicToggle") == (int)BoolInt.TRUE;

		foreach (AbilityType ability in System.Enum.GetValues(typeof(AbilityType)))
		{
			string abilityName = System.Enum.GetName(typeof(AbilityType), ability);

			bool abilityValue = PlayerPrefs.GetInt(abilityName) == (int)BoolInt.TRUE;
			SetAbility(ability, abilityValue);
		}
	}

	public static void ResetAbilities()
	{
		foreach (AbilityType ability in System.Enum.GetValues(typeof(AbilityType)))
		{
			if (ability == AbilityType.NULL)
			{
				SetAbility(ability, true);
			}
			else
			{
				SetAbility(ability, false);
			}
		}
	}

	public static float GetCarSpeedMultiplier()
	{
		if (GameDatas.HasAbility(AbilityType.SPEED_175))
		{
			return 1.75f;
		}
		else if (GameDatas.HasAbility(AbilityType.SPEED_150))
		{
			return 1.5f;
		}
		else if (GameDatas.HasAbility(AbilityType.SPEED_125))
		{
			return 1.25f;
		}
		else
		{
			return 1.0f;
		}
	}

	public static int GetFareDistance()
	{
		if (HasAbility(AbilityType.FARE_DISTANCE_5))
		{
			return 5; 
		}
		else if (HasAbility(AbilityType.FARE_DISTANCE_6))
		{
			return 6;
		}
		else if (HasAbility(AbilityType.FARE_DISTANCE_7))
		{
			return 7; 
		}
		else
		{
			return 8;
		}
	}

	public static int GetStash()
	{
		if (HasAbility(AbilityType.STASH_100))
		{
			return 100;
		}
		else if (HasAbility(AbilityType.STASH_50))
		{
			return 50;
		}
		else if (HasAbility(AbilityType.STASH_30))
		{
			return 30;
		}
		else
		{
			return 20;
		}
	}

	public static int GetFarePrice()
	{
		if (HasAbility(AbilityType.FARE_PRICE_10))
		{
			return 10;
		}
		else if (HasAbility(AbilityType.FARE_PRICE_5))
		{
			return 5;
		}
		else if (HasAbility(AbilityType.FARE_PRICE_3))
		{
			return 3;
		}
		else
		{
			return 2;
		}
	}
}
