using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShootTriggerable : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody projectile;

    public Transform bulletSpawn;

    [HideInInspector]
    public float projectileForce = 250f;

    #region Projectile properties

    public int piercing = 0;
    public int fork = 0;
    public int chain = 0;
    #endregion

    #region Multiple projectile angles

    public int damage;
    public int Projectiles = 1;
    float endAngle = 45f;
    float startingAngle = -45f;
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

            var bob = Instantiate(projectile, bulletSpawn.transform.position, Quaternion.identity);

            bob.GetComponent<ProjectileScript>().damage = damage;
            bob.GetComponent<ProjectileScript>().pierceMax = piercing;
            bob.GetComponent<ProjectileScript>().chainNumbers = chain;
            bob.GetComponent<Rigidbody>().AddForce(bulletSpawn.transform.forward * projectileForce, ForceMode.Force);
        }

        bulletSpawn.transform.rotation = Quaternion.Euler(0, 0, 0);


    }

}
