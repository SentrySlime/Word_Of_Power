using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Abilities/ProjectileAbility")]
public class ProjectileAbility : Ability
{
    public Rigidbody projectile;
    private ProjectileShootTriggerable launcher;
    CharacterStats characterStats;


    #region Projectile Properties
    [Header("Primary stats")]
    public float damage = 20;
    public int critChance = 1;
    public int piercing = 0;
    public int chainMax = 0;
    public int Projectiles = 1;
    #endregion

    [Header("Extra stats")]
    public float bleedPercentage = 0;
    public float bleedDuration = 0;
    public float leechAmount = 0;
    public float projectileForce = 500f;

    private void Awake()
    {
    }

    public override void Initialize(GameObject obj)
    {
        characterStats = GameObject.Find("Player").GetComponent<CharacterStats>();
        launcher = obj.GetComponent<ProjectileShootTriggerable>();
        launcher.projectile = projectile;
        launcher.piercing = piercing + characterStats.pierce;
        launcher.chain = chainMax + characterStats.chain;
        launcher.Projectiles = Projectiles + characterStats.projectiles;
        launcher.damage = damage + characterStats.power;
        launcher.criticalChance = critChance + characterStats.criticalChance;

        launcher.bleedPercentage = bleedPercentage + characterStats.bleedPercentage;
        launcher.bleedDuration = bleedDuration + characterStats.bleedDuration;
        characterStats.tempAbilityLeech = leechAmount;

        launcher.projectileForce = projectileForce;
    }

    public override void TriggerAbility()
    {
        launcher.Launch();
    }
}
