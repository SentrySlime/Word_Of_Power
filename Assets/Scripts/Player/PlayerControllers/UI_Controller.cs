using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_Controller : MonoBehaviour
{
    public GameObject characterMenu;
    public CharacterStats characterStats;
    public ManageRandomAbility manageRandomAbility;
    private void Start()
    {
        characterMenu = GameObject.Find("CharacterMenu");
        characterStats = GameObject.Find("Player").GetComponent<CharacterStats>();
        manageRandomAbility = GameObject.Find("RandomizeAbilities").GetComponent<ManageRandomAbility>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            if(characterMenu.activeSelf)
            {
                characterMenu.SetActive(false);
            }
            else if(!characterMenu.activeSelf)
            {
                characterMenu.SetActive(true);
                if (characterStats.AAPoint < 0)
                {
                    manageRandomAbility.randomizeAbilities.gameObject.SetActive(false);
                }
            }
        }
        
        if(Input.GetKeyDown(KeyCode.K))
        {

        }

    }
}
