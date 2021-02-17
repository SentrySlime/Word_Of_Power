using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/MeleeAbility")]
public class MeleeAbilities : Ability
{
    public Collider attackCollider;
    public Transform attackPos;
    CharacterStats characterStats;

    public MeleeTriggerable meleeTrigger;

    Ability abilityNumbers;

    [Header("Primary stats")]
    public float damage = 0;
    public int critChance = 1;
    public float range;

    [Header("Extra stats")]
    public float bleedPercentage = 0;
    public float bleedDuration = 0;
    public float leechAmount = 0;
    public float energyOnHit = 0;

    public override void Initialize(GameObject obj)
    {
        if(characterStats == null)
        {
            characterStats = GameObject.Find("Player").GetComponent<CharacterStats>();
        }

        range = characterStats.range;
        //meleeTrigger.range = range;

        meleeTrigger = obj.GetComponent<MeleeTriggerable>();

        meleeTrigger.attackCollider = attackCollider;
        meleeTrigger.damage = damage;
        meleeTrigger.critChance = critChance;

        meleeTrigger.bleedPercentage = bleedPercentage;
        meleeTrigger.bleedDuration = bleedDuration;
        meleeTrigger.leechAmount = leechAmount;
        meleeTrigger.energyOnHit = energyOnHit;

    }

    public override void TriggerAbility()
    {
        meleeTrigger.Hit();
    }

}
