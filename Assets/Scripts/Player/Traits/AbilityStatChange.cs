using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityStatChange : MonoBehaviour
{



    public float damage;
    public float rayRange;
    public float cooldown;
    public int critChance;
    public int piercemax;
    public int chain;
    public int projectile;

    CharacterStats characterStats;

    private void Awake()
    {
        characterStats = GameObject.Find("Player").GetComponent<CharacterStats>();

    }


    public void ChangeNumbers()
    {
        characterStats.power += damage;
        characterStats.range += rayRange;
        characterStats.criticalChance += critChance;
        characterStats.pierce += piercemax;
        characterStats.chain += chain;
        characterStats.projectiles = projectile;
    }


    public void ChangeDamage(float damage)
    {
        
    }

    public void ChangeCritChance(int critChance)
    {

    }
    public void ChangeRange(float range)
    {

    }
    public void ChangePierce(int pierce)
    {

    }
    public void ChangeChain(int chain)
    {

    }
    public void ChangeProjectileAmount(int projeciles)
    {

    }
    public void Cooldown(float cooldown)
    {

    }
}