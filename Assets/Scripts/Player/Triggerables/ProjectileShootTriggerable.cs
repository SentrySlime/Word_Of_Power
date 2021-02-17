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


    public void Launch()
    {

        TurnDegrees();
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

            bob.GetComponent<Rigidbody>().AddForce(bulletSpawn.transform.forward * projectileForce, ForceMode.Force);
        }

        bulletSpawn.transform.localRotation= Quaternion.Euler(0, 0, 0);


    }

}
