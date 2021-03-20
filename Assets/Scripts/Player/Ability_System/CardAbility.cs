using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardAbility : MonoBehaviour
{

    public ManageRandomAbility manageRandomAbility;
    ManageRandomTrait manageRandomTrait;
    public AbilityButton abilityButton;
    AbilitySelection abilitySelection;
    public Ability ability;

    public TextMeshProUGUI abilityDescription;
    public Image abilityImage;
    public TextMeshProUGUI abilityName;
     public Text abilityCooldownText;
    Button cardButton;

    public CharacterStats characterStats;

    private void Awake()
    {
        characterStats = GameObject.Find("Player").GetComponent<CharacterStats>();
        abilitySelection = GameObject.Find("AbilitySelection").GetComponent<AbilitySelection>();
        manageRandomAbility = GameObject.Find("RandomizeAbilities").GetComponent<ManageRandomAbility>();
        manageRandomTrait = GameObject.Find("RandomizeAbilities").GetComponent<ManageRandomTrait>();
        cardButton = GetComponent<Button>();
        cardButton.onClick.AddListener(AddToOwnedAbilities);
    }

    //public void SetCardAbility()
    //{
    //    abilityImage.sprite = ability.aSprite;
    //    abilityName.text = ability.aName;
    //    abilityDescription.text = ability.aDescription;

    //}

    public void AddToOwnedAbilities()
    {

        manageRandomAbility.randomizeAbilities.interactable = true;
        manageRandomTrait.randomizeTrait.interactable = true;

        for (int i = 0; i < abilitySelection.abilityButtonList.Count; i++)
        {

            if (ReferenceEquals(abilitySelection.abilityButtonList[i].ability, ability))
            {
                
                abilitySelection.abilityButtonList[i].transform.SetParent(abilitySelection.abilitySelectionPanel.transform, false);
            }

        }

        manageRandomAbility.tempCardHolderList.Remove(this);
        manageRandomAbility.RevertRandomCards();
        gameObject.SetActive(false);
    }

}
