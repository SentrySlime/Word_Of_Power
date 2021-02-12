using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddTRaits : MonoBehaviour
{

    ManageRandomTrait manageRandomTrait;
    ManageRandomAbility manageRandomAbility;
    PopUpTrait popUpTrait;

    public GameObject traitTemplet;
    public GameObject traitContainer;

    public Image traitImage;

    public AbilityStatChange abilityStatChange;

    void Start()
    {

        GetComponent<Button>().onClick.AddListener(AddToOwnedTraits);

        abilityStatChange = GetComponent<AbilityStatChange>();
        manageRandomTrait = GameObject.Find("RandomizeAbilities").GetComponent<ManageRandomTrait>();
        manageRandomAbility = GameObject.Find("RandomizeAbilities").GetComponent<ManageRandomAbility>(); 
        traitTemplet = Resources.Load<GameObject>("Traits/TraitImages/TraitTemplet");
        
        popUpTrait = traitTemplet.GetComponent<PopUpTrait>();

        traitContainer = GameObject.Find("TraitContainer");


    }

    public void AddToOwnedTraits()
    {
        abilityStatChange.ChangeNumbers();

        popUpTrait.traitCard = gameObject;
        popUpTrait.traitCardImage = traitImage;
        popUpTrait.GetComponent<Image>().sprite = popUpTrait.traitCardImage.sprite;

        Instantiate(traitTemplet, traitContainer.transform);


        for (int i = 0; i < manageRandomTrait.threeRandomTraits.Count; i++)
        {
            if (ReferenceEquals(manageRandomTrait.threeRandomTraits[i].gameObject, gameObject))
            {
                manageRandomTrait.threeRandomTraits.RemoveAt(i);
            }
        }

        manageRandomTrait.RevertRandomCards();

        manageRandomTrait.randomizeTrait.interactable = true;
        manageRandomAbility.randomizeAbilities.interactable = true;

        gameObject.SetActive(false);
    }
}
