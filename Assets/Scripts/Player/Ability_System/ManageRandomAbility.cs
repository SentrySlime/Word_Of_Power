using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageRandomAbility : MonoBehaviour
{

    public Button randomizeAbilities;
    AbilitySelection abilitySelection;
    public Transform cardHolder;
    public List<CardAbility> tempCardHolder = new List<CardAbility>();

    CharacterStats characterStats;

    void Start()
    {
        characterStats = GameObject.Find("Player").GetComponent<CharacterStats>();
        abilitySelection = GameObject.Find("AbilitySelection").GetComponent<AbilitySelection>();
        cardHolder = GameObject.Find("CardHolder").GetComponent<Transform>();
        randomizeAbilities = GameObject.Find("RandomizeAbilityButton").GetComponent<Button>();
        randomizeAbilities.onClick.AddListener(RandomizeAbilities);

        randomizeAbilities.gameObject.SetActive(false);

        InstantiateCards();

    }

    void Update()
    {
        

        if(Input.GetKeyDown(KeyCode.P))
        {

        }
    }

    public void RandomizeAbilities()
    {
        
        characterStats.AAPoint--;
        for (int i = 0; i < 3; i++)
        {
            int randomNumber = Random.Range(0, tempCardHolder.Count);                       //This picks a random number based on how many abilities there are in the game
            SetParent(randomNumber);

            tempCardHolder.RemoveAt(randomNumber);                                          //this removes that same ability from the pool so you can't get it again, since you remove it you don't have to check if the ability is already taken              
        }   

        if(characterStats.AAPoint < 1)
        {
            randomizeAbilities.gameObject.SetActive(false);
        }
    }

    public void InstantiateCards()
    {
        for (int i = 0; i < abilitySelection.cardAbility.Count; i++)
        {
            tempCardHolder.Add (Instantiate(abilitySelection.cardAbility[i], cardHolder.transform));
        }
    }

    public void SetParent(int randomNumberInput)
    {
        for (int i = 0; i < 3; i++)
        {
            tempCardHolder[randomNumberInput].transform.SetParent(gameObject.transform);
        }
    }


}
