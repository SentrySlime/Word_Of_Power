using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageRandomTrait : MonoBehaviour
{
    public List<Button> threeRandomTraits;
    public List<Button> traitList = new List<Button>();
    //public List<Image> 

    CharacterStats characterStats;
    ManageRandomAbility manageRandomAbility;

    public Button randomizeTrait;
    public Transform traitCardHolder;

    void Start()
    {
        characterStats = GameObject.Find("Player").GetComponent<CharacterStats>();
        traitCardHolder = GameObject.Find("TraitCardHolder").GetComponent<Transform>();
        randomizeTrait = GameObject.Find("RandomizeTraitButton").GetComponent<Button>();
        manageRandomAbility = GetComponent<ManageRandomAbility>();

        List<Button> tempTraitList = new List<Button>(Resources.LoadAll<Button>("Traits/CardButtons"));

        randomizeTrait.onClick.AddListener(RandomizeTraits);

        for (int i = 0; i < tempTraitList.Count; i++)
        {
            traitList.Add(Instantiate(tempTraitList[i], traitCardHolder.transform));
        }
    }

    
    public void RandomizeTraits()
    {
        randomizeTrait.interactable = false;                                //So you can't click it again once you are in a state of choosing
        manageRandomAbility.randomizeAbilities.interactable = false;

        characterStats.AAPoint--;                                           //So you can't click it again once you've chosen a trait or ability
        for (int i = 0; i < 3; i++)                                         //For the 3 randomly choosen traits
        {
            int randomNumber = Random.Range(0, traitList.Count);            //Picks a random number from the amount of traits available
            SetParent(randomNumber);                                        //Calls the setparent function for the current randomly chosen number

            threeRandomTraits.Add(traitList[randomNumber]);                     
            traitList.RemoveAt(randomNumber);                               //Then remove it from the list so you it doesn't get picked in the future
        }

        if (characterStats.AAPoint < 1)                                      //And if the AAPoints are less than 1 it inactivates the randomize button until you level up again.
        {
            randomizeTrait.gameObject.SetActive(false);
            manageRandomAbility.randomizeAbilities.gameObject.SetActive(false);
        }

    }

    public void SetParent(int randomNumberInput)
    {
        for (int i = 0; i < 3; i++)                                                     //for the 3 chosen traits
        {
            traitList[randomNumberInput].transform.SetParent(gameObject.transform);     //Transform thems them to the location on the screen where you can see them
        }
    }

    public void RevertRandomCards()
    {
        for (int i = 0; i < threeRandomTraits.Count; i++)
        {
            threeRandomTraits[i].transform.SetParent(traitCardHolder.transform, false);
            traitList.Add(threeRandomTraits[i]);
        }
            threeRandomTraits.Clear();
    }


}
