using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AbilityButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public AbilityCooldown abilityCooldown;
    AbilitySelection abilitySelectionLocation;


    public Button abilityButton;
    public Ability ability;
    public GameObject abilityCard;
    public Image buttonImage;

    GameObject tempAbilityCard;

    private void Awake()
    {
        abilityButton = GetComponent<Button>();
        buttonImage = GetComponent<Image>();
        abilityButton.onClick.AddListener(EquipAbility);
        abilitySelectionLocation = GameObject.Find("Canvas").GetComponentInChildren<AbilitySelection>();
    }

    public void EquipAbility()
    {
        abilityCooldown.ability = ability;
        abilityCooldown.Initialize(abilityCooldown.ability, abilityCooldown.weaponHolder);
        GameObject abilitySelection = GameObject.Find("AbilitySelectionPanel");
        abilitySelection.SetActive(false);
        Destroy(tempAbilityCard);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(tempAbilityCard == null)
        {

            tempAbilityCard = Instantiate(abilityCard, gameObject.transform.position, Quaternion.identity);
            tempAbilityCard.transform.SetParent(abilitySelectionLocation.transform);
            tempAbilityCard.transform.localScale = new Vector3(1, 1, 1);
            tempAbilityCard.transform.localPosition += (tempAbilityCard.transform.up * 80) + (tempAbilityCard.transform.right * 50);
            tempAbilityCard.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Destroy(tempAbilityCard);
    }
}
