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

    public List<GameObject> currenHitObjects = new List<GameObject>();
    //public GameObject currenHitObject;

    public float sphereRadius;
    public float rayRange;
    public float rayDamage = 5;
    [HideInInspector]
    public int pierce;
    public int pierceMax;
    [HideInInspector]
    public int chain;
    [Header("ChainingStuff")]
    public int chainMax;                //Chain max displays if you have chain or not, 0 = no chain, 1 = have chain so chainmax should never go above 1
    public int chainNumbers;            //ChainNumbers always needs to be 1 less than chainnumbers
    public LayerMask layerMask;

    public bool block = false;

    [Header("When shooting more than one projectiles")]
    public int Projectiles = 1;
    public float endAngle = 45f;
    public float startingAngle = -45f;
    float fullAngle;
    float currentAngle;

    public GameObject projectileStart;

    Transform chainTarget;
    Collider nearest;

    public float sphereCastRange = 20f;
    [HideInInspector]
    public List<GameObject> alreadyChained = new List<GameObject>();
    //[HideInInspector]
    public List<Collider> tempColliders = new List<Collider>();

    int bob = 0;

    Vector3 currentHitPosition;
    #endregion


    public void Initialize()
    {
        //lineRenderer = GetComponent<LineRenderer>();

        //rayStart = GameObject.Find("Player").GetComponent<Transform>();
        //rayStart = rayStartPoint;
        if(chainNumbers > 1)
        {
            chainMax = 1;
        }
    }


    public void Fire()
    {
        
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
                currentHitDistance = rayRange;
                print(hit.collider.gameObject.transform);
                if(hit.transform.CompareTag("Object"))
                {
                    currentHitPosition = hit.collider.transform.position;
                    pierce = 0;
                    chain = 0;
                }

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
                currentHitDistance = Vector3.Distance(currentHitPosition, projectileStart.transform.position);
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
            hit.transform.gameObject.GetComponent<BasicEnemyFunctions>().TakeDamage(rayDamage);
            print("Piercing damage");
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
            Collider[] hitColliders = Physics.OverlapSphere(chainTarget.position, sphereCastRange, layerMask);
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
                if (nearest.transform.CompareTag("Enemy"))
                {
                    nearest.gameObject.GetComponent<BasicEnemyFunctions>().TakeDamage(rayDamage);
                }
                //and play sound here

            }
            tempColliders.Clear();
            Debug.DrawRay(projectileStart.transform.position, projectileStart.transform.forward * currentHitDistance, Color.red, .5f);
        }

        chain--;
    }


    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawLine(projectileStart.transform.position, projectileStart.transform.position + projectileStart.transform.forward * currentHitDistance);
    //    Gizmos.DrawWireSphere(projectileStart.transform.position + projectileStart.transform.transform.forward * currentHitDistance, sphereRadius);

    //}

    //private IEnumerator ShotEffect()
    //{
    //    //lineRenderer.enabled = true;

    //    yield return shotDuration;

    //    //lineRenderer.enabled = false;
    //}


}
