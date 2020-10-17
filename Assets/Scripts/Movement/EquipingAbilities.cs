//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class EquipingAbilities : MonoBehaviour
//{

//    public AbilityCooldown abilityCooldown;

//    public GeneralAbilities projectileAbility;
//    public AbilityScriptable scriptObj;
//    public GameObject projectile;
//    public GameObject weaponHolder;

//    [SerializeField] GeneralAbilities[] ProjectileAbilityArray;
//    //[SerializeField] MeleeAbility[] MeleeAbilityArray;

//    int randomAbilityNumber;

//    private void Start()
//    {
//        weaponHolder = GameObject.Find("Player");
//        ProjectileAbilityArray = Resources.LoadAll<GeneralAbilities>("Prefabs/ScriptableObjects/ScriptableAbilites");
//        projectileAbility = ProjectileAbilityArray[0];
//        EquipingAbility1();
//    }

//    private void Update()
//    {

//        projectile = projectileAbility.projectile;

//        if (Input.GetKeyDown(KeyCode.R))
//        {
//            EquipingAbility1();
//        }

//        if (Input.GetKeyDown(KeyCode.Q))
//        {
//            EquipingAbility2();
//        }
//    }

//    public void RandomNumberGenerator(int aRandomNumber)
//    {
//        randomAbilityNumber = Random.Range(0, aRandomNumber);
//    }

//    public void RandomAbility()
//    {
//        RandomNumberGenerator(ProjectileAbilityArray.Length);
//        //All of this fucking code chooses a random number and matches the abilities ID with that random number
//    }

//    public void EquipingAbility1()
//    {

//        AbilityCooldown abilitything = GameObject.Find("AbilitySlot1").GetComponent<AbilityCooldown>();
//        abilitything.projectileAbility = ProjectileAbilityArray[1];
//        print(abilitything);




//        //abilityCooldown.SettingAbility(projectileAbility);
//        //abilityCooldown.Initializing(scriptObj, weaponHolder);
//    }

//    public void EquipingAbility2()
//    {
//        AbilityCooldown abilitything2 = GameObject.Find("AbilitySlot2").GetComponent<AbilityCooldown>();
//        abilitything2.projectileAbility = ProjectileAbilityArray[2];
//        print(abilitything2 + " " + ProjectileAbilityArray[2]);
//    }


//}