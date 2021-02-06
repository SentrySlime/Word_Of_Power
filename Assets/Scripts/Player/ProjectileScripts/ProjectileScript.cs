using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class ProjectileScript : MonoBehaviour
{
    float moveSpeed = 5000f;
    private Vector2 movedirection;

    public Rigidbody rb;
    public Transform chainTarget;
    public Collider nearest;

    public LayerMask layerMask;
    public Vector3 nextenemyDirection;

    public List<GameObject> alreadyChained = new List<GameObject>();
    public List<Collider> tempColliders = new List<Collider>();

    public int damage;

    int bob = 0;

    public int pierce = 0;
    public int pierceMax = 0;

    int chain = 0;
    public int chainNumbers = 2;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        pierce = pierceMax;
        chain = chainNumbers;
    }


    private void OnTriggerEnter(Collider other)
    {

        //other.GetComponent<EnemyHealth>().TakeDamage(100);
        chainTarget = other.transform;

        if (pierce >= 1)
        {
            other.GetComponent<BasicEnemyFunctions>().TakeDamage(damage);
            pierce--;
        }
        else if (chain >= 1)
        {
            ChainMethod();
            chain--;
        }
        else
        {
            other.GetComponent<BasicEnemyFunctions>().TakeDamage(damage);
            Destroy();
        }



    }

    public void ChainMethod()
    {
        if (chainNumbers >= 1 && chainTarget.CompareTag("Enemy"))
        {
            chainTarget.GetComponent<BasicEnemyFunctions>().TakeDamage(damage);
            if (alreadyChained.Count == 0)
            {
                alreadyChained.Add(chainTarget.gameObject);
            }

            Collider[] hitColliders = Physics.OverlapSphere(chainTarget.position, 80f, layerMask);
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
                        chainTarget = nearest.transform;
                    }
                }
            }

            if (alreadyChained.Contains(nearest.gameObject))
            {

            }
            else
            {
                nextenemyDirection = Vector3.zero;
                nextenemyDirection = (nearest.transform.position - transform.position).normalized;
                rb.velocity = Vector3.zero;
                rb.AddForce(nextenemyDirection * moveSpeed, ForceMode.Force);
                alreadyChained.Add(nearest.gameObject);
                chainNumbers--;
            }
            tempColliders.Clear();
        }
        else
        {
            Destroy();
        }
    }

    public void SetMoveDirection(Vector2 dir)
    {
        movedirection = dir;
    }

    private void Destroy()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
