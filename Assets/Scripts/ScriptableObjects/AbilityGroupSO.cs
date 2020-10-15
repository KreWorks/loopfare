using UnityEngine;

[CreateAssetMenu(fileName = "AbilityGroup", menuName = "Loop Fare / AbilityGroup")]
public class AbilityGroupSO : ScriptableObject
{
	public string groupTitle;

	public AbilitySO[] level;
}
