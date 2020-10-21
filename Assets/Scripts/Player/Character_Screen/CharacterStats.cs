using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    [Header("Life")]
    public float currentLife;
    public float maxLife;
    Slider life_Bar;

    [Header("Energy")]
    public float currentEnergy;
    public float maxEnergy;
    Slider energy_Bar;

    [Header("EXP_Level")]
    public float currentLevel;
    public float currentExp;
    public float expToNextLevel;
    Slider expBar;

    float remainderExp;      //The overkill exp transfered between levels

    //Thses stats where chosen as they are the most prevalent, as in these stats can make up for the player is lacking in terms of gear and skills. In other words, if the player is lacking in something 
    //because RNG wasn't kind to them then they have a chance to make up for that themselvs. It's sort of a safety net, plus it feels really good to allocate the skillpoints "enpowering the player".
    [Header("Stats")]
    public float power;                 //Increases the raw damage output
    public float defence;               //Increases the flat damage reduction of incoming damage
    public float vitality;              //Increase the max life( this stat is divided by ten then added)
    public float spirit;                //Increase the max energy
    public float speed;                 //Increases the players movement speed
    public float criticalChance;        //Increase the critical hit chance of attacks

    //Text Slots for the stat numbers
    Text powerSlot;
    Text defenceSlot;
    Text vitalitySlot;
    Text spiritSlot;
    Text speedSlot;
    Text criticalChanceSlot;

    //Button slots that increase the stat values
    Button powerButton;
    Button defenceButton;
    Button vitalityButton;
    Button spiritButton;
    Button speedButton;
    Button critChanceButton;

    void Start()
    {


        #region statBars
        //Sets life, energy and exp bar
        life_Bar = GameObject.FindGameObjectWithTag("life_Bar").GetComponent<Slider>();
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
        criticalChanceSlot = GameObject.Find("CriticalChanceNumber").GetComponent<Text>();
        #endregion

        #region statButtons
        //Sets the buttons for the stats
        vitalityButton = GameObject.Find("VitalityButton").GetComponent<Button>();
        vitalityButton.onClick.AddListener(() => IncreaseVitality(1));

        spiritButton = GameObject.Find("SpiritButton").GetComponent<Button>();
        spiritButton.onClick.AddListener(() => IncreaseSpirit(1));

        #endregion

        #region statsStart
        //Initialize the stat values
        IncreasePower(0);
        IncreaseDefence(0);
        IncreaseVitality(15);
        IncreaseSpirit(15);
        IncreaseSpeed(0);
        IncreaseCriticalChance(0);

        //Initializes the life and energy bars
        SetMaxLife();
        ToMaxLife();

        SetMaxEnergy();
        ToMaxEnergy();
        #endregion
    }

    void Update()
    {

        life_Bar.value = currentLife;
        energy_Bar.value = currentEnergy;
        expBar.value = currentExp;

        if(currentExp >= expToNextLevel)
        {
            LevelUp();
        }

        if(Input.GetKeyDown(KeyCode.I))
        {
            IncreaseExp(7);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            //EquipingAbility1();
        }
    }

    public void DefenceCalculation(float incomingDamage)    //the incoming damage gets the defence bonus applied to it
    {
        incomingDamage -= defence;

        TakeDamage(incomingDamage);                         
    }

    public void TakeDamage(float handledDamage)             //The calculated value then gets sent to be applied to the health
    {
        currentLife -= handledDamage;
    }


    #region Stats 
    //close region here
    public void IncreasePower(float increaseInPower)
    {
        power += increaseInPower;
        powerSlot.text = power.ToString();
    }

    public void IncreaseDefence(float increaseInDefence)
    {
        defence += increaseInDefence;
        defenceSlot.text = defence.ToString();
    }

    
    
    //Vitality needs to access life settings
    public void IncreaseVitality(float increaseInVitality)          //this method takes a number and divides it by 10 and then adds the percentage to Life (15 = 1.5 = 50%)   (12.5 = 1.25 = 25%)   (10.7 = .07 = 7%) 
    {
        if (vitality != 0)                                          //If vitality is zero don't decrease it
        {
            DecreaseMaxLife(vitality/ 10);                          //Removes the previous amount of the vitality bonus
        }

        vitality += increaseInVitality;                             //Sets vitality to it's new value
        vitalitySlot.text = vitality.ToString();                    //Sets the value text to the updated value
        IncreaseMaxLife(vitality / 10);                             //Increase life with the updatved vitality value
    }
    
    //Spirit needs to access energy settings
    public void IncreaseSpirit(float increaseInSpirit)
    {
        if (spirit != 0)                                            //If spirit is zero don't decrease it
        {
            DecreaseMaxEnergy(spirit / 10);                         //Removes the previous amount of the spirit bonus
        }

        spirit += increaseInSpirit;                                 //Sets vitality to it's new value
        spiritSlot.text = spirit.ToString();                        //Sets the value text to the updated value
        IncreaseMaxEnergy(spirit / 10);                             //Increase energy with the updatved spirit value
    }

    public void IncreaseSpeed(float increaseInSpeed)
    {
        speed += increaseInSpeed;
        speedSlot.text = speed.ToString();
    }

    public void IncreaseCriticalChance(float increaseInCritChance)
    {
        criticalChance += increaseInCritChance;
        criticalChanceSlot.text = criticalChance.ToString();
    }
    #endregion

    #region LifeSettings
    //These are the life settings
    public void IncreaseMaxLife(float LifeIncrease)                 //Increase life by a percentage
    {
        maxLife *= LifeIncrease;
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

    #region EnergySettings
    //These are the energy settings
    public void IncreaseMaxEnergy(float energyIncrease)
    {
        maxEnergy *= energyIncrease;
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
    }
    #endregion

}
