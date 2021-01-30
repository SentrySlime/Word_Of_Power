using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardAbility : MonoBehaviour
{

    ManageRandomAbility manageRandomAbility;
    public AbilityButton abilityButton;
    AbilitySelection abilitySelection;
    public Ability ability;

    [HideInInspector] public Text abilityDescription;
    [HideInInspector] public Image abilityImage;
    [HideInInspector] public Text abilityName;
    Button cardButton;


    private void Awake()
    {

        abilitySelection = GameObject.Find("AbilitySelection").GetComponent<AbilitySelection>();

        manageRandomAbility = GameObject.Find("RandomizeAbilities").GetComponent<ManageRandomAbility>();

        cardButton = GetComponent<Button>();

        cardButton.onClick.AddListener(AddToOwnedAbilities);
        
    }

    public void SetCardAbility()
    {
        abilityImage.sprite = ability.aSprite;
        abilityName.text = ability.aName;
        abilityDescription.text = ability.aDescription;
    }

    public void AddToOwnedAbilities()
    {

        manageRandomAbility.randomizeAbilities.interactable = true;

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
