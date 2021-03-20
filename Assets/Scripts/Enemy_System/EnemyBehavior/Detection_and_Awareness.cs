using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection_and_Awareness : MonoBehaviour
{

    public float detectionSize = 5;
    public bool aware = false;
    public Collider detectionArea;
    public LayerMask layermask;

    Collider[] closeEnemies;

    public Material material;
    public Color baseColor;

    void Start()
    {
        baseColor = material.color;
    }

    
    void Update()
    {
        if(aware == true)
        {
            material.color = Color.red;
        }
        else
        {
            material.color = baseColor;
        }
    }


    public void DetectionRelay()
    {
        closeEnemies = Physics.OverlapSphere(transform.position, detectionSize, layermask);
        for (int i = 0; i < closeEnemies.Length; i++)
        {

            closeEnemies[i].GetComponent<Detection_and_Awareness>().aware = true;
            closeEnemies[i].GetComponent<Detection_and_Awareness>().DetectionRelay();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            aware = true; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            aware = false;
        }
    }



}
