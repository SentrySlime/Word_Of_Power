using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageRandomAbility : MonoBehaviour
{
    AbilitySelection abilitySelection;
    

    void Start()
    {
        abilitySelection = GameObject.Find("AbilitySelection").GetComponent<AbilitySelection>();
        RandomizeAbilities();
    }

    
    void Update()
    {

        
    }

    public void RandomizeAbilities()
    {

        for (int i = 0; i < 3; i++)
        {
            int randomNumber = Random.Range(0, abilitySelection.abilityButtonList.Count);                   //This picks a random number based on how many abilities there are in the game

            abilitySelection.randomizedAbilities.Add(abilitySelection.abilityButtonList[randomNumber]);     //This adds that ability to the list of the randomized abilities

            abilitySelection.abilityButtonList.RemoveAt(randomNumber);                                      //this removes that same ability from the pool so you can't get it again, since you remove it you don't have to check if the ability is already taken
        }   

        //then instantiate these 3 picks, then when the player picks it add it as a child to "Ability selection"   Also fix it so that things get hideen and UI gets uninteractable
    }


}
