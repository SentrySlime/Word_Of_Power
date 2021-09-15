using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Ranged_Attack : MonoBehaviour
{

    Enemy_Movement enemy_Movement;
    EnemyMotor enemyMotor;

    public LayerMask layerMask;

    public bool isAttacking;
    public GameObject attackPrefab;

    public float damage;
    public float attackBuildUp;

    public float rayRange;


    void Start()
    {
        enemy_Movement = GetComponent<Enemy_Movement>();
        enemyMotor = GetComponent<EnemyMotor>();

    }


    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * rayRange);

        if (Physics.Raycast(transform.position, transform.forward * rayRange, layerMask))
        {
            if (enemy_Movement.distance <= 4 && isAttacking == false)
            {
                StartCoroutine(AttackBuildUp());
                isAttacking = true;
            }
        }
    }
    
    public void Attack()
    {

    }

    IEnumerator AttackBuildUp()
    {
        yield return new WaitForSeconds(attackBuildUp);

        Attack();
    }

}
