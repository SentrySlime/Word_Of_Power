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

    

    public void Launch()
    {
        Rigidbody clonedbullet = Instantiate(projectile, bulletSpawn.position, transform.rotation) as Rigidbody;

        clonedbullet.AddForce(bulletSpawn.transform.forward * projectileForce);
    }

}
