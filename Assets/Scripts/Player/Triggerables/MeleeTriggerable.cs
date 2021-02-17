using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeTriggerable : MonoBehaviour
{
    public Collider attackCollider;
    public Transform attackPos;


    [Header("Primary stats")]
    public float damage;
    public int critChance;
    public float range;

    [Header("Extra stats")]
    public float bleedPercentage;
    public float bleedDuration;
    public float leechAmount;
    public float energyOnHit;



    public void Hit()
    {



        float tempDistance = attackCollider.transform.localScale.z / 2;
        MeleeCollider meleeCollider = Instantiate(attackCollider, attackPos.transform.position + attackPos.transform.forward * tempDistance, attackPos.transform.rotation).GetComponent<MeleeCollider>();
        meleeCollider.transform.parent = null;

        meleeCollider.damage = damage;
        meleeCollider.critChance = critChance;

        meleeCollider.bleedPercentage = bleedPercentage;
        meleeCollider.bleedDuration = bleedDuration;
        meleeCollider.leechAmount = leechAmount;
        meleeCollider.energyOnHit = energyOnHit;

        //bob.transform.localScale *= scale;
        //bob.transform.position += bob.transform.forward * tempScale;
        StartCoroutine(ColliderDuration(.1f, meleeCollider.gameObject));

    }


    public IEnumerator ColliderDuration(float waitTime, GameObject collider)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(collider);
    }

}
