using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySelection : MonoBehaviour
{
    [HideInInspector] public List<Ability> abilityList;
    [HideInInspector] public List<AbilityButton> abilityButtonList;
    public List<AbilityButton> ownedAbilities = new List<AbilityButton>();
    public List<CardAbility> cardAbility = new List<CardAbility>();

    private void Awake()
    {
        abilityList = new List<Ability>(Resources.LoadAll<Ability>("Abilities"));
        abilityButtonList = new List<AbilityButton>(Resources.LoadAll<AbilityButton>("Abilities/Buttons"));
        cardAbility = new List<CardAbility>(Resources.LoadAll<CardAbility>("Abilities/Cards"));
    }

    private void Start()
    {
        UpdateOwnedAbilities();
        SetAllAbilities();
        gameObject.SetActive(false);
    }

    //Instantiate all the buttons under a button holder 
    //Then when clicking the card we simply parent it to ability selection which requires a layout group Cardability.AddToOwnedAbilities

    private void SetAllAbilities()
    {
        for (int i = 0; i < abilityButtonList.Count; i++)
        {
            abilityButtonList[i].ability = abilityList[i];
            cardAbility[i].abilityButton = abilityButtonList[i];
            cardAbility[i].ability = abilityButtonList[i].ability;
            //cardAbility[i].SetCardAbility();
        }
    }

    public void SetAbilitySlot(AbilityCooldown abilitySlot)
    {
        for (int i = 0; i < ownedAbilities.Count; i++)
        {
            ownedAbilities[i].abilityCooldown = abilitySlot;
        }

    }

    public void UpdateOwnedAbilities()
    {

        for (int i = 0; i < transform.childCount; i++)
        {
            
            ownedAbilities.Add(transform.GetChild(i).gameObject.GetComponent<AbilityButton>());

        }

    }

}