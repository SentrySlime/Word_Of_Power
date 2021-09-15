using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    public Animator enemyMoveAnim;
    public bool isWalking;

    public Transform playerPosition;
    Vector3 playerDir;
    public float distance;
    EnemyMotor enemyMotor;
    Detection_and_Awareness detection_Awareness;
    Enemy_Melee_Attack enemyMeleeAttack;
    Enemy_Ranged_Attack enemyRangedAttack;
    EnemySoundPlayer enemySoundPlayer;

    float currentTimer;
    public float maxTimer;


    [Header("Agent Parameters")]
    public float movementSpeed;
    public int stopDistance;
    public float speed;
    void Start()
    {
        enemyMotor = GetComponent<EnemyMotor>();
        enemyMotor.enemyAgent.speed = movementSpeed;
        enemyMotor.enemyAgent.stoppingDistance = stopDistance;
        detection_Awareness = GetComponentInChildren<Detection_and_Awareness>();
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        enemyMeleeAttack = GetComponent<Enemy_Melee_Attack>();
        enemyRangedAttack = GetComponent<Enemy_Ranged_Attack>();
        enemySoundPlayer = GetComponent<EnemySoundPlayer>();
    }

    
    void Update()
    {
        currentTimer += Time.deltaTime;

        Debug.DrawRay(transform.position, playerDir, Color.red);

        distance = Vector3.Distance(transform.position, playerPosition.position);
        float singleStep = speed * Time.deltaTime;

        playerDir = (playerPosition.position - transform.position);
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, playerDir, singleStep, 0.0f);

        if (currentTimer >= maxTimer && distance > 2 && enemyMeleeAttack.isAttacking == false & detection_Awareness.aware == true)
        {
            currentTimer = 0;
            Destination();
        }
        else if(currentTimer >= maxTimer && distance < 4 && enemyMeleeAttack.isAttacking == false && detection_Awareness.aware == true)
        {
            Vector3 tempPos = new Vector3(playerPosition.position.x, 0, playerPosition.position.z);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }

    }

    public void Destination()
    {

        
        enemyMotor.EnemyMove(playerPosition.position);

        enemyMoveAnim.SetBool("isWalking", true);
        isWalking = true;
        enemySoundPlayer.PlayWalkSound();

    }


}
