using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AbilityController : MonoBehaviour, IPointerClickHandler
{
	public AbilitySO ability;

	public Color lockedColor;
	public Color unlockedColor;
	public Color selectedColor;
	public Color disabledColor;

	bool locked;
	bool selected;

	Image border;
	Image background;
	Image icon;

	MenuController menuController;

    // Start is called before the first frame update
    void Start()
    {
		menuController = FindObjectOfType<MenuController>();
		menuController.AddListenerOnAbilitySelectedEvent(OtherIconSelected);
		menuController.AddListenerOnAbilityUnlockEvent(AbilityUnlocked);

		locked = !GameDatas.HasAbility(ability.type);
		selected = false;

		Image[] images = GetComponentsInChildren<Image>();

		foreach(Image iconPart in images)
		{
			if (iconPart.tag == "IconBorder")
			{
				border = iconPart;
			}
			else if(iconPart.tag == "IconBackground")
			{
				background = iconPart;
			}
			else if(iconPart.tag == "IconImage")
			{
				icon = iconPart;
			}
		}

		SetBorderColor();
		SetIconImage();
    }

	void SetBorderColor()
	{
		//Unlocked
		if (!locked)
		{
			border.color = unlockedColor;
		}
		//Selected
		else if (selected)
		{
			border.color = selectedColor;
		}
		//Has enought money, parent unlocked, not selected
		else if(!selected && ability.cost <= GameDatas.BankAccount && GameDatas.HasAbility(ability.parentAbility))
		{
			border.color = lockedColor;
		}
		//no money OR no parent unlocked AND not selected
		else if (!selected && (ability.cost > GameDatas.BankAccount || !GameDatas.HasAbility(ability.parentAbility)))
		{
			border.color = disabledColor;
		}
		else
		{
			Debug.Log("Something is not covered");
		}
	
	}

	void SetIconImage()
	{
		if (locked)
		{
			icon.sprite = ability.iconBW;
		}
		else
		{
			icon.sprite = ability.iconColor;
		}
	}

	void OtherIconSelected(AbilitySO ability)
	{
		if(this.ability != ability)
		{
			this.selected = false;
		}

		SetBorderColor();
	}

	void AbilityUnlocked(AbilityType abilityType)
	{
		if(abilityType == ability.type)
		{
			locked = false;

			SetIconImage();
			SetBorderColor();
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (locked)
		{ 
			selected = true;
			SetBorderColor();

			menuController.SetSelectedAbilityData(ability);
		}
	}

}
