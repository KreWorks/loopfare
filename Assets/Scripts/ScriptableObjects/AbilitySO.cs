using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Loop Fare / Ability")]
public class AbilitySO : ScriptableObject
{
	public string title;
	public string description;
	public Sprite iconBW;
	public Sprite iconColor;
	public int cost;
	public AbilityType type;
	public float abilityValue;
	public AbilityType parentAbility;
}
