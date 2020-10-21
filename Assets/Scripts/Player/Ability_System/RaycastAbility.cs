﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu (menuName = "Abilities/RaycastAbility")]
public class RaycastAbility : Ability
{
    public float rayDamage = 1;
    public float rayRange = 50f;
    public float hitForce = 100f;

    public Color rayColor = Color.white;

    private RaycastShootTriggerable rcShoot;

    public override void Initialize(GameObject obj)
    {
        rcShoot = obj.GetComponent<RaycastShootTriggerable>();
        rcShoot.Initialize();

        rcShoot.rayDamage = rayDamage;
        rcShoot.rayRange = rayRange;
        rcShoot.hitForce = hitForce;
        rcShoot.lineRenderer.material = new Material(Shader.Find("Unlit/Color"));
        rcShoot.lineRenderer.material.color = rayColor;


    }

    public override void TriggerAbility()
    {
        rcShoot.Fire();
    }

}
