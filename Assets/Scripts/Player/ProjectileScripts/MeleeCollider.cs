using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCollider : MonoBehaviour
{
    CharacterStats characterStats;

    [Header("Primary stats")]
    public float damage;
    public int critChance;

    [Header("Extra stats")]
    public float bleedPercentage;
    public float bleedDuration;
    public float leechAmount;
    public float energyOnHit;

    void Start()
    {
        characterStats = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            DealDamage(other.gameObject);
        }
    }

    public void DealDamage(GameObject target)
    {
       
        target.GetComponent<BasicEnemyFunctions>().TakeDamage(damage, critChance);
        
        EnergyOnHit();

        if (bleedPercentage > 0)
        {
            target.GetComponent<EnemyBleed>().SetBleed(bleedDuration, bleedPercentage);
        }
    }

    public void EnergyOnHit()
    {
        characterStats.currentEnergy += energyOnHit;
    }

}
