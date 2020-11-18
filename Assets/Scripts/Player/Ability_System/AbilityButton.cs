using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour
{
    public AbilityCooldown abilityCooldown;
    public Button abilityButton;
    public Ability ability;                        

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
        GameObject abilitySelection = GameObject.Find("AbilitySelectionPanel");
        abilitySelection.SetActive(false);
    }
}
