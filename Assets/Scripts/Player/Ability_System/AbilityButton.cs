using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour
{
    public Button abilityButton;
    public Ability ability;                         //We actually want to set this variable holding the ability
    public AbilityCooldown abilityCooldown;

    public Image buttonImage;

    private void Awake()
    {
        abilityButton = GetComponent<Button>();
        buttonImage = GetComponent<Image>();
        abilityButton.onClick.AddListener(EquipAbility);

    }

    public void EquipAbility()
    {
        abilityCooldown.ability = ability;
        abilityCooldown.Initialize(abilityCooldown.ability, abilityCooldown.weaponHolder);
        //Remove from the pop up square containing all the abilities teh player currently has
        GameObject abilitySelection = GameObject.Find("AbilitySelectionPanel");
        abilitySelection.SetActive(false);
    }
}
