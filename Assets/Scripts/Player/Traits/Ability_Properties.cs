using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_Properties : MonoBehaviour
{

    ProjectileOnTrigger bleedProjectile;

    void Start()
    {
        bleedProjectile = GetComponent<ProjectileOnTrigger>();
    }

    void Update()
    {
        
    }


    public void Pierce(int pierceNumber)
    {
        bleedProjectile.piercing += pierceNumber;
    }

    public void Fork(int forkNumber)
    {
        bleedProjectile.fork += forkNumber;
    }

    public void Chain(int chainNumber)
    {
        bleedProjectile.chain += chainNumber;
    }

}
