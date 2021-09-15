using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Melee_Attack : MonoBehaviour
{
    public Animator enemyAttackAnimator;

    Enemy_Movement enemy_Movement;
    EnemyMotor enemyMotor;
    EnemySoundPlayer enemySoundPlayer;
    public LayerMask layerMask;

    public bool isAttacking;
    public GameObject attackPrefab;

    public float damage;
    public float attackBuildUp;
    public float rayRange;

    public float attackTimer;
    public float currentTimer;

    void Start()
    {
        enemy_Movement = GetComponent<Enemy_Movement>();
        enemyMotor = GetComponent<EnemyMotor>();
        attackPrefab.SetActive(false);
        enemySoundPlayer = GetComponent<EnemySoundPlayer>();
    }

    
    void Update()
    {
        if(currentTimer < attackTimer)
        {
            currentTimer += Time.deltaTime;
        }

        Debug.DrawRay(transform.position, transform.forward * rayRange);

        if(Physics.Raycast(transform.position, transform.forward * rayRange, layerMask))
        {
            if (enemy_Movement.distance <= 4 && isAttacking == false && currentTimer >= attackTimer)
            {
                currentTimer = 0;
                enemy_Movement.isWalking = false;
                enemySoundPlayer.StopWalkSound();
                enemyAttackAnimator.SetBool("isWalking", false);
                StartCoroutine(AttackBuildUp());
                isAttacking = true;
            }
        }
    }

    public void Attack()
    {
        //GameObject tempFab = Instantiate(attackPrefab, attackStart);
        attackPrefab.SetActive(true);
        StartCoroutine(DisableAttack());
    }

    IEnumerator AttackBuildUp()
    {
        
        enemyMotor.enemyAgent.isStopped = true;
        enemyAttackAnimator.SetTrigger("attack");

        yield return new WaitForSeconds(attackBuildUp);

        Attack();
    }

    IEnumerator DisableAttack()
    {
        yield return new WaitForSeconds(.1f);
        isAttacking = false;
        attackPrefab.SetActive(false);
        enemyMotor.enemyAgent.isStopped = false;
    }

}
