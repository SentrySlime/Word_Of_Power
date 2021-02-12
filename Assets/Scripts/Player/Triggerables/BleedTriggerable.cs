using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BleedTriggerable : MonoBehaviour
{

    //[HideInInspector]
    public float bleedDuration = 50f;

    //[HideInInspector]
    public float bleedTimer = 0f;

    public float bleedPercentage = 0.2f;            //  1 = 100%      0.5 = 50%    0.01 = 1%

    //[HideInInspector]
    public EnemyBleed target;
    
    //public GameObject enemy;

    public float percentageDamage;

    void Start()
    {

        
    }

    void Update()
    {   
        //if (target != null && target.isBleeding == true)
        //{
        //    StartCoroutine(BleedTimer());
        //}
    }

    public void SetBleed(GameObject enemy)
    {
        target = enemy.GetComponent<EnemyBleed>();
        target.isBleeding = true;



    }

    //public IEnumerator BleedTimer()
    //{
    //    print(bleedPercentage);
    //    percentageDamage = target.maxHealth * bleedPercentage;
    //    print(percentageDamage);
    //    if(target != null)
    //    {
    //        while (bleedTimer < bleedDuration)
    //        {

    //            bleedTimer += Time.deltaTime;
    //            target.TakeDamage(percentageDamage * Time.deltaTime, 0);
    //            target.isBleeding = false;
    //            yield return null;
    //        }
    //    }

    //    target.isBleeding = false;
    //    bleedTimer = 0f;
    //}

}
