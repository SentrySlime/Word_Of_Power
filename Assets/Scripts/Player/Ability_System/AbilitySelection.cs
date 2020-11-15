using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySelection : MonoBehaviour
{
    [HideInInspector] public List<Ability> abilityList;                                                         //This is a list with all the abilities
    [HideInInspector] public AbilityButton abilityButtonPrefab;
    /*[HideInInspector]*/ public List<AbilityButton> abilityButtonList;                                         //This is the list with all the buttons, but it should only be a single button

    ManageRandomAbility manageRandomAbility;


    public List<AbilityButton> ownedAbilities = new List<AbilityButton>();

    CardAbility cardAbilityPrefab;
    //public List<CardAbility> cardAbility = new List<CardAbility>();                                             //This is a list with all the cards, but it should only be a single card
    GameObject abilityButtonHolder;
    public GameObject abilitySelectionPanel;


    private void Awake()
    {
        abilityList = new List<Ability>(Resources.LoadAll<Ability>("Abilities"));                               //you can load all abilities into a list

        manageRandomAbility = GameObject.Find("RandomizeAbilities").GetComponent<ManageRandomAbility>();

        abilityButtonPrefab = Resources.Load<AbilityButton>("Abilities/Buttons/AbilitySelection");
        cardAbilityPrefab = Resources.Load<CardAbility>("Abilities/Cards/AbilityCard");

        //cardAbility = new List<CardAbility>(Resources.LoadAll<CardAbility>("Abilities/Cards"));

        abilityButtonHolder = GameObject.Find("AbilityButtonHolder");

        abilitySelectionPanel = GameObject.Find("AbilitySelectionPanel");
    }

    private void Start()
    {
        UpdateOwnedAbilities();
        SetAllAbilities();
        abilitySelectionPanel.gameObject.SetActive(false);
    }



    private void SetAllAbilities()                                          //This function should create the buttons and the cards then assign the correct abilities to the cards
    {
        InstantiateCards();
        for (int i = 0; i < abilityList.Count; i++)
        {
            abilityButtonList.Add(Instantiate(abilityButtonPrefab, abilityButtonHolder.transform));                 //Creates the buttons and add them to the abilitybuttonlist

            abilityButtonList[i].ability = abilityList[i];                                                          //Take all ability buttons and assign an ability to them

            manageRandomAbility.cardHolderList[i].abilityButton = abilityButtonList[i];                             //Take all ability cards and assign an abilitybutton to it

            manageRandomAbility.cardHolderList[i].ability = abilityButtonList[i].ability;                           //Then sets the ability of the corresponding button

            //abilityButtonList[i].gameObject.SetActive(false);                               //Inactivates them 
        }
        AbilityButtonVisual();
        InitializeCards();
    }


    public void AbilityButtonVisual()
    {
        for (int i = 0; i < abilityButtonList.Count; i++)
        {
            abilityButtonList[i].buttonImage.sprite = abilityButtonList[i].ability.aSprite;
        }
    }

    public void SetAbilitySlot(AbilityCooldown abilitySlot)
    {
        for (int i = 0; i < abilityButtonList.Count; i++)
        {
            abilityButtonList[i].abilityCooldown = abilitySlot;
        }


    }

    public void InstantiateCards()
    {
        for (int i = 0; i < abilityList.Count; i++)
        {
            manageRandomAbility.cardHolderList.Add(Instantiate(cardAbilityPrefab,manageRandomAbility.cardHolder.transform));
            
        }
    }

    public void InitializeCards()
    {
        for (int i = 0; i < manageRandomAbility.cardHolderList.Count; i++)
        {

            manageRandomAbility.cardHolderList[i].abilityName.text = manageRandomAbility.cardHolderList[i].ability.aName;
            manageRandomAbility.cardHolderList[i].abilityDescription.text = manageRandomAbility.cardHolderList[i].ability.aDescription;
            manageRandomAbility.cardHolderList[i].abilityImage.sprite = manageRandomAbility.cardHolderList[i].ability.aSprite;
        }
    }

    public void UpdateOwnedAbilities()
    {
        ownedAbilities.Clear();
        for (int i = 0; i < abilitySelectionPanel.transform.childCount; i++)
        {
            ownedAbilities.Add(abilitySelectionPanel.transform.GetChild(i).gameObject.GetComponent<AbilityButton>());
        }

    }

    public void HideOrShowOwnedAbilities()
    {
        for (int i = 0; i < ownedAbilities.Count; i++)
        {
            if(ownedAbilities[i].gameObject.activeSelf == true)
            {
                ownedAbilities[i].gameObject.SetActive(false);
            }
            else
            {
                ownedAbilities[i].gameObject.SetActive(true);
            }
        }
    }

}