using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilitiesRenderer : MonoBehaviour
{
	public List<AbilityGroupSO> abilities;

	public GameObject abilityLinePrefab;
	public GameObject iconPrefab;

    // Start is called before the first frame update
    void Awake()
    {
        for(int i = 0; i < abilities.Count; i++)
		{
			RenderLine(abilities[i]);
		}
    }

	void RenderLine(AbilityGroupSO abilityLine)
	{
		GameObject line = Instantiate(abilityLinePrefab, this.transform);
		TMP_Text title = line.GetComponentInChildren<TMP_Text>();
		title.text = abilityLine.groupTitle;

		for(int i = 0; i < 3; i++)
		{
			GameObject icon = Instantiate(iconPrefab, line.transform);
			AbilityController ac = icon.GetComponent<AbilityController>();
			ac.ability = abilityLine.level[i];
		}
	}
}
