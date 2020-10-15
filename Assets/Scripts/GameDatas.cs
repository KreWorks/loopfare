using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameDatas
{
	private static int collectecCoins, bankAccount;
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
}
