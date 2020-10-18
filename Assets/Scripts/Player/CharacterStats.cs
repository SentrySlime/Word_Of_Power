using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    [Header("Life")]
    public float currentLife;
    public float maxLife;
    public Slider life_Bar;

    [Header("Energy")]
    public float currentEnergy;
    public float maxEnergy;
    public Slider energy_Bar;

    [Header("EXP_Level")]
    public float currentLevel;
    public float currentExp;
    public float expToNextLevel;
    public Slider expBar;

    public float remainderExp;

    [Header("Stats")]
    public float power;
    public float defence;
    public float speed;

    void Start()
    {
        life_Bar = GameObject.FindGameObjectWithTag("life_Bar").GetComponent<Slider>();
        energy_Bar = GameObject.FindGameObjectWithTag("energy_Bar").GetComponent<Slider>();
        expBar = GameObject.FindGameObjectWithTag("expBar").GetComponent<Slider>();

        ToMaxLife();
        ToMaxEnergy();

        SetMaxLife();
        SetMaxEnergy();

        expBar.maxValue = expToNextLevel;

    }
    
    void Update()
    {

        life_Bar.value = currentLife;
        energy_Bar.value = currentEnergy;
        expBar.value = currentExp;

        if(Input.GetKeyDown(KeyCode.U))
        {
            IncreaseExp(10f);
        }

        if(currentExp >= expToNextLevel)
        {
            LevelUp();
        }
    }

    public void TakeDamage(float incomingDamage)
    {
        incomingDamage -= defence;
        currentLife -= incomingDamage;
    }

    #region LifeSettings
    public void IncreaseMaxLife(float LifeIncrease)
    {
        maxLife += LifeIncrease;
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
    public void IncreaseMaxEnergy(float EnergyIncrease)
    {
        maxEnergy += EnergyIncrease;
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

    public void IncreaseExp(float expReward)
    {
        currentExp += expReward;

    }

    public void LevelUp()
    {

        remainderExp = currentExp - expToNextLevel; //Calculates the overkill exp
        currentExp = 0;                             //sets current exp to next levels 0 exp
        IncreaseExp(remainderExp);                  //Increases the next levels current exp with the remaining exp from the last level
        remainderExp = 0;                           //Then sets the remainder exp to 0 again

        currentLevel++;                             //Increases the level

        ToMaxLife();
        ToMaxEnergy();
    }

}
