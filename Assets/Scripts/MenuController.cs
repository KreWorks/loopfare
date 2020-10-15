using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class MenuController : MonoBehaviour
{
	public Action<AbilitySO> OnAbilitySelected;
	public Action<AbilityType> OnAbilityUnlock;

	public GameObject mainMenu;
	public GameObject abilitiesMenu;

	public GameObject mainTab;
	public GameObject optionsTab;
	public GameObject abilitiesTab;

	public TMP_Text mainTabBankAccount;
	public TMP_Text mainTabCoins;

	public TMP_Text abilitiesTabBankAccount;
	public TMP_Text abilitiesTabCost;
	public TMP_Text abilitiesTabTitle;
	public TMP_Text abilitiesTabDescription;

	public Slider volumeSlider;
	public Slider musicToggle;

	AbilitySO selectedAbility;

	void Start()
	{
		HideAllMenuAndTab();

		mainMenu.SetActive(true);
		mainTab.SetActive(true);
		UpdateAccounts();
	}

	private void UpdateAccounts()
	{
		mainTabCoins.text = "In your last run you collected " + GameDatas.CollectedCoins.ToString() + " coins.";
		mainTabBankAccount.text = "You current bank account is " + GameDatas.BankAccount.ToString() + " coins.";

		abilitiesTabBankAccount.text = "Account: " + GameDatas.BankAccount.ToString();
	}

	public void PlayGameButtonAction()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void OptionsButtonAction()
	{
		HideAllMenuAndTab();

		mainMenu.SetActive(true);
		optionsTab.SetActive(true);
	}

	public void AbilitiesButtonAction()
	{
		HideAllMenuAndTab();

		abilitiesMenu.SetActive(true);
		abilitiesTab.SetActive(true);
	}

	public void QuitButtonAction()
	{
		Application.Quit();
	}

	public void UnlockButtonAction()
	{
		if (GameDatas.HasAbility(selectedAbility.parentAbility) && GameDatas.BankAccount >= selectedAbility.cost)
		{
			GameDatas.SetAbility(selectedAbility.type, true);
			GameDatas.SpendMoney(selectedAbility.cost);

			UpdateAccounts();
			OnAbilityUnlock?.Invoke(selectedAbility.type);
		}
	}

	public void AbilitiesBackButtonAction()
	{
		HideAllMenuAndTab();

		mainMenu.SetActive(true);
		mainTab.SetActive(true);
	}

	public void OptionsResetButtonAction()
	{
		AbilityManager abilityManager = FindObjectOfType<AbilityManager>();

		abilityManager.ResetAbilities();
	}

	void HideAllMenuAndTab()
	{
		mainMenu.SetActive(false);
		abilitiesMenu.SetActive(false);

		mainTab.SetActive(false);
		optionsTab.SetActive(false);
		abilitiesTab.SetActive(false);
	}

	public void SetMusicVolume()
	{
		PlayerPrefs.SetFloat("AudioVolume", volumeSlider.value);
	}

	public void SetBackGroundMusic()
	{
		int isMusic = PlayerPrefs.GetInt("IsMusic", 1);
		//We want it to work as a toggle
		if (isMusic == 1.0f && musicToggle.value < 1.0f)
		{
			musicToggle.value = 0.0f;
			PlayerPrefs.SetInt("IsMusic", 0);
		}
		else if (isMusic == 0.0f && musicToggle.value > 0.0f)
		{
			musicToggle.value = 1.0f;
			PlayerPrefs.SetInt("IsMusic", 1);
		}
	}

	public void SetSelectedAbilityData(AbilitySO ability)
	{
		abilitiesTabCost.text = "cost: " + ability.cost.ToString();
		abilitiesTabTitle.text = ability.title;

		selectedAbility = ability;

		if (GameDatas.HasAbility(ability.parentAbility))
		{
			if (GameDatas.BankAccount >= ability.cost)
			{
				abilitiesTabDescription.text = ability.description;
			}
			else
			{
				abilitiesTabDescription.text = "You don't have enough money for this.";
			}
		}
		else
		{
			abilitiesTabDescription.text = "You should unlock the parent ability first.";
		}

		OnAbilitySelected?.Invoke(ability);
	}

	public void AddListenerOnAbilitySelectedEvent(Action<AbilitySO> listener)
	{
		OnAbilitySelected += listener;
	}
	public void RemoveListenerOnAbilitySelectedEvent(Action<AbilitySO> listener)
	{
		OnAbilitySelected -= listener;
	}

	public void AddListenerOnAbilityUnlockEvent(Action<AbilityType> listener)
	{
		OnAbilityUnlock += listener;
	}
	public void RemoveListenerOnAbilityUnlockEvent(Action<AbilityType> listener)
	{
		OnAbilityUnlock -= listener;
	}
}
