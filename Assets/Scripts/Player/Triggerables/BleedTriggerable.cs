using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BleedTriggerable : MonoBehaviour
{

    //[HideInInspector]
    public float bleedDuration = 50f;

    //[HideInInspector]
    public float bleedTimer = 0f;

    [HideInInspector]
    public float bleedPercentage = 10f;

    //[HideInInspector]
    public BasicEnemyFunctions target;
    
    //public GameObject enemy;

    public float percentageDamage;

    void Start()
    {

        
    }

    void Update()
    {   
        if (target != null && target.isBleeding == true)
        {
            print("Hello?=");
            StartCoroutine(BleedTimer());
        }
    }

    public void SetBleed(GameObject enemy)
    {
        target = enemy.GetComponent<BasicEnemyFunctions>();
        target.isBleeding = true;
    }

    public IEnumerator BleedTimer()
    {
        percentageDamage = target.maxHealth / bleedPercentage;
        print(percentageDamage);
        if(target != null)
        {
            while (bleedTimer < bleedDuration)
            {
                bleedTimer += Time.deltaTime;
                target.TakeDamage(percentageDamage * Time.deltaTime);
                target.isBleeding = false;
                yield return null;
            }
        }

        target.isBleeding = false;
        bleedTimer = 0f;
    }

}
