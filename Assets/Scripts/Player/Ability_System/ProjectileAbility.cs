using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Abilities/ProjectileAbility")]
public class ProjectileAbility : Ability
{
    public float projectileForce = 500f;

    public Rigidbody projectile;

    private ProjectileShootTriggerable launcher;

    #region Projectile Properties

    public int damage = 20;
    public int piercing = 0;
    public int forkMax = 0;
    public int chainMax = 0;

    public int Projectiles = 1;
    #endregion

    
    public override void Initialize(GameObject obj)
    {
        launcher = obj.GetComponent<ProjectileShootTriggerable>();
        launcher.projectileForce = projectileForce;
        launcher.projectile = projectile;

        launcher.piercing = piercing;
        launcher.fork = forkMax;
        launcher.chain = chainMax;
        launcher.Projectiles = Projectiles;
        launcher.damage = damage;
    }

    public override void TriggerAbility()
    {
        launcher.Launch();
    }
}
