using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class BasicEnemyFunctions : MonoBehaviour, IEnemy
{

    public float currentHealth;
    public float maxHealth;
    public int expReward = 10;
    public float damage = 5;

    public GameObject[] abilityCooldownArray;

    CharacterStats characterStats;
    int critMax = 1;
    int critMax2 = 101;

    Detection_and_Awareness detectionAwareness;
    public OnDeathEffect onDeathScript;
    public GenerateLoot generateLoot;

    void Start()
    {
        currentHealth = maxHealth;
        characterStats = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>();
        generateLoot = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GenerateLoot>();
        abilityCooldownArray = GameObject.FindGameObjectsWithTag("AbilityCooldown");
        detectionAwareness = GetComponentInChildren<Detection_and_Awareness>();
        onDeathScript = GetComponent<OnDeathEffect>();
    }

    void Update()
    {
        
    }

    public event Action<float> OnHealthChanged = delegate { };

    public void PerformAttack()
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(float amount, int critChance) 
    {
        if(detectionAwareness != null)
            detectionAwareness.aware = true;
        if(critChance <= 100)
        {
            critMax = UnityEngine.Random.Range(1, 101);
            if (critChance >= critMax)
            {
                print("Crit");
                CriticalHitDamage2x(amount);
            }
            else
            {
                CauseDamage(amount);
            }
        }
        else if(critChance > 100)
        {
            critMax2 = UnityEngine.Random.Range(101, 200);
            if (critChance >= critMax2)
            {
               CriticalHitDamage3x(amount);
            }
            else
            {
               CriticalHitDamage2x(amount);
            }
        }
    }

    public void CriticalHitDamage2x(float damage)
    {
        
        damage *= 2;
        CauseDamage(damage);
    }

    public void CriticalHitDamage3x(float damage)
    {
        damage *= 3;

    }

    public void CauseDamage(float amount)
    {
        currentHealth -= amount;
        float currentHealthPercent = (float)currentHealth / (float)maxHealth;
        OnHealthChanged(currentHealthPercent);
        
        characterStats.Leech(amount);

        for (int i = 0; i < abilityCooldownArray.Length; i++)
        {
            abilityCooldownArray[i].GetComponent<AbilityCooldown>().currentDamage += amount;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        generateLoot.DropLoot(transform);
        characterStats.IncreaseExp(expReward);
        if(gameObject != null)
        {
            onDeathScript.SpawnBones();
            Destroy(gameObject);
        }
    }

    public void OnInstantiate()
    {
        currentHealth = maxHealth;
        characterStats = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>();
        generateLoot = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GenerateLoot>();
        abilityCooldownArray = GameObject.FindGameObjectsWithTag("AbilityCooldown");
        detectionAwareness = GetComponentInChildren<Detection_and_Awareness>();
    }
    

}
