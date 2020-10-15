using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
	{
		ResetAbilities();

		GameDatas.BankAccount = 200;
	}

	public void ResetAbilities()
	{
		foreach (AbilityType ability in System.Enum.GetValues(typeof(AbilityType)))
		{
			if (ability == AbilityType.NULL)
			{
				GameDatas.SetAbility(ability, true);
			}
			else
			{
				GameDatas.SetAbility(ability, false);
			}
		}
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
