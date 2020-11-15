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

    public Button cardButton;
    public Image abilityImage;
    public Text abilityName;
    public Text abilityDescription;


    int bob;


    private void Awake()
    {

        //abilityImage = GetComponent<Image>();

        //abilityName = GetComponent<Text>();

        //abilityDescription = GetComponent<Text>();

        abilitySelection = GameObject.Find("AbilitySelection").GetComponent<AbilitySelection>();

        manageRandomAbility = GameObject.Find("RandomizeAbilities").GetComponent<ManageRandomAbility>();

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

        for (int i = 0; i < abilitySelection.abilityButtonList.Count; i++)
        {

            //print(ReferenceEquals(abilitySelection.abilityButtonList[i].ability, ability));
            
            print(bob);
            if (ReferenceEquals(abilitySelection.abilityButtonList[i].ability, ability))
            {
                
                print(abilitySelection.abilityButtonList[i].ability);
                abilitySelection.abilityButtonList[i].transform.SetParent(abilitySelection.abilitySelectionPanel.transform, false);
            }

            bob++;
        }

        abilitySelection.UpdateOwnedAbilities();

        manageRandomAbility.tempCardHolderList.Remove(this);
        manageRandomAbility.RevertRandomCards();
        gameObject.SetActive(false);


        //manageRandomAbility.RevertRandomCards();
        //Add these as children under "AbilitySelection"
        //Destroy or tremove the three cards.
    }

    //This button needs a reference to the ability button
    //So when you click this card button, you add that ability to the owned pool
    //

}
