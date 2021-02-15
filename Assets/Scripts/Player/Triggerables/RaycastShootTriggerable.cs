using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class RaycastShootTriggerable : MonoBehaviour
{
    #region old stuff

    //[HideInInspector]
    //public float rayDamage = 1f;

    //[HideInInspector]
    //public float rayRange;

    //public float maxRange = 50f;
    //[HideInInspector]
    //public float hitForce = 1f;

    //public Transform rayStartPoint;
    ////public LineRenderer lineRenderer;
    //public Transform rayStart;
    //private WaitForSeconds shotDuration = new WaitForSeconds(0.3f);

    //[Header("SphereCast Stats")]
    //public float sphereRadius = 0.5f;
    //public GameObject currentHitObject;
    public float currentHitDistance;
    //public LayerMask layerMask;

    #endregion


    #region new stuff

    [HideInInspector]
    public List<GameObject> currenHitObjects = new List<GameObject>();
    
    [HideInInspector]
    public List<GameObject> alreadyChained = new List<GameObject>();
    
    [HideInInspector]
    public List<Collider> tempColliders = new List<Collider>();
    public float sphereRadius;

    #region Basic stats

    public float damage = 5;
    public int criticalChance = 1;
    public int Projectiles = 1;
    public int pierceMax;
    public int chainNumbers;
    public float rayRange;
    public float chainRange = 20f;
    #endregion

    #region Extra stats

    public float bleedPercentage = 0;
    public float bleedDuration = 0;

    public float energyOnHit = 0;

    #endregion

    #region Hide stats
    [HideInInspector]
    public int pierce;
    [HideInInspector]
    public int chain;
    [Header("ChainingStuff")]
    public int chainMax;                //Chain max displays if you have chain or not, 0 = no chain, 1 = have chain so chainmax should never go above 1
    public LayerMask layerMask;
    public LayerMask layerMask2;
    public bool block = false;

    #endregion

    #region Multiple Projectile Angles

    public float endAngle = 45f;
    public float startingAngle = -45f;
    float fullAngle;
    float currentAngle;

    #endregion

    BleedTriggerable bleedTriggerable;
    CharacterStats characterStats;

    public GameObject projectileStart;
    public Vector3 currentHitPosition;
    Transform chainTarget;
    Collider nearest;

    int bob = 0;

    #endregion


    public void Initialize()
    {
        //lineRenderer = GetComponent<LineRenderer>();
        bleedTriggerable = GameObject.FindGameObjectWithTag("Player").GetComponent<BleedTriggerable>();
        characterStats = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>();
        //rayStart = GameObject.Find("Player").GetComponent<Transform>();
        //rayStart = rayStartPoint;
        if(chainNumbers > 1)
        {
            chainMax = 1;
        }
    }


    public void Fire()
    {
        currentHitDistance = rayRange;
        fullAngle = endAngle - startingAngle;
        fullAngle /= (1 + Projectiles);
        currentAngle = startingAngle;

        alreadyChained.Clear();
        tempColliders.Clear();

        for (int a = 0; a < Projectiles; a++)
        {
            
            currentAngle = currentAngle + fullAngle;
            projectileStart.transform.localRotation = Quaternion.Euler(0, currentAngle, 0);
            pierce = pierceMax;
            chain = chainMax;
            
            currenHitObjects.Clear();
            RaycastHit[] hits = Physics.SphereCastAll(projectileStart.transform.position, sphereRadius, projectileStart.transform.forward, rayRange, layerMask, QueryTriggerInteraction.Collide);
            
            foreach (RaycastHit hit in hits.OrderBy(x => x.distance))
            {
                //currentHitDistance = rayRange;
                if (hit.transform.CompareTag("Object"))
                {
                    currentHitPosition = hit.collider.transform.position;
                    currentHitDistance = Vector3.Distance(currentHitPosition, projectileStart.transform.position);
                    pierce = 0;
                    chain = 0;
                }

                //if(pierce == 0 && chain == 0)
                //{
                //    PierceMethod(hit);
                //}
                if (pierce >= 1)
                {
                    PierceMethod(hit);
                }
                else if (chain >= 1)
                {
                    ChainMethod(hit);
                }
                alreadyChained.Clear();
            }

            if (pierce >= 1)
            {
                currentHitDistance = rayRange;
                Debug.DrawRay(projectileStart.transform.position, projectileStart.transform.forward * currentHitDistance, Color.red, .1f);
                //Implement SFX here
            }
            else
            {
                //currentHitDistance = Vector3.Distance(currentHitPosition, projectileStart.transform.position);s
                Debug.DrawRay(projectileStart.transform.position, projectileStart.transform.forward * currentHitDistance, Color.red, .1f);
                //Implement SFX here
            }

        }
        
        projectileStart.transform.rotation = Quaternion.Euler(0, 0, 0);

    }


    public void PierceMethod(RaycastHit hit)
    {

        if (hit.transform.CompareTag("Enemy"))
        {
            currentHitPosition = hit.collider.transform.position;
            if(pierce == 1 && chain >= 1)
            {
                ChainMethod(hit);
            }
            DealDamage(hit.transform.gameObject);
            //hit.transform.gameObject.GetComponent<BasicEnemyFunctions>().TakeDamage(damage, criticalChance);

            //play sound here
            pierce--;
        }
        else if (hit.transform.CompareTag("Object"))
        {
            currentHitPosition = hit.collider.transform.position;
            pierce = 0;
        }
        
    }

    public void ChainMethod(RaycastHit hit)
    {
        chainTarget = hit.transform;
        for (int c = 0; c < chainNumbers; c++)
        {
            nearest = null;
            if (alreadyChained.Contains(chainTarget.gameObject))
            {
                alreadyChained.Add(chainTarget.gameObject);
            }
            Collider[] hitColliders = Physics.OverlapSphere(chainTarget.position, chainRange, layerMask2);
            tempColliders.AddRange(hitColliders);
            for (int i = 0; i < tempColliders.Count; i++)
            {
                nearest = tempColliders.OrderBy(t => Vector3.Distance(chainTarget.position, t.transform.position)).FirstOrDefault();
                for (int b = 0; b < alreadyChained.Count; b++)
                {
                    if (alreadyChained.Contains(nearest.gameObject))
                    {
                        tempColliders.Remove(nearest);
                    }
                    else
                    {
                        Vector3 start = chainTarget.position;
                        Vector3 destination = nearest.transform.position;
                        bob++;
                        if (bob < 2)
                        {
                            Debug.DrawLine(destination, start, Color.green, 1f);
                        }
                        else if (bob > 3)
                        {
                            Debug.DrawLine(destination, start, Color.red, 1f);
                        }

                        chainTarget = nearest.transform;
                    }
                }
            }

            if (alreadyChained.Contains(nearest.gameObject))
            {

            }
            else
            {
                chainTarget = nearest.transform;
                alreadyChained.Add(nearest.gameObject);
                currentHitPosition = hit.collider.transform.position;
                currentHitDistance = Vector3.Distance(currentHitPosition, projectileStart.transform.position);
                if (nearest.transform.CompareTag("Enemy"))
                {
                    DealDamage(nearest.gameObject);
                    //nearest.gameObject.GetComponent<BasicEnemyFunctions>().TakeDamage(damage, criticalChance);
                }
                //and play sound here

            }
            tempColliders.Clear();
            Debug.DrawRay(projectileStart.transform.position, projectileStart.transform.forward * currentHitDistance, Color.red, .5f);
        }

        chain--;
    }


    public void DealDamage(GameObject target)
    {
        target.GetComponent<BasicEnemyFunctions>().TakeDamage(damage, criticalChance);

        EnergyOnHit();

        if(bleedPercentage > 0)
        {

            target.GetComponent<EnemyBleed>().SetBleed(bleedDuration, bleedPercentage);
        }
    }


    public void EnergyOnHit()
    {
        characterStats.currentEnergy += energyOnHit;
    }
}
