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

    public int piercing = 0;
    public int fork = 0;
    public int chain = 0;

    #endregion

    public override void Initialize(GameObject obj)
    {
        launcher = obj.GetComponent<ProjectileShootTriggerable>();
        launcher.projectileForce = projectileForce;
        launcher.projectile = projectile;

        launcher.piercing = piercing;
        launcher.fork = fork;
        launcher.chain = chain;

    }

    public override void TriggerAbility()
    {
        launcher.Launch();
    }
}
