using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu (menuName = "Abilities/RaycastAbility")]
public class RaycastAbility : Ability
{
    public float rayDamage = 1;
    public float rayRange = 50f;
    public int piercemax = 1;
    public int chain = 0;
    public int projectiles = 1;

    public float hitForce = 100f;
    public float sphereCastSize = 1f;

    public Material rayMaterial;

    private RaycastShootTriggerable rcShoot;

    public override void Initialize(GameObject obj)
    {
        rcShoot = obj.GetComponent<RaycastShootTriggerable>();
        rcShoot.Initialize();

        rcShoot.rayDamage = rayDamage;
        rcShoot.rayRange = rayRange;
        rcShoot.pierceMax = piercemax;
        rcShoot.chainNumbers = chain;
        rcShoot.Projectiles = projectiles;

        rcShoot.sphereRadius = sphereCastSize;
        //rcShoot.hitForce = hitForce;
        //rcShoot.lineRenderer.material = rayMaterial;
        //rcShoot.lineRenderer.material.color = rayColor;


    }

    public override void TriggerAbility()
    {
        rcShoot.Fire();
    }

}
