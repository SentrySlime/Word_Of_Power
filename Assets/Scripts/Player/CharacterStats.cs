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

        life_Bar.maxValue = maxLife;
        energy_Bar.maxValue = maxEnergy;
        expBar.maxValue = expToNextLevel;

    }

    
    void Update()
    {

        life_Bar.value = currentLife;
        energy_Bar.value = currentEnergy;
        expBar.value = currentExp;

        if(Input.GetKeyDown(KeyCode.U))
        {
            IncreaseExp(1f);
        }
    }




    public void IncreaseExp(float  expReward)
    {
        currentExp += expReward;
    }

    public void LevelUp()
    {
        ToMaxLife();
        ToMaxEnergy();
    }

    public void TakeDamage(float incomingDamage)
    {

    }

    public void ToMaxLife()
    {
        currentLife = maxLife;
    }

    public void ToMaxEnergy()
    {
        currentEnergy = maxEnergy;
    }

}
