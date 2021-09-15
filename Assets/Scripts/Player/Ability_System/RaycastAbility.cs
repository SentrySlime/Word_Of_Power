using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu (menuName = "Abilities/RaycastAbility")]
public class RaycastAbility : Ability
{
    #region hidy stats
    public float hitForce = 100f;
    public float sphereCastSize = 1f;

    public Material rayMaterial;
    private RaycastShootTriggerable rcShoot;
    private CharacterStats characterStats;
    #endregion

    #region Basic Stats
    [Header("Primary stats")]
    public float damage = 1;
    public int critChance = 1;
    public float rayRange = 50f;
    public int piercemax = 1;
    public int chain = 1;
    public int projectiles = 1;
    #endregion

    

    #region Extra stats
    [Header("Extra stats")]
    public float bleedPercent = 0;
    public float bleedDuration = 0;
    public float leechAmount = 0;

    public float energyOnHit = 0;
    #endregion

    #region Angles
    [Header("Angles")]
    public float endAngle = 45;
    public float startAngle = -45;
    #endregion


    public string attackAnimation;
    public float attackTimer;

    private void Awake()
    {

    }

    public override void Initialize(GameObject obj)
    {
        characterStats = GameObject.Find("Player").GetComponent<CharacterStats>();

        rcShoot = obj.GetComponent<RaycastShootTriggerable>();
        rcShoot.Initialize();

        rcShoot.animationTimer = attackTimer - (attackTimer * characterStats.attackTimer);
        rcShoot.attackAnimation = attackAnimation;
        rcShoot.damage = damage + characterStats.finalPower;
        rcShoot.criticalChance = critChance + characterStats.criticalChance;
        rcShoot.rayRange = rayRange + characterStats.range;
        rcShoot.pierceMax = piercemax + characterStats.pierce;
        rcShoot.chainNumbers = chain + characterStats.chain;
        rcShoot.Projectiles = projectiles + characterStats.projectiles;
        rcShoot.bleedPercentage = bleedPercent + characterStats.bleedPercentage;
        rcShoot.bleedDuration = bleedDuration + characterStats.bleedDuration;
        characterStats.tempAbilityLeech = leechAmount;

        rcShoot.sphereRadius = sphereCastSize;

        rcShoot.startingAngle = startAngle;
        rcShoot.endAngle = endAngle;

        //rcShoot.hitForce = hitForce;
        //rcShoot.lineRenderer.material = rayMaterial;
        //rcShoot.lineRenderer.material.color = rayColor;

    }

    public override void TriggerAbility()
    {
        rcShoot.Fire();
    }

}
