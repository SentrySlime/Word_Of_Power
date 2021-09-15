using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class ProjectileScript : MonoBehaviour
{
    public float moveSpeed = 0;
    private Vector2 movedirection;

    [HideInInspector]
    public Rigidbody rb;
    
    public Transform chainTarget;
    
    public Collider nearest;

    [HideInInspector]
    public Vector3 nextenemyDirection;

    public List<GameObject> alreadyChained = new List<GameObject>();
    public List<Collider> tempColliders = new List<Collider>();

    public CharacterStats characterStats;

    public LayerMask layerMask;

    SoundManager soundManager;

    [Header("Primary stats")]
    public float damage;
    public int criticalChance = 1;
    [HideInInspector]
    public int pierce = 0;
    [SerializeField]
    public int pierceMax = 1;
    [Header("Chaining Bullshit")]
    public int chain = 0;
    public int chainNumbers = 0;

    [Header("Extra stats")]
    public float bleedPercentage = 0;
    public float bleedDuration = 0;

    public float energyOnHit = 0;

    private void Start()
    {

        rb = gameObject.GetComponent<Rigidbody>();
        characterStats = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>();
        soundManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SoundManager>();
        pierce = pierceMax;
        chain = chainNumbers;
        
    }

    private void OnTriggerEnter(Collider other)
    {

        //other.GetComponent<EnemyHealth>().TakeDamage(100);
        
        if(other.CompareTag("Enemy"))
        {
            chainTarget = other.transform;

            int tempRand = UnityEngine.Random.Range(1, 5);
            if(tempRand == 1)
            {
                soundManager.ImpactSound1();
            }
            else if (tempRand == 2)
            {
                soundManager.ImpactSound2();
            }
            else if (tempRand == 3)
            {
                soundManager.ImpactSound3();
            }
            else if (tempRand == 4)
            {
                soundManager.ImpactSound4();
            }
            else if (tempRand == 5)
            {
                soundManager.ImpactSound5();
            }


            if (pierce >= 1)
            {
                DealDamage(other.gameObject);
                pierce--;
            }
            else if (chain >= 1)
            {
                ChainMethod();

            }
            else
            {
                DealDamage(other.gameObject);
                Destroy();
            }
        }
        



    }

    public void ChainMethod()
    {
        if (chainNumbers >= 1 && chainTarget.CompareTag("Enemy"))
        {
            DealDamage(chainTarget.gameObject);
            //chainTarget.GetComponent<BasicEnemyFunctions>().TakeDamage(damage, criticalChance);
            if (alreadyChained.Count == 0)
            {
                alreadyChained.Add(chainTarget.gameObject);
            }

            Collider[] hitColliders = Physics.OverlapSphere(chainTarget.position, 200f, layerMask);
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
                chain--;
            }
            tempColliders.Clear();
        }
        else if (chainTarget.CompareTag("Object"))
        {
            //Destroy();
        }
    }


    public void DealDamage(GameObject target)
    {
        if (chainTarget != null)
        {
            if(target.CompareTag("Enemy"))
            {
                target.GetComponent<BasicEnemyFunctions>().TakeDamage(damage, criticalChance);

                if (bleedPercentage > 0)
                {
                    target.GetComponent<EnemyBleed>().SetBleed(bleedDuration, bleedPercentage);
                }
            }
        }
        
        EnergyOnHit();
        
    }

    public void EnergyOnHit()
    {
        characterStats.EnergyOnHit(energyOnHit);
        //characterStats.currentEnergy += energyOnHit;
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
