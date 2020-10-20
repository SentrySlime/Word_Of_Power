//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//[CreateAssetMenu(menuName = "Abilities/ProjectileAbilities")]
//public class GeneralAbilities : ScriptableObject
//{

//    public float projectileForce = 500f;
//    public float damage;
//    public GameObject projectile;


//    public override void Initialize(GameObject obj)
//    {
//        launcher = obj.GetComponent<ProjectileShootTriggerable>();
//        launcher.projectileCopy = projectile;
//        launcher.projectileForce = projectileForce;
//    }

//    public override void TriggerAbility()
//    {
//        launcher.Launch();
//    }

//}

