using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageRandomAbility : MonoBehaviour
{
    //Card ability lists
    public List<CardAbility> tempCardHolderList = new List<CardAbility>();
    public List<CardAbility> cardHolderList = new List<CardAbility>();                  //Tempcardholder is instantiated cards which can then be parented under "Randomize abilities" so you can click one and add the ability to your owned abilities
    
    //Other class references
    CharacterStats characterStats;
    
    //Gamobjects
    public Button randomizeAbilities;
    public Transform cardHolder;



    void Start()
    {
        characterStats = GameObject.Find("Player").GetComponent<CharacterStats>();
        cardHolder = GameObject.Find("CardHolder").GetComponent<Transform>();
        randomizeAbilities = GameObject.Find("RandomizeAbilityButton").GetComponent<Button>();
        randomizeAbilities.onClick.AddListener(RandomizeAbilities);

        randomizeAbilities.gameObject.SetActive(true);

    }

    public void RandomizeAbilities()
    {
        randomizeAbilities.interactable = false;
        characterStats.AAPoint--;
        for (int i = 0; i < 3; i++)                                                         //This will give an error if you have less than 3 cards
        {
            int randomNumber = Random.Range(0, cardHolderList.Count);                       //This picks a random number based on how many abilities there are in the game
            SetParent(randomNumber);


            tempCardHolderList.Add(cardHolderList[randomNumber]);
            cardHolderList.RemoveAt(randomNumber);                                          //this removes all the abilities the player picked from the pool so you can't get it again, since you remove it you don't have to check if the ability is already taken
        }                                                                                   //You should only remove a card when you click on it

        if(characterStats.AAPoint < 1)
        {
            randomizeAbilities.gameObject.SetActive(false);
        }
    }

    public void RevertRandomCards()
    {
        for (int i = 0; i < tempCardHolderList.Count; i++)
        {
            tempCardHolderList[i].transform.SetParent(cardHolder.transform, false);
            cardHolderList.Add(tempCardHolderList[i]);
        }
    }

    public void SetParent(int randomNumberInput)
    {
        for (int i = 0; i < 3; i++)
        {
            cardHolderList[randomNumberInput].transform.SetParent(gameObject.transform);
        }
    }

    public void RevertParent(int randomNumberInput)                                                                  //Between the 3 randomized cards, the 2 cards they player didn't pick gets their parent reverted to the tempholder (This might cause format issues)
    {
        tempCardHolderList[randomNumberInput].transform.SetParent(cardHolder.transform);
    }

}
