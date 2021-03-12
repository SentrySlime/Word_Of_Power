using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    [Header("Life")] 
    public float currentLife;
    public float maxLife;
    float percentBasedLife = 1;
    int addativeLife;
    Slider life_Bar;

    [Header("Energy Barrier")]
    public float currentEnergyBarrier;
    public float maxEnergyBarrier;
    int addativeEnergyBarrier;
    float percentBasedEnergyBarrier = 1;
    Slider energy_Barrier_Bar;

    [Header("Energy")]
    public float maxEnergy;
    public float currentEnergy;
    int addaTiveEnergy;
    float percentBasedEnergy = 1;
    Slider energy_Bar;

    [Header("Leeching")]
    public float leechAmount = 0.01f;
    public float tempAbilityLeech = 0;

    [Header("EXP_Level")]
    public float currentLevel;
    public float currentExp;
    public float expToNextLevel;
    Slider expBar;


    public int AAPoint;
    public float skillPoints;
    float remainderExp;      //The overkill exp transfered between levels

    #region stats numbers
    //Thses stats where chosen as they are the most prevalent, as in these stats can make up for the player is lacking in terms of gear and skills. In other words, if the player is lacking in something 
    //because RNG wasn't kind to them then they have a chance to make up for that themselvs. It's sort of a safety net, plus it feels really good to allocate the skillpoints "enpowering the player".
    [Header("Stats")]
    public float basePower;
    public float baseDefence;               //Increases the flat damage reduction of incoming damage
    public int vitality;              //Increase the max life( this stat is divided by ten then added)
    public float spirit;                //Increase the max energy
    public float speed;                 //Increases the players movement speed
    public int criticalChance;        //Increase the critical hit chance of attacks

    [Header("Final Stats")]
    public float finalArmour = 0;
    public float finalPower = 0;                 //Increases the raw damage output

    [Header("Extra sts")]
    [HideInInspector] public int projectiles = 0;
    [HideInInspector] public float range = 0;
    [HideInInspector] public int pierce = 0;
    [HideInInspector] public int chain = 0;
    [HideInInspector] public float bleedPercentage = 0;
    [HideInInspector] public float bleedDuration = 0;

    public float cooldownModifier = 0;
    #endregion

    #region Bools

    public bool energyLeech;

    #endregion

    #region stat Texts
    //Text Slots for the stat numbers
    Text powerSlot;
    Text defenceSlot;
    Text vitalitySlot;
    Text spiritSlot;
    Text speedSlot;
    Text criticalChanceSlot;
    Text skillPointText;
    #endregion

    #region statButtons
    //Button slots that increase the stat values
    Button powerButton;
    Button defenceButton;
    Button vitalityButton;
    Button spiritButton;
    Button speedButton;
    Button critChanceButton;

    #endregion
    //Other script references
    ManageRandomAbility manageRandomAbility;
    ManageRandomTrait manageRandomTrait;
    PlayerMotor playerMotor;

    #region Calculation Variables

    int addativePower = 0;
    float percentPower = 1;

    int additiveArmour = 0;
    float percentArmour = 1;
    
    #endregion

    public float totArmorReduction;
    public float lifeTaken;
    private void Awake()
    {
        manageRandomAbility = GameObject.Find("RandomizeAbilities").GetComponent<ManageRandomAbility>();
        manageRandomTrait = GameObject.Find("RandomizeAbilities").GetComponent<ManageRandomTrait>();
        playerMotor = GameObject.Find("Player").GetComponent<PlayerMotor>();
    }

    //Start
    void Start()
    {

        #region statBars
        //Sets life, energy and exp bar
        life_Bar = GameObject.FindGameObjectWithTag("life_Bar").GetComponent<Slider>();
        energy_Barrier_Bar = GameObject.FindGameObjectWithTag("Energy_Barrier_Bar").GetComponent<Slider>();
        energy_Bar = GameObject.FindGameObjectWithTag("energy_Bar").GetComponent<Slider>();
        expBar = GameObject.FindGameObjectWithTag("expBar").GetComponent<Slider>();
        expBar.maxValue = expToNextLevel;
        #endregion

        #region statNumbers
        //Sets the text number for the stats
        powerSlot = GameObject.Find("PowerNumber").GetComponent<Text>();
        defenceSlot = GameObject.Find("DefenceNumber").GetComponent<Text>();
        vitalitySlot = GameObject.Find("LifeNumber").GetComponent<Text>();
        spiritSlot = GameObject.Find("EnergyNumber").GetComponent<Text>();
        speedSlot = GameObject.Find("SpeedNumber").GetComponent<Text>();
        defenceSlot = GameObject.Find("DefenceNumber").GetComponent<Text>();
        criticalChanceSlot = GameObject.Find("CriticalChanceNumber").GetComponent<Text>();
        skillPointText = GameObject.Find("SkillpointsNumber").GetComponent<Text>();
        #endregion

        #region statButtons
        //Sets the buttons for the stats
        vitalityButton = GameObject.Find("VitalityButton").GetComponent<Button>();
        vitalityButton.onClick.AddListener(() => StatButton(vitalityButton));

        spiritButton = GameObject.Find("SpiritButton").GetComponent<Button>();
        spiritButton.onClick.AddListener(() => StatButton(spiritButton));


        speedButton = GameObject.Find("SpeedButton").GetComponent<Button>();
        speedButton.onClick.AddListener(() => StatButton(speedButton));

        defenceButton = GameObject.Find("DefenceButton").GetComponent<Button>();
        defenceButton.onClick.AddListener(() => StatButton(defenceButton));

        powerButton = GameObject.Find("PowerButton").GetComponent<Button>();
        powerButton.onClick.AddListener(() => StatButton(powerButton));

        #endregion

        #region statsStart
        //Initialize the stat values
        IncreasePower(0);
        IncreaseDefence(0);
        IncreaseVitality(0);
        IncreaseSpirit(0);
        IncreaseSpeed(0);
        IncreaseCriticalChance(0);
        UpdateSkillPoints();

        //Initializes the life and energy bars
        SetMaxLife();
        ToMaxLife();

        SetMaxEnergy();
        ToMaxEnergy();
        #endregion

        energy_Barrier_Bar.maxValue = maxEnergyBarrier;
        currentEnergyBarrier = maxEnergyBarrier;

    }

    //Update
    void Update()
    {
        life_Bar.value = currentLife;
        energy_Barrier_Bar.value = currentEnergyBarrier;
        energy_Bar.value = currentEnergy;
        expBar.value = currentExp;
        if(currentExp >= expToNextLevel)
        {
            LevelUp();
        }

        if(Input.GetKeyDown(KeyCode.G))
        {
            AddDefence(100, 0);
        }

        if(Input.GetKeyDown(KeyCode.H))
        {
            FinalDefenceReduction(100);
        }
    }

    public void FinalDefenceReduction(float incomingDamage)    //the incoming damage gets the defence bonus applied to it
    {
        totArmorReduction = finalArmour / (finalArmour + 10 * incomingDamage);
        incomingDamage -= (lifeTaken = incomingDamage * totArmorReduction);
        TakeDamage(incomingDamage);                         
    }

    public void TakeDamage(float handledDamage)             //The calculated value then gets sent to be applied to the health
    {
        if(currentEnergyBarrier > 0)
        {
            currentEnergyBarrier -= handledDamage;
            currentEnergyBarrier = Mathf.Clamp(currentEnergyBarrier, 0, maxEnergyBarrier);
        }
        else if(currentEnergyBarrier <= 0)
        {
            currentLife -= handledDamage;
        }
        
        if(currentLife < 0)
        {
            //Die();
        }
    }


    public void Leech(float damage)
    {
        int tempAmount = (int)Mathf.Round(damage * (leechAmount + tempAbilityLeech));
        currentLife += tempAmount;
        currentLife = Mathf.Clamp(currentLife, 0, maxLife);
        //tempAbilityLeech = 0;

        if(energyLeech == true)
        {
            currentEnergy += tempAmount;
            currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
        }

    }

    public void EnergyOnHit(float energy)
    {

        currentEnergy += energy;
        currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);

    }

    public float CooldownModifier(float cooldown)
    {
        float tempNumber = cooldown * cooldownModifier;
        
        cooldown = cooldown - tempNumber;

        return cooldown;
    }

    public void StatButton(Button statButton)
    {



        if(skillPoints >= 1)
        {
            
            skillPoints--;

            if (statButton == spiritButton)
            {
                IncreaseSpirit(1);
            }
            else if (statButton == vitalityButton)
            {
                IncreaseVitality(1);
            }
            else if(statButton == speedButton)
            {
                IncreaseSpeed(1);
            }
            else if(statButton == defenceButton)
            {
                IncreaseDefence(1);
            }
            else if(statButton == powerButton)
            {
                IncreasePower(1);
            }
            UpdateSkillPoints();
        }
    }


    #region Stats 
    public void IncreasePower(float increaseInPower)
    {
        basePower += increaseInPower;
        powerSlot.text = basePower.ToString();
        PowerCalculation();
    }

    public void IncreaseDefence(float increaseInDefence)
    {
        baseDefence += increaseInDefence;
        defenceSlot.text = baseDefence.ToString();
        DefenceCalculation();
    }

    public void IncreaseVitality(int increaseInVitality)          //this method takes a number and divides it by 10 and then adds the percentage to Life (15 = 1.5 = 50%)   (12.5 = 1.25 = 25%)   (10.7 = .07 = 7%) 
    {
        vitality += increaseInVitality;                             //Sets vitality to it's new value
        vitalitySlot.text = vitality.ToString();                    //Sets the value text to the updated value
        IncreaseMaxLife();
    }

    public void IncreaseSpirit(float increaseInSpirit)
    {
        spirit += increaseInSpirit;                                 //Sets vitality to it's new value
        spiritSlot.text = spirit.ToString();                        //Sets the value text to the updated value
        IncreaseMaxEnergy();                             //Increase energy with the updatved spirit value
        IncreaseMaxEB();
    }

    public void IncreaseSpeed(float increaseInSpeed)
    {
        speed += increaseInSpeed;
        speedSlot.text = speed.ToString();
        playerMotor.SetCharacterSpeed();
    }

    public void IncreaseCriticalChance(int increaseInCritChance)
    {
        criticalChance += increaseInCritChance;
        criticalChanceSlot.text = criticalChance.ToString();
    }
    #endregion




    #region LifeSettings
    
    //These are the life settings
    public void AddLife(int plusLife, float percentLife)
    {
        addativeLife += plusLife;
        percentBasedLife += percentLife;
        IncreaseMaxLife();
    }

    public void IncreaseMaxLife()                 //Increase life by a percentage
    {
        maxLife = ((vitality * 5) + addativeLife) * percentBasedLife;
        currentLife = Mathf.Clamp(currentLife, 0, maxLife);
        SetMaxLife();
    }

    public void DecreaseMaxLife(float LifeDecrease)                 //Decreases life by a percentage   (30 = 3 = 300%)    (12 = 1.2 = 20%)    (1.7 = .07 = 7%)  
    {
        maxLife /= LifeDecrease;
        SetMaxLife();
    }

    public void SetMaxLife()
    {
        life_Bar.maxValue = maxLife;
    }

    public void ToMaxLife()
    {
        currentLife = maxLife;
    }
    #endregion

    #region Energy Barrier Settings

    public void AddEnergyBarrier(int plusEB, float percentEB)
    {
        addativeEnergyBarrier += plusEB;
        percentBasedEnergyBarrier += percentEB;
    }

    public void IncreaseMaxEB()
    {
        maxEnergyBarrier = ((spirit *5)+ addativeEnergyBarrier) * percentBasedEnergyBarrier;
        currentEnergyBarrier = Mathf.Clamp(currentEnergyBarrier, 0, maxLife);
        SetMaxEnergyBarrier();
    }

    public void SetMaxEnergyBarrier()
    {
        energy_Barrier_Bar.maxValue = maxEnergyBarrier;
    }

    public void ToMaxEnergyBarrier()
    {
        currentEnergyBarrier = maxEnergyBarrier;
    }
    


    #endregion

    #region EnergySettings
    //These are the energy settings

    public void AddEnergy(int plusEnergy, float percentEnergy)
    {
        addaTiveEnergy += plusEnergy;
        percentBasedEnergy += percentEnergy;
        IncreaseMaxEnergy();
    }

    public void IncreaseMaxEnergy()
    {
        maxEnergy = ((spirit *5)+ addaTiveEnergy) * percentBasedEnergy;
        currentEnergy = Mathf.Clamp(currentEnergy, 0, maxLife);
        SetMaxEnergy();
    }

    public void DecreaseMaxEnergy(float energyDecrease)
    {
        maxEnergy /= energyDecrease;
        SetMaxEnergy();
    }

    public void SetMaxEnergy()
    {
        energy_Bar.maxValue = maxEnergy;
    }

    public void ToMaxEnergy()
    {
        currentEnergy = maxEnergy;
    }

    #endregion

    #region exp&LevelSettings
    public void IncreaseExp(float expReward)
    {
        currentExp += expReward;
    }

    public void SetExpToNextLevel()             //This increases the exp needed for the next level
    {
        if(currentLevel < 6)
        {
            expToNextLevel += 15;               //This scales linearly until level 6 where it stops increasing
        }
        expBar.maxValue = expToNextLevel;
    }                                           //This is so the player can get some quick levels early and stear into a build
                                                //So you will feel like you are making progress right of the bat
    public void LevelUp()
    {

        remainderExp = currentExp - expToNextLevel; //Calculates the overkill exp
        currentExp = 0;                             //sets current exp to next levels 0 exp
        IncreaseExp(remainderExp);                  //Increases the next levels current exp with the remaining exp from the last level
        SetExpToNextLevel();                        //Increases the amount of exp needed for the next level
        remainderExp = 0;                           //Then sets the remainder exp to 0 again

        currentLevel++;                             //Increases the level

        ToMaxLife();
        ToMaxEnergy();
        ToMaxEnergyBarrier();

        skillPoints += 3;
        AAPoint++;
        UpdateSkillPoints();
        EnableAARandomizer();
    }
    #endregion

    public void EnableAARandomizer()
    {
        if(AAPoint > 0)
        {
            manageRandomAbility.randomizeAbilities.gameObject.SetActive(true);
            manageRandomTrait.randomizeTrait.gameObject.SetActive(true);
        }

    }

    public void UpdateSkillPoints()
    {
        skillPointText.text = skillPoints.ToString();
    }

    #region Calculate Stats

    #region Defence
    public void AddDefence(int flatIncrease, float percentIncrease)
    {
        additiveArmour += flatIncrease;
        percentArmour += percentIncrease;
        DefenceCalculation();
    }

    public void RemoveDefence(int flatDecrease, float percentDecrease)
    {
        additiveArmour -= flatDecrease;
        percentArmour -= percentDecrease;
        DefenceCalculation();
    }

    public void DefenceCalculation()
    {
        finalArmour = ((baseDefence * 40) + additiveArmour) * percentArmour;
        PowerCalculation();

    }

    #endregion


    #region Power


    public void AddPower(int flatIncrease, float percentIncrease)
    {
        addativePower += flatIncrease;
        percentPower += percentIncrease;
        PowerCalculation();
    }

    public void PowerCalculation()
    {
        finalPower = ((basePower * 5) + addativePower) * percentPower;
        //finalPower = ((basePower * 5) + (finalArmour / 10) + addativePower) * percentPower;

        //if(defenceBasedPower == true)
        //{


        //}

    }

    #endregion

    #endregion

}
