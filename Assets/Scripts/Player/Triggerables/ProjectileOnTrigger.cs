using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileOnTrigger : MonoBehaviour
{
    public BleedTriggerable bleedScript;
    

    public int piercing = 0;
    public int fork = 0;
    public int chain = 0;

    Vector3 startPos;
    float distance = 0;
    float maxDistance = 20f;

    private void Awake()
    {
        bleedScript = GameObject.Find("Player").GetComponent<BleedTriggerable>();
        startPos = transform.position;
    }

    private void Update()
    {
        distance = Vector3.Distance(startPos, transform.position);
        if (distance > maxDistance)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            other.GetComponent<GameObject>();
            bleedScript.SetBleed(other.gameObject);
            print("Called");
            //StartCoroutine(bleedScript.BleedTimer(other.gameObject));
            if (piercing >= 1)
            {
                piercing--;
            }
            else
            {
                Destroy(gameObject);
            }

        }
    }
}
