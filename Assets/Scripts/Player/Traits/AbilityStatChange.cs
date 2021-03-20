﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityStatChange : MonoBehaviour
{

    [Header("HP")]
    public int HPflat;
    public float HPpercent;

    [Header("MP")]
    public int MPflat;
    public float MPpercent;

    [Header("EB")]
    public int EBflat;
    public float EBpercent;

    [Header("Defence")]
    public int defenceFlat;
    public float defencePercent;

    [Header("Power")]
    public int flatPower;
    public float percentPower;


    [Header("Primary Stats")]
    public float rayRange;
    public float cooldown;
    public int critChance;

    [Header("Extra Stats")]
    public int piercemax;
    public int chain;
    public int projectile;

    [Header("Leech Stat")]
    public float addativeLeech;
    public float multiplicativeLeech;


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
        IncreasePower();
        characterStats.AddRange(rayRange);
        characterStats.AddCritChance(critChance);
        characterStats.AddPierce(piercemax);
        characterStats.AddChain(chain);
        characterStats.AddProjectiles(projectile);
        characterStats.AddLeech(addativeLeech, multiplicativeLeech);
        Cooldown(cooldown);
    }


    public void HPIncrease()
    {
        characterStats.AddLife(HPflat, HPpercent);
        characterStats.SetMaxLife();
    }

    public void MPIncrease()
    {
        characterStats.AddEnergy(MPflat, MPpercent);
        characterStats.SetMaxEnergy();
    }

    public void EBIncrease()
    {
        characterStats.AddEnergyBarrier(EBflat, EBpercent);
        characterStats.SetMaxEnergyBarrier();
    }
    
    public void IncreaseDefence()
    {
        characterStats.AddDefence(defenceFlat, defencePercent);
    }

    public void IncreasePower()
    {
        characterStats.AddPower(flatPower, percentPower);

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