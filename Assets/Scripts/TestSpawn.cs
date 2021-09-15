using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawn : MonoBehaviour
{

    public GameObject bone;
    public GameObject enemy;
    public GameObject destroy;

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            Instantiate(bone, enemy.transform.position, enemy.transform.rotation);
            //Destroy(destroy);
        }
    }
}
