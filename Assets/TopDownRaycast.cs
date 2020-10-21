using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownRaycast : MonoBehaviour
{



    public GameObject player;

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            Shoot();
        }
    }


    void Shoot()
    {
        Vector3 startPos = player.transform.position;

        Vector3 endPos = player.transform.right * 10;

        Debug.DrawRay(startPos, endPos);

        RaycastHit hit;

        if(Physics.Raycast(startPos, endPos, out hit))
        {

            Debug.Log(hit.transform.name + "was hit");
        }
    }

}
