using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage_Player : MonoBehaviour
{

    public CharacterStats characterStats;
    public BasicEnemyFunctions beInfo;

    void Start()
    {
        characterStats = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>();
        beInfo = GetComponentInParent<BasicEnemyFunctions>();
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        print("Damaging");
        DealDamage();
    }

    private void OnTriggerStay(Collider other)
    {
    }

    public void DealDamage()
    {
        characterStats.FinalDefenceReduction(beInfo.damage);
    }

}
