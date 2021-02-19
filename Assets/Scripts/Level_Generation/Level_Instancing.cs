using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class Level_Instancing : MonoBehaviour
{
    public List<GameObject> itemsToPickFrom;
    public GameObject teleporter;


    public int gridX;
    public int gridZ;
    public float gridSpacingOffset = 1f;

    public float points = -1;
    public int randomNumber;

    public Vector3 gridOrigin = Vector3.zero;
    public List<NavMeshSurface> surfaces = new List<NavMeshSurface>();
    public GameObject forestSuroudning;

    private void Awake()
    {
        while(points == 4 || points == -1)
        {
            pickRanomdNuyer();
        }

        SpawnGrid();

        surfaces[0].BuildNavMesh();

        

    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {

            pickRanomdNuyer();

        }
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

        if(points == randomNumber)
        {
            surfaces.Add(Instantiate(teleporter, positionToSpawn, rotationToSpawn, gameObject.transform).GetComponent<NavMeshSurface>());
        }
        else
        {

            int randomIndex = Random.Range(0, itemsToPickFrom.Count);
            surfaces.Add(Instantiate(itemsToPickFrom[randomIndex], positionToSpawn, rotationToSpawn, gameObject.transform).GetComponent<NavMeshSurface>());
        }

        points++;

        //itemsToPickFrom.RemoveAt(randomIndex);
    }



    public void pickRanomdNuyer()
    { 
        int val = Random.Range(0, 10);
       
        if (val == 4)
        {
            print(4);
            pickRanomdNuyer();
        }
        else
            randomNumber = val;
    }
}