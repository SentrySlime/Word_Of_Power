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

    public void Launch()
    {
        Rigidbody clonedbullet = Instantiate(projectile, bulletSpawn.position, transform.rotation) as Rigidbody;

        clonedbullet.AddForce(bulletSpawn.transform.forward * projectileForce);
    }

}
