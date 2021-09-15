using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShootTriggerable : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody projectile;
    public Transform bulletSpawn;
    public CharacterStats characterStats;
    [HideInInspector]

    #region Projectile properties

    public float damage;
    public int criticalChance = 1;
    public int Projectiles = 1;
    public int piercing = 0;
    public int fork = 0;
    public int chain = 0;
    public float projectileForce = 250f;

    public float bleedPercentage = 0;
    public float bleedDuration = 0;

    public float energyOnHit = 0;
    #endregion

    #region Multiple projectile angles

    public float endAngle = 45f;
    public float startingAngle = -45f;
    float fullAngle;
    float currentAngle;

    #endregion

    Animator animator;
    public float animationTimer;
    public string attackAnimation;

    public void Launch()
    {
        animator = GetComponentInChildren<Animator>();
        animator.SetTrigger(attackAnimation);
        StartCoroutine(AttackTimer());
        //StartCoroutine(TimerTimer());
        //TurnDegrees();
    }

    private void Update()
    {
        
        
    }

    public void TurnDegrees()
    {

        fullAngle = endAngle - startingAngle;

        fullAngle /= (1 + Projectiles);

        currentAngle = startingAngle;

        for (int i = 0; i < Projectiles; i++)
        {
            currentAngle = currentAngle + fullAngle;

            bulletSpawn.transform.localRotation = Quaternion.Euler(0, currentAngle, 0);

            ProjectileScript bob = Instantiate(projectile, bulletSpawn.transform.position, Quaternion.identity).GetComponent<ProjectileScript>();

            bob.characterStats = characterStats;

            bob.damage = damage;
            bob.criticalChance = criticalChance;
            bob.pierceMax = piercing;
            bob.chainNumbers = chain;

            bob.bleedPercentage = bleedPercentage;
            bob.bleedDuration = bleedDuration;

            bob.energyOnHit = energyOnHit;
            bob.moveSpeed = projectileForce;
            bob.GetComponent<Rigidbody>().AddForce(bulletSpawn.transform.forward * projectileForce, ForceMode.Force);
        }

        bulletSpawn.transform.localRotation= Quaternion.Euler(0, 0, 0);


    }

    IEnumerator AttackTimer()
    {
        yield return new WaitForSeconds(animationTimer);
        
        TurnDegrees();
    }

    public void StopCoRoutines()
    {
        StopAllCoroutines();
    }

    IEnumerator TimerTimer()
    {
        yield return new WaitForSeconds(0.35f);
        if (animator != null)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("arthur_walk_01"))
            {
                print("Stopping all couroutines");
                StopAllCoroutines();
            }
        }
    }

}
