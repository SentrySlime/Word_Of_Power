using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyMotor : MonoBehaviour
{

    NavMeshAgent enemyAgent;
    Camera cam;

    void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        cam = Camera.main;
    }


    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                EnemyMove(hit.point);
            }

        }
    }

    public void EnemyMove(Vector3 destination)
    {
        enemyAgent.SetDestination(destination);
    }


}
