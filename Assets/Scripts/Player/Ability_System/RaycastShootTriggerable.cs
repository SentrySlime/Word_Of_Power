using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastShootTriggerable : MonoBehaviour
{
    [HideInInspector]
    public float rayDamage = 1f;

    [HideInInspector]
    public float rayRange = 50f;

    [HideInInspector]
    public float hitForce = 1f;

    public Transform rayStartPoint;

    [HideInInspector]
    public LineRenderer lineRenderer;

    private Transform rayStart;

    private WaitForSeconds shotDuration = new WaitForSeconds(0.3f);


    public void Initialize()
    {
        lineRenderer = GetComponent<LineRenderer>();

        //rayStart = GameObject.Find("Player").GetComponent<Transform>();
        rayStart = rayStartPoint;

    }

    public void Fire()
    {
        Vector3 rayOrigin = rayStart.position;

        Debug.DrawRay(rayOrigin, rayStart.transform.forward * rayRange, Color.green);

        RaycastHit hit;

        StartCoroutine(ShotEffect());

        lineRenderer.SetPosition(0, rayStartPoint.position);

        if(Physics.Raycast(rayOrigin, rayStart.transform.forward, out hit, rayRange))
        {

            lineRenderer.SetPosition(1, hit.point);

            BasicEnemyFunctions health = hit.collider.GetComponent<BasicEnemyFunctions>();

            health.TakeDamage(rayDamage);

            //if (hit.collider.tag == "Enemy")
            //{
            //    BasicEnemyFunctions bEF = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BasicEnemyFunctions>();
            //    bEF.TakeDamage(rayDamage);
                
            //}

            //if(health != null)
            //{
            //    health.TakeDamage(rayDamage);
            //}

        }
        else
        {
            lineRenderer.SetPosition(1, rayStart.transform.forward * rayRange);
        }

    }

    private IEnumerator ShotEffect()
    {
        lineRenderer.enabled = true;

        yield return shotDuration;

        lineRenderer.enabled = false;
    }



}
