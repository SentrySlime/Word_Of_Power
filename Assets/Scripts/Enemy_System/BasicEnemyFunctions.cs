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

    [Header("Status Effects")]
    public bool isBleeding = false;

    [SerializeField] CharacterStats characterStats;



    void Start()
    {
        currentHealth = maxHealth;
        characterStats = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>();
    }

    void Update()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
        transform.Rotate(0f, -.5f, 0f);

        if (Input.GetKeyDown(KeyCode.U))
        {
            TakeDamage(3);
        }
    }

    public event Action<float> OnHealthChanged = delegate { };

    public void PerformAttack()
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        float currentHealthPercent = (float)currentHealth / (float)maxHealth;
        OnHealthChanged(currentHealthPercent);
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
