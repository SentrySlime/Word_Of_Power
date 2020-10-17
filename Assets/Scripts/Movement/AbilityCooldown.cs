//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class AbilityCooldown : MonoBehaviour
//{

//    public string abilityButtonAxisName = "Fire1";
//    public Image darkMask;
//    public Text coolDownTextDisplay;

//    [HideInInspector] public PlayerController playerController;
//    [SerializeField] private AbilityScriptable ability;

//    [SerializeField] public GeneralAbilities projectileAbility;
//    [SerializeField] public ProjectileShootTriggerable proShot;


//    [HideInInspector] public GameObject weaponHolder;
//    [HideInInspector] public CharacterStats characterStats;
//    private Image myButtonImage;
//    private float coolDownDuration;
//    private float nextReadyTime;
//    private float coolDownTimeLeft;

//    private void Awake()
//    {
//        weaponHolder = GameObject.Find("Player");
//        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
//        characterStats = GameObject.Find("Player").GetComponent<CharacterStats>();
//        proShot = GameObject.Find("Player").GetComponent<ProjectileShootTriggerable>();
//    }

//    private void Start()
//    {
//        Initializing(ability, weaponHolder);    //This Initializes the ability and should be called when equiping a different skill
//        SettingAbility(projectileAbility);
//    }

//    public void Initializing(AbilityScriptable selectedAbility, GameObject weaponHolder) //This gets called when the game start, but instead we want to call this when equiping a different ability
//    {

//        ability = selectedAbility;                      //This decides what skill is being used,---- This is set through inspector
//        myButtonImage = GetComponent<Image>();          //This gets the button to all the components used for the cooldown
//        myButtonImage.sprite = ability.aSprite;         //This sets the sprite to the sprite of the ability
//        darkMask.sprite = ability.aSprite;              //This gets the darkmask which lets the ability do a 360 circle for the duration of the cooldown
//        coolDownDuration = ability.aBaseCoolDown;       //This gets the cooldown number of the ability
//        ability.Initialize(weaponHolder);               //This part lets it initialize using ProjectileShootTriggerable, since it needs to acces it to shoot projectiles
//        AbilityReady();                                 //This indicates to the player through the icon that the Ability is ready
//    }

//    public void SettingAbility(GeneralAbilities projectileAbility)
//    {
//        //This code will give the projectile to the shooter, so you can have different projetiles at once
//        proShot.projectileCopy = projectileAbility.projectile;
//        proShot.projectileForce = projectileAbility.projectileForce;

//    }

//    private void Update()
//    {
//        bool coolDownComplete = (Time.time > nextReadyTime);
//        if (playerController.isStill && coolDownComplete)
//        {
//            AbilityReady();
//            if (Input.GetButton(abilityButtonAxisName) && characterStats.currentEnergy >= ability.aEnergyCost)
//            {
//                playerController.Turning();
//                SettingAbility(projectileAbility);
//                SubtractEnergy();
//                ButtonTriggered();
//            }
//        }
//        else
//        {
//            CoolDown();
//        }

//        print(projectileAbility.projectile.name);
//    }

//    private void AbilityReady()         //This method simply displays to the player the status of the cooldown
//    {
//        coolDownTextDisplay.enabled = false;    //This removes the countdown text from the Ability icon
//        darkMask.enabled = false;               //This completely disables the dark overlay of the Ability icon
//    }

//    private void CoolDown()                         //This is the method used to control the cooldown
//    {
//        coolDownTimeLeft -= Time.deltaTime;                             //This counts down the timer for the Ability cooldown
//        float roundedCd = Mathf.Round(coolDownTimeLeft);                //This rounds that timer to a round number 
//        coolDownTextDisplay.text = roundedCd.ToString();                //This displays that same rounded number on the text so the player will know how long until it is done
//        darkMask.fillAmount = (coolDownTimeLeft / coolDownDuration);    //This fills the radial darkmask in a 360, in accordance to the status of the timer
//    }

//    private void SubtractEnergy()
//    {
//        characterStats.currentEnergy -= ability.aEnergyCost;
//    }

//    private void ButtonTriggered()                  //This method sets a new cooldown and uses the ability
//    {
//        nextReadyTime = coolDownDuration + Time.time;   //This sets the number for the cooldown
//        coolDownTimeLeft = coolDownDuration;            //This sets the countdown clock equal to the cooldown
//        darkMask.enabled = true;                        //This enables the darkmask so it can do a 360 radial
//        coolDownTextDisplay.enabled = true;             //This enables the text to the cooldown timer

//        ability.TriggerAbility();                       //This uses the Ability
//    }
//}
