using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardAbility : MonoBehaviour
{

    public Button cardButton;

    public Image abilityImage;
    public Text abilityName;
    public Text abilityDescription;

    public Ability ability;

    public AbilityButton abilityButton;

    ManageRandomAbility manageRandomAbility;

    AbilitySelection abilitySelection;

    private void Awake()
    {
        abilitySelection = GameObject.Find("AbilitySelection").GetComponent<AbilitySelection>();

        cardButton = GetComponent<Button>();

        cardButton.onClick.AddListener(AddToOwnedAbilities);
        //SetCardAbility();

    }

    public void Start()
    {
        //SetCardAbility();
    }

    public void SetCardAbility()
    {
        //ability = abilityButton.ability;

        abilityImage.sprite = ability.aSprite;
        abilityName.text = ability.aName;
        abilityDescription.text = ability.aDescription;
    }

    public void AddToOwnedAbilities()
    {
        abilitySelection.ownedAbilities.Add(abilityButton);
        //Destroy or tremove the three cards.
    }

    //This button needs a reference to the ability button
    //So when you click this card button, you add that ability to the owned pool
    //

}
