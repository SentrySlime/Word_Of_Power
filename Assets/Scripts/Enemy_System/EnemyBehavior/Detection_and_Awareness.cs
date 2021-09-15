using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection_and_Awareness : MonoBehaviour
{

    public float detectionSize = 5;
    public bool aware = false;
    public LayerMask layermask;

    public Collider[] closeEnemies;

    public Material material;
    public Color baseColor;

    void Start()
    {
        ChangeColor();
    }

    
    void Update()
    {
        


    }


    public void DetectionRelay()
    {
        closeEnemies = Physics.OverlapSphere(transform.position, detectionSize, layermask);
        if(closeEnemies.Length != 0)
        {
            for (int i = 0; i < closeEnemies.Length; i++)
            {

                Detection_and_Awareness tempDetect = closeEnemies[i].GetComponent<Detection_and_Awareness>();
                if (tempDetect != null)
                    tempDetect.aware = true;
                    
                //closeEnemies[i].GetComponentInChildren<Detection_and_Awareness>().aware = true;
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            aware = true;
            ChangeColor();
            DetectionRelay();
        }
        

    }



    public void ChangeColor()
    {
        if (aware == true)
        {
            material.color = Color.red;
        }
        if (aware == false)
        {
            material.color = Color.white;
        }
    }

    private void OnDrawGizmosSelected()
    {

        //Gizmos.DrawSphere(transform.position, detectionSize);

    }

}
