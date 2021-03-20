using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BasicEnemyFunctions : MonoBehaviour, IEnemy
{


    [SerializeField] private float moveSpeed = 2f;
    public float currentHealth;
    public float maxHealth;
    public int expReward = 10;
    public float damage = 5;

    public GameObject[] abilityCooldownArray;

    CharacterStats characterStats;
    int critMax = 1;
    int critMax2 = 101;


    void Start()
    {
        currentHealth = maxHealth;
        characterStats = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>();
        abilityCooldownArray = GameObject.FindGameObjectsWithTag("AbilityCooldown");
    }

    void Update()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
        transform.Rotate(0f, -.5f, 0f);

    }

    public event Action<float> OnHealthChanged = delegate { };

    public void PerformAttack()
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(float amount, int critChance) 
    {

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

    //public void DealDamage()
    //{
    //    characterStats.DefenceCalculation(damage);
    //}

    public void Die()
    {

        characterStats.IncreaseExp(expReward);
        if(gameObject != null)
        {
            Destroy(gameObject);
        }
    }
}
