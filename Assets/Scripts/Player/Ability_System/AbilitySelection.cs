using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySelection : MonoBehaviour
{
    public List<Ability> abilityList;
    //public List<Button> abilityButtonList;
    //public AbilityButton abilityButton;

    public List<AbilityButton> abilityButtonList;

    public List<AbilityButton> ownedAbilities = new List<AbilityButton>();
    public List<AbilityButton> randomizedAbilities = new List<AbilityButton>();
    public List<AbilityButton> discardedAbilities = new List<AbilityButton>();

    private void Awake()
    {
        abilityList = new List<Ability>(Resources.LoadAll<Ability>("Abilities"));
        abilityButtonList = new List<AbilityButton>(Resources.LoadAll<AbilityButton>("Abilities/Buttons"));
        
    }

    private void Start()
    {


        UpdateOwnedAbilities();

        SetAllAbilities();

    }

    private void SetAllAbilities()
    {
        for (int i = 0; i < abilityButtonList.Count; i++)
        {
            abilityButtonList[i].ability = abilityList[i];
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