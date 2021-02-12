using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBleed : MonoBehaviour
{
    BasicEnemyFunctions basicEnemyFunctions;
    public bool isBleeding = false;
    public float bleedDuration;
    float bleedTimer;
    public float bleedPercentage;
    public float totalDamage;

    void Start()
    {
        basicEnemyFunctions = gameObject.GetComponent<BasicEnemyFunctions>();
        
    }

    
    void Update()
    {
        if (isBleeding == true)
        {
            StartCoroutine(BleedTimer());
        }
    }

    public void SetBleed(float duration, float percentage)
    {
        isBleeding = true;
        bleedDuration = duration;
        bleedPercentage = percentage;

    }



    public IEnumerator BleedTimer()
    {
        print(bleedPercentage);
        totalDamage = basicEnemyFunctions.maxHealth * bleedPercentage;
        print(totalDamage);

        while (bleedTimer < bleedDuration)
        {
            bleedTimer += Time.deltaTime;
            basicEnemyFunctions.TakeDamage(totalDamage * Time.deltaTime, 0);
            isBleeding = false;
            yield return null;
        }


        isBleeding = false;
        bleedTimer = 0f;
    }



}
