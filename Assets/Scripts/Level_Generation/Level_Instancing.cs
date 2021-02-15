﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Instancing : MonoBehaviour
{
    public GameObject[] itemsToPickFrom;

    public int gridX;
    public int gridZ;
    public float gridSpacingOffset = 1f;

    public Vector3 gridOrigin = Vector3.zero;

   
    private void Start()
    {
        SpawnGrid();

    }


    void SpawnGrid()
    {
        for (int x = 0; x < gridX; x++)
        {
            for (int z = 0; z < gridZ; z++)
            {


                Vector3 spawnPosition = new Vector3(x * gridSpacingOffset, 0, z * gridSpacingOffset) + gridOrigin;
                PickandSpawn(spawnPosition, Quaternion.identity);
                
            }
        }
    }


    void PickandSpawn(Vector3 positionToSpawn, Quaternion rotationToSpawn)
    {
        int randomIndex = Random.Range(0, itemsToPickFrom.Length);
        GameObject clone = Instantiate(itemsToPickFrom[randomIndex], positionToSpawn, rotationToSpawn);

    }

}
