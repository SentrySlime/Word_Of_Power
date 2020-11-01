using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EquipingAbilities : MonoBehaviour
{
    public Ability ability;

    [SerializeField] Ability[] abilityArray;

    int randomAbilityNumber;

    private void Start()
    {
        abilityArray = Resources.LoadAll<Ability>("Abilities");
        ability = abilityArray[0];
    }

    private void Update()
    {

        //projectile = projectileAbility.projectile;

        if (Input.GetKeyDown(KeyCode.R))
        {
            EquipingAbility1();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            EquipingAbility2();
        }
    }

    public void RandomNumberGenerator(int aRandomNumber)
    {
        randomAbilityNumber = Random.Range(0, aRandomNumber);
    }

    public void RandomAbility()
    {
        RandomNumberGenerator(abilityArray.Length);
        //All of this fucking code chooses a random number and matches the abilities ID with that random number
    }

    public void EquipingAbility1()
    {

        
        AbilityCooldown abilitySlot1 = GameObject.Find("AbilitySlot1").GetComponent<AbilityCooldown>();
        




        //abilityCooldown.SettingAbility(projectileAbility);
        //abilityCooldown.Initializing(scriptObj, weaponHolder);
    }

    public void EquipingAbility2()
    {
        AbilityCooldown abilitything2 = GameObject.Find("AbilitySlot2").GetComponent<AbilityCooldown>();
        //abilitything2.projectileAbility = ProjectileAbilityArray[2];
        
    }


}