using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityStatChange : MonoBehaviour
{

    [Header("Character_Stats")]
    public float HP;
    public float MP;
    public float EB;
    public float defence;

    [Header("Primary Stats")]
    public float damage;
    public float rayRange;
    public float cooldown;
    public int critChance;

    [Header("Extra Stats")]
    public int piercemax;
    public int chain;
    public int projectile;
    public float leechAmount;

    CharacterStats characterStats;

    private void Awake()
    {
        characterStats = GameObject.Find("Player").GetComponent<CharacterStats>();

    }


    public void ChangeNumbers()
    {
        HPIncrease();
        MPIncrease();
        EBIncrease();
        IncreaseDefence();
        characterStats.power += damage;
        characterStats.range += rayRange;
        characterStats.criticalChance += critChance;
        characterStats.pierce += piercemax;
        characterStats.chain += chain;
        characterStats.projectiles += projectile;
        characterStats.leechAmount += leechAmount;
        Cooldown(cooldown);
    }


    public void HPIncrease()
    {
        characterStats.maxLife += HP;
        characterStats.SetMaxLife();
    }

    public void MPIncrease()
    {
        characterStats.maxEnergy += MP;
        characterStats.SetMaxEnergy(); ;
    }

    public void EBIncrease()
    {
        characterStats.maxEnergyBarrier += EB;
        characterStats.SetMaxEnergyBarrier();
    }
    
    public void IncreaseDefence()
    {
        characterStats.AddDefence(defence, 0);
    }

    public void ChangeChain(int chain)
    {

    }
    public void ChangeProjectileAmount(int projeciles)
    {

    }
    public void Cooldown(float cooldown)
    {
        characterStats.cooldownModifier += cooldown;
    }
}